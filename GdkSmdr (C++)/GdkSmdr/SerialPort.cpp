
#include "stdafx.h"
#include "SerialPort.h"

#include <assert.h>
 
//
// Constructor
//
CSerialPort::CSerialPort()
{
	m_hComm = NULL;

	// initialize overlapped structure members to zero
	m_ov.Offset = 0;
	m_ov.OffsetHigh = 0;

	// create events
	m_ov.hEvent = NULL;
	m_hWriteEvent = NULL;
	m_hShutdownEvent = NULL;

	m_szWriteBuffer = NULL;

	m_bThreadAlive = FALSE;
}

//
// Delete dynamic memory
//
CSerialPort::~CSerialPort()
{
	/*do
	{
		SetEvent(m_hShutdownEvent);
	} while (m_bThreadAlive);

	TRACE("Thread ended\n");*/

	delete [] m_szWriteBuffer;
}

//
// Initialize the port. This can be port 1 to 4.
//
BOOL CSerialPort::InitPort(CWnd* pPortOwner,	// the owner (CWnd) of the port (receives message)
						   UINT  portnr,		// portnumber (1..4)
						   UINT  baud,			// baudrate
						   char  parity,		// parity 
						   UINT  databits,		// databits 
						   UINT  stopbits,		// stopbits 
						   DWORD dwCommEvents,	// EV_RXCHAR, EV_CTS etc
						   UINT  writebuffersize)	// size to the writebuffer
{
	assert(portnr > 0 && portnr < 5);
	assert(pPortOwner != NULL);

	// if the thread is alive: Kill
	if (m_bThreadAlive)
	{
		do
		{
			SetEvent(m_hShutdownEvent);
		} while (m_bThreadAlive);
		TRACE("Thread ended\n");
	}

	// create events
	if (m_ov.hEvent != NULL)
		ResetEvent(m_ov.hEvent);
	m_ov.hEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

	if (m_hWriteEvent != NULL)
		ResetEvent(m_hWriteEvent);
	m_hWriteEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
	
	if (m_hShutdownEvent != NULL)
		ResetEvent(m_hShutdownEvent);
	m_hShutdownEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

	// initialize the event objects
	m_hEventArray[0] = m_hShutdownEvent;	// highest priority
	m_hEventArray[1] = m_ov.hEvent;
	m_hEventArray[2] = m_hWriteEvent;

	// initialize critical section
	InitializeCriticalSection(&m_csCommunicationSync);
	
	// set buffersize for writing and save the owner
	m_pOwner = pPortOwner;

	if (m_szWriteBuffer != NULL)
		delete [] m_szWriteBuffer;
	m_szWriteBuffer = new char[writebuffersize];

	m_nPortNr = portnr;

	m_nWriteBufferSize = writebuffersize;
	m_dwCommEvents = dwCommEvents;

	BOOL bResult = FALSE;
	char *szPort = new char[50];
	char *szBaud = new char[50];

	// now it critical!
	EnterCriticalSection(&m_csCommunicationSync);

	// if the port is already opened: close it
	if (m_hComm != NULL)
	{
		CloseHandle(m_hComm);
		m_hComm = NULL;
	}

	// prepare port strings
	sprintf(szPort, "COM%d", portnr);
	sprintf(szBaud, "baud=%d parity=%c data=%d stop=%d", baud, parity, databits, stopbits);

	// get a handle to the port
	m_hComm = CreateFile(szPort,						
					     GENERIC_READ | GENERIC_WRITE,	/s
					     0,								
					     NULL,							// no security attributes
					     OPEN_EXISTING,					
					     FILE_FLAG_OVERLAPPED,			
					     0);							

	if (m_hComm == INVALID_HANDLE_VALUE)
	{
		// port not found
		delete [] szPort;
		delete [] szBaud;

		return FALSE;
	}

	// set the timeout values
	m_CommTimeouts.ReadIntervalTimeout = 50;	
	m_CommTimeouts.ReadTotalTimeoutMultiplier = 10;	
	m_CommTimeouts.ReadTotalTimeoutConstant = 50;	
	m_CommTimeouts.WriteTotalTimeoutMultiplier = 10;	
	m_CommTimeouts.WriteTotalTimeoutConstant = 50;	

	// configure
	if (SetCommTimeouts(m_hComm, &m_CommTimeouts))
	{						   
		if (SetCommMask(m_hComm, dwCommEvents))
		{
			if (GetCommState(m_hComm, &m_dcb))
			{
				m_dcb.fRtsControl = RTS_CONTROL_ENABLE;		// set RTS bit high!
				if (BuildCommDCB(szBaud, &m_dcb))
				{
					if (SetCommState(m_hComm, &m_dcb))
						; // normal operation... continue
					else
						ProcessErrorMessage("SetCommState()");
				}
				else
					ProcessErrorMessage("BuildCommDCB()");
			}
			else
				ProcessErrorMessage("GetCommState()");
		}
		else
			ProcessErrorMessage("SetCommMask()");
	}
	else
		ProcessErrorMessage("SetCommTimeouts()");

	delete [] szPort;
	delete [] szBaud;

	// flush the port
	PurgeComm(m_hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);

	// release critical section
	LeaveCriticalSection(&m_csCommunicationSync);

	
	return TRUE;
}

