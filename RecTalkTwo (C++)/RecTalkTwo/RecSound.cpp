// RecSound.cpp : implementation file
//

#include "stdafx.h"
#include "RecTalk.h"
#include "RecSound.h"
//#include "FileAcm.h"
//#include "FileAcmT.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CRecSound

IMPLEMENT_DYNCREATE(CRecSound, CWinThread)

CRecSound::CRecSound()
{
	m_Frequency = 8000;
	m_BitCount = 8;
	m_Channels = 2;
	int rtr = GetPrivateProfileInt("Params", "DeviceRecord1", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(rtr > 0)
		m_DeviceID = rtr - 2;
	else
		m_DeviceID = -1;
		
	m_wfx.wFormatTag = WAVE_FORMAT_PCM;
	m_wfx.nChannels = (WORD) m_Channels;
	m_wfx.nSamplesPerSec = (DWORD) m_Frequency;
	m_wfx.wBitsPerSample = (WORD) m_BitCount;
	m_wfx.nBlockAlign = (WORD)(m_BitCount/8*m_Channels);
	m_wfx.nAvgBytesPerSec = m_wfx.nSamplesPerSec*m_wfx.nBlockAlign;
    m_wfx.cbSize = 0;
}

CRecSound::~CRecSound()
{
}

BOOL CRecSound::InitInstance()
{
	// TODO:  perform and per-thread initialization here
	m_iMfnCanal = GetPrivateProfileInt("Params", "MfnCanal", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	m_iRecLevel1 = GetPrivateProfileInt("Params", "Volume1", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	m_iRecLevel2 = GetPrivateProfileInt("Params", "Volume2", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	int rtr = 0;
	rtr = GetPrivateProfileInt("Params", "AutoStart", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(rtr == 1)
	{
		m_Stop1 = false;
		m_Stop2 = false;
	}else
	{
		m_Stop1 = true;
		m_Stop2 = true;
	}
	m_blReStart = false;
	return TRUE;
}

int CRecSound::ExitInstance()
{
	// TODO:  perform any per-thread cleanup here
	
	return CWinThread::ExitInstance();
}

BEGIN_MESSAGE_MAP(CRecSound, CWinThread)
	//{{AFX_MSG_MAP(CRecSound)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		ON_THREAD_MESSAGE(WM_RECORDSOUND_STARTRECORDING,OnProcessRecording)
		ON_THREAD_MESSAGE(WM_RECORDSOUND_WRITETHREAD,OnPtrSoundWriter)
	//}}AFX_MSG_MAP
	
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CRecSound message handlers

void CRecSound::OnProcessRecording(WPARAM wParam, LPARAM lParam)
{

	ProcessRecord();
	//return TRUE;
}

void CRecSound::ProcessRecord()
{
int BlockSize = 8*1024;	//16*1024;
int WaitTime = BlockSize*2000/m_wfx.nAvgBytesPerSec;
m_Stop = false;
SYSTEM_INFO si;
GetSystemInfo(&si);
WAVEHDR Header[2];
ZeroMemory(Header, sizeof(Header));
m_hRecord = (HWAVEIN) 0;
HANDLE hEvent = CreateEvent(0, TRUE, FALSE, 0);
int signalStart1 = 0;
int signalStart2 = 0;
BYTE btRec1[32768];	//65536, 32768
BYTE btRec2[65536];
BYTE btSumRec1[65536];
//BYTE *btSumRec1;	
BYTE btSumRec2[65536];

	m_RecordPosition1 = 0;
	m_RecordPosition2 = 0;
	__try
	{
		waveInOpen(&m_hRecord, m_DeviceID, &m_wfx, 
			(DWORD) hEvent, 0, CALLBACK_EVENT);
		//Header[0].lpData = (LPSTR) malloc(BlockSize*2);
		//Header[0].lpData = (LPSTR) VirtualAlloc(0, (BlockSize*2+si.dwPageSize-1)
		//	/si.dwPageSize*si.dwPageSize, MEM_RESERVE|MEM_COMMIT, PAGE_READWRITE);
		Header[0].lpData = (LPSTR)GlobalAlloc(GMEM_FIXED, BlockSize);
		//Header[1].lpData = Header[0].lpData + BlockSize;
		Header[1].lpData = (LPSTR)GlobalAlloc(GMEM_FIXED, BlockSize);
		Header[0].dwBufferLength = Header[1].dwBufferLength = BlockSize;
		waveInPrepareHeader(m_hRecord, Header, sizeof(WAVEHDR));
		waveInPrepareHeader(m_hRecord, Header+1, sizeof(WAVEHDR));
		waveInAddBuffer(m_hRecord, Header, sizeof(WAVEHDR));
		waveInAddBuffer(m_hRecord, Header+1, sizeof(WAVEHDR));
		ResetEvent(hEvent);
		waveInStart(m_hRecord);
		int ii=0;
		int signal1 = 120;
		int signal2 = 120;
		int signalP1 = 0;
		int signalP2 = 0;
		int iByte1 = 0;
		int iByte2 = 0;
		
		for(;;)
		{
			if(WAIT_TIMEOUT==WaitForSingleObject(hEvent, WaitTime))
				MessageBox(m_hDlgRec, "Не удаётся записать данные - вышел лимит времени",
						"Ошибка записи в буфер", MB_OK|MB_ICONSTOP);
			
			//SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN2,"");
			//clock_t start, finish;
			//int duration;
			//start = clock();

			ResetEvent(hEvent);
			WAVEHDR *curhdr = Header + ((ii++)&1);
			if(curhdr ->dwBytesRecorded>0)
			{
				int iS1 = 1;
				int iS2 = 1;
				int i = 0;
				if(iByte1 >= 32768 || signalStart1 ==0)
				{
					//btSumRec1 = new BYTE[65536];
					iByte1 = 0;
				}
				if(iByte2 == 65536 || signalStart2 ==0)
					iByte2 = 0;
								
				while (i < 8191) 
				{
					memcpy (&btRec1[iByte1], &curhdr->lpData[i], 1);
					iS1 += abs(curhdr->lpData[i]);
					i++;
					iByte1++;
					memcpy (&btRec2[iByte2], &curhdr->lpData[i], 1);
					iS2 += abs(curhdr->lpData[i]);
					i++;
					iByte2++;
				}
				signal1 = iS1/4096;
				signal2 = iS2/4096;
				curhdr->dwBytesRecorded = 0;
				
				// 1 канал записи
				if(signal1 < m_iRecLevel1)
				{
					signalP1 = 0;
					signalStart1 = 1;
				}else
				{
					if(signalP1 > 7)
					{
						signalP1 = 0;
						m_RecordPosition1 = 0;
						signalStart1 = 0;
						memcpy(&btSumRec1,&btRec1,iByte1);
						m_FileAcmThread->PostThreadMessage(WM_RECORDSOUND_WRITEFILE1,
											(WPARAM)iByte1, (LPARAM)btSumRec1);	//закрыть файл				
						iByte1 = 0;
						SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN1,"");
					}else
					{
						if(signalStart1 == 1)
							signalP1++;
					}
				}
				if(signalStart1 == 1)
				{
					if(iByte1 == 32768)
					{
						memcpy(&btSumRec1,&btRec1,iByte1);
						m_FileAcmThread->PostThreadMessage(WM_RECORDSOUND_WRITEFILE1,
											(WPARAM)0, (LPARAM)btSumRec1);
					}
					char buffer[32];
					m_RecordPosition1 += 4096;
					_i64toa(m_RecordPosition1, buffer, 10);
					strcat(buffer, " байт");
					SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN1,buffer);
				}
				// 2 канал записи
				if(m_iMfnCanal == 0)
				{
					if(signal2 < m_iRecLevel2)
					{
						signalP2 = 0;
						signalStart2 = 1;
					}else
					{
						if(signalP2 > 7)
						{
							signalP2 = 0;
							m_RecordPosition2 = 0;
							signalStart2 = 0;
							memcpy(&btSumRec2,&btRec2,iByte2);
							m_FileAcmThreadT->PostThreadMessage(WM_RECORDSOUND_WRITEFILE2,
												(WPARAM)iByte2, (LPARAM)btSumRec2);	//закрыть файл				
							iByte2 = 0;
							SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN2,"");
						}else
						{
							if(signalStart2 == 1)
								signalP2++;
						}
					}
					if(signalStart2 == 1)
					{
						if(iByte2 == 65536)
						{
							memcpy(&btSumRec2,&btRec2,iByte2);
							m_FileAcmThreadT->PostThreadMessage(WM_RECORDSOUND_WRITEFILE2,
												(WPARAM)0, (LPARAM)btSumRec2);
						}
						char buffer[32];
						m_RecordPosition2 += 4096;
						_i64toa(m_RecordPosition2, buffer, 10);
						strcat(buffer, " байт");
						SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN2,buffer);
					}
				}

				if(m_Stop)
				{
					if(0==Header[0].dwBytesRecorded 
							&& 0==Header[1].dwBytesRecorded)
							break;
				}else waveInAddBuffer(m_hRecord, curhdr, sizeof(WAVEHDR));

				//finish = clock();
				//duration = (finish - start);
				//char buffer[32];
				//_itoa(duration, buffer, 10);
				//SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN2,buffer);
			}
		}
	}
	__finally
	{
		if(m_hRecord!=0)
		{
			waveInReset(m_hRecord);
			waveInUnprepareHeader(m_hRecord, Header, sizeof(WAVEHDR));
			waveInUnprepareHeader(m_hRecord, Header+1, sizeof(WAVEHDR));
			//free(Header[0].lpData);
			GlobalFree(Header[0].lpData);
			GlobalFree(Header[1].lpData);
			//if (Header[0].lpData!=0) VirtualFree(Header[0].lpData, 0, MEM_RELEASE);
			waveInClose(m_hRecord);
			m_hRecord = 0;
			
		}
	}
	StopRecording();
}

void CRecSound::OnPtrSoundWriter(WPARAM wParam, LPARAM lParam)
{
	m_FileAcmThread = (CFileAcm*) lParam;
	m_FileAcmThreadT = (CFileAcmT*) wParam;
	//return TRUE;
}

LRESULT CRecSound::StopRecording()
{
	if(m_blReStart == true)
	{
		m_Stop1 = false;
		m_Stop2 = false;
		m_blReStart = false;
		//PostThreadMessage(WM_RECORDSOUND_STARTRECORDING, 0, 0L);
	}else
	{
		SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN1,"Остановка записи!");
		SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN2,"Остановка записи!");
		Sleep(1000);
		m_FileAcmThread->ExitInstance();
		//m_FileAcmThreadT->ExitInstance();
		PostMessage(AfxGetApp()->m_pMainWnd->m_hWnd,WM_QUIT,0,0);
	}

	return TRUE;
}