//
//  The CommThread Function.
//
UINT CSerialPort::CommThread(LPVOID pParam)
{
	CSerialPort *port = (CSerialPort*)pParam;
	
	port->m_bThreadAlive = TRUE;	
		
	DWORD BytesTransfered = 0; 
	DWORD Event = 0;
	DWORD CommEvent = 0;
	DWORD dwError = 0;
	COMSTAT comstat;
	BOOL  bResult = TRUE;
		
	if (port->m_hComm)		// check if the port is opened
		PurgeComm(port->m_hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);

	// begin forever loop.  This loop will run as long as the thread is alive.
	for (;;) 
	{ 

		bResult = WaitCommEvent(port->m_hComm, &Event, &port->m_ov);

		if (!bResult)  
		{ 
			
			switch (dwError = GetLastError()) 
			{ 
			case ERROR_IO_PENDING: 	
				{ 
					
					break;
				}
			case 87:
				{
					
					break;
				}
			default:
				{
					
					port->ProcessErrorMessage("WaitCommEvent()");
					break;
				}
			}
		}
		else
		{
			
			bResult = ClearCommError(port->m_hComm, &dwError, &comstat);

			if (comstat.cbInQue == 0)
				continue;
		}	

		
		Event = WaitForMultipleObjects(3, port->m_hEventArray, FALSE, INFINITE);
		
		bResult = ClearCommError(port->m_hComm, &dwError, &comstat);
		if (comstat.cbInQue == 0)
			continue;
		switch (Event)
		{
		case 0:
			{
				
			 	port->m_bThreadAlive = FALSE;
				
				// Kill this thread.  break is not needed, but makes me feel better.
				AfxEndThread(100);
				break;
			}
		case 1:	// read event
			{
				GetCommMask(port->m_hComm, &CommEvent);
				/*if (CommEvent & EV_CTS)
					::SendMessage(port->m_pOwner->m_hWnd, WM_COMM_CTS_DETECTED, (WPARAM) 0, (LPARAM) port->m_nPortNr);
				if (CommEvent & EV_RXFLAG)
					::SendMessage(port->m_pOwner->m_hWnd, WM_COMM_RXFLAG_DETECTED, (WPARAM) 0, (LPARAM) port->m_nPortNr);
				if (CommEvent & EV_BREAK)
					::SendMessage(port->m_pOwner->m_hWnd, WM_COMM_BREAK_DETECTED, (WPARAM) 0, (LPARAM) port->m_nPortNr);
				if (CommEvent & EV_ERR)
					::SendMessage(port->m_pOwner->m_hWnd, WM_COMM_ERR_DETECTED, (WPARAM) 0, (LPARAM) port->m_nPortNr);
				if (CommEvent & EV_RING)
					::SendMessage(port->m_pOwner->m_hWnd, WM_COMM_RING_DETECTED, (WPARAM) 0, (LPARAM) port->m_nPortNr);
				*/
				if (CommEvent & EV_RXCHAR)
					// Receive character event from port.
					ReceiveChar(port, comstat);
					
				break;
			}  
		case 2: // write event
			{
				
				WriteChar(port);
				break;
			}

		} 

	} 

	return 0;
}

//
// start comm watching
//
BOOL CSerialPort::StartMonitoring()
{
	if (!(m_Thread = AfxBeginThread(CommThread, this)))
		return FALSE;
	TRACE("Thread started\n");
	return TRUE;	
}

//
// Restart the comm thread
//
BOOL CSerialPort::RestartMonitoring()
{
	TRACE("Thread resumed\n");
	m_Thread->ResumeThread();
	return TRUE;	
}


//
// Suspend the comm thread
//
BOOL CSerialPort::StopMonitoring()
{
	TRACE("Thread suspended\n");
	m_Thread->SuspendThread(); 
	return TRUE;	
}


//
// If there is a error, give the right message
//
void CSerialPort::ProcessErrorMessage(char* ErrorText)
{
	char *Temp = new char[200];
	
	LPVOID lpMsgBuf;

	FormatMessage( 
		FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
		NULL,
		GetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
		(LPTSTR) &lpMsgBuf,
		0,
		NULL 
	);

	sprintf(Temp, "WARNING:  %s Failed with the following error: \n%s\nPort: %d\n", (char*)ErrorText, lpMsgBuf, m_nPortNr); 
	MessageBox(NULL, Temp, "Application Error", MB_ICONSTOP);

	LocalFree(lpMsgBuf);
	delete[] Temp;
}

//
// Write a character.
//
void CSerialPort::WriteChar(CSerialPort* port)
{
	BOOL bWrite = TRUE;
	BOOL bResult = TRUE;

	DWORD BytesSent = 0;

	ResetEvent(port->m_hWriteEvent);

	// Gain ownership of the critical section
	EnterCriticalSection(&port->m_csCommunicationSync);

	if (bWrite)
	{
		// Initailize variables
		port->m_ov.Offset = 0;
		port->m_ov.OffsetHigh = 0;

		// Clear buffer
		PurgeComm(port->m_hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);

		bResult = WriteFile(port->m_hComm,							
							port->m_szWriteBuffer,					
							(DWORD)(strlen((char*)port->m_szWriteBuffer)),	
							&BytesSent,								
							&port->m_ov);							/

		
		if (!bResult)  
		{
			DWORD dwError = GetLastError();
			switch (dwError)
			{
				case ERROR_IO_PENDING:
					{
						
						BytesSent = 0;
						bWrite = FALSE;
						break;
					}
				default:
					{
						
						port->ProcessErrorMessage("WriteFile()");
					}
			}
		} 
		else
		{
			LeaveCriticalSection(&port->m_csCommunicationSync);
		}
	} 

	if (!bWrite)
	{
		bWrite = TRUE;
	
		bResult = GetOverlappedResult(port->m_hComm,	// Handle to COMM port 
									  &port->m_ov,		// Overlapped structure
									  &BytesSent,		// Stores number of bytes sent
									  TRUE); 			// Wait flag

		LeaveCriticalSection(&port->m_csCommunicationSync);

		
		if (!bResult)  
		{
			port->ProcessErrorMessage("GetOverlappedResults() in WriteFile()");
		}	
	} 

	// Verify that the data size send equals what we tried to send
	if (BytesSent != strlen((char*)port->m_szWriteBuffer))
	{
		TRACE("WARNING: WriteFile() error.. Bytes Sent: %d; Message Length: %d\n", BytesSent, strlen((char*)port->m_szWriteBuffer));
	}
}

//
// Character received. Inform the owner
//
void CSerialPort::ReceiveChar(CSerialPort* port, COMSTAT comstat)
{
	BOOL  bRead = TRUE; 
	BOOL  bResult = TRUE;
	DWORD dwError = 0;
	DWORD BytesRead = 0;
	unsigned char RXBuff;

	for (;;) 
	{ 
				
		EnterCriticalSection(&port->m_csCommunicationSync);

		
		bResult = ClearCommError(port->m_hComm, &dwError, &comstat);

		LeaveCriticalSection(&port->m_csCommunicationSync);

			
		if (comstat.cbInQue == 0)
		{
			
			break;
		}
						
		EnterCriticalSection(&port->m_csCommunicationSync);

		if (bRead)
		{
			bResult = ReadFile(port->m_hComm,		 
							   &RXBuff,				
							   1,					
							   &BytesRead,			
							   &port->m_ov);		
			 
			if (!bResult)  
			{ 
				switch (dwError = GetLastError()) 
				{ 
					case ERROR_IO_PENDING: 	
						{ 
							
							bRead = FALSE;
							break;
						}
					default:
						{
							
							port->ProcessErrorMessage("ReadFile()");
							break;
						} 
				}
			}
			else
			{
				
				bRead = TRUE;
			}
		}  

		if (!bRead)
		{
			bRead = TRUE;
			bResult = GetOverlappedResult(port->m_hComm,	
										  &port->m_ov,		
										  &BytesRead,		
										  TRUE); 			

			if (!bResult)  
			{
				port->ProcessErrorMessage("GetOverlappedResults() in ReadFile()");
			}	
		}  
				
		LeaveCriticalSection(&port->m_csCommunicationSync);

		// notify parent that a byte was received
		::SendMessage((port->m_pOwner)->m_hWnd, WM_COMM_RXCHAR, (WPARAM) RXBuff, (LPARAM) port->m_nPortNr);
	} 

}

//
// Write a string to the port
//
void CSerialPort::WriteToPort(char* string)
{		
	assert(m_hComm != 0);

	memset(m_szWriteBuffer, 0, sizeof(m_szWriteBuffer));
	strcpy(m_szWriteBuffer, string);

	// set event for write
	SetEvent(m_hWriteEvent);
}

//
// Return the device control block
//
DCB CSerialPort::GetDCB()
{
	return m_dcb;
}

//
// Return the communication event masks
//
DWORD CSerialPort::GetCommEvents()
{
	return m_dwCommEvents;
}

//
// Return the output buffer size
//
DWORD CSerialPort::GetWriteBufferSize()
{
	return m_nWriteBufferSize;
}




