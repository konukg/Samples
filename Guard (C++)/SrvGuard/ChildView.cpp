// ChildView.cpp : implementation of the CChildView class
//

#include "stdafx.h"
#include "SrvGuard.h"
#include "ChildView.h"
#include "ClientSocket.h"
#include "SendDlg.h"

#ifdef __cplusplus
extern "C" {
#endif

#include "iBut\ownet.h"
#include "iBut\swt12.h"
#include "iBut\findtype.h"
#include "iBut\swt1f.h"


#ifdef __cplusplus
} // ends the extern "C" {
#endif

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define MAXDEVICES         15
#define SWITCH_FAMILY_S      0x12

static char *owErrorMsg[105] =
   {
   /*000*/ "No Error Was Set",
   /*001*/ "No Devices found on 1-Wire Network",
   /*002*/ "1-Wire Net Reset Failed",
   /*003*/ "Search ROM Error: Couldn't locate next device on 1-Wire",
   /*004*/ "Access Failed: Could not select device",
   /*005*/ "DS2480B Adapter Not Detected",
   /*006*/ "DS2480B: Wrong Baud",
   /*007*/ "DS2480B: Bad Response",
   /*008*/ "Open COM Failed",
   /*009*/ "Write COM Failed",
   /*010*/ "Read COM Failed",
   /*011*/ "Data Block Too Large",
   /*012*/ "Block Transfer failed",
   /*013*/ "Program Pulse Failed",
   /*014*/ "Program Byte Failed",
   /*015*/ "Write Byte Failed",
   /*016*/ "Read Byte Failed",
   /*017*/ "Write Verify Failed",
   /*018*/ "Read Verify Failed",
   /*019*/ "Write Scratchpad Failed",
   /*020*/ "Copy Scratchpad Failed",
   /*021*/ "Incorrect CRC Length",
   /*022*/ "CRC Failed",
   /*023*/ "Failed to acquire a necessary system resource",
   /*024*/ "Failed to initialize system resource",
   /*025*/ "Data too long to fit on specified device.",
   /*026*/ "Read exceeds memory bank end.",
   /*027*/ "Write exceeds memory bank end.",
   /*028*/ "Device select failed",
   /*029*/ "Read Scratch Pad verify failed.",
   /*030*/ "Copy scratchpad complete not found",
   /*031*/ "Erase scratchpad complete not found",
   /*032*/ "Address read back from scrachpad was incorrect",
   /*033*/ "Read page with extra-info not supported by this memory bank",
   /*034*/ "Read page packet with extra-info not supported by this memory bank",
   /*035*/ "Length of packet requested exceeds page size",
   /*036*/ "Invalid length in packet",
   /*037*/ "Program pulse required but not available",
   /*038*/ "Trying to access a read-only memory bank",
   /*039*/ "Current bank is not general purpose memory",
   /*040*/ "Read back from write compare is incorrect, page may be locked",
   /*041*/ "Invalid page number for this memory bank",
   /*042*/ "Read page with CRC not supported by this memory bank",
   /*043*/ "Read page with CRC and extra-info not supported by this memory bank",
   /*044*/ "Read back from write incorrect, could not lock page",
   /*045*/ "Read back from write incorrect, could not lock redirect byte",
   /*046*/ "The read of the status was not completed.",
   /*047*/ "Page redirection not supported by this memory bank",
   /*048*/ "Lock Page redirection not supported by this memory bank",
   /*049*/ "Read back byte on EPROM programming did not match.",
   /*050*/ "Can not write to a page that is locked.",
   /*051*/ "Can not lock a redirected page that has already been locked.",
   /*052*/ "Trying to redirect a locked redirected page.",
   /*053*/ "Trying to lock a page that is already locked.",
   /*054*/ "Trying to write to a memory bank that is write protected.",
   /*055*/ "Error due to not matching MAC.",
   /*056*/ "Memory Bank is write protected.",
   /*057*/ "Secret is write protected, can not Load First Secret.",
   /*058*/ "Error in Reading Scratchpad after Computing Next Secret.",
   /*059*/ "Load Error from Loading First Secret.",
   /*060*/ "Power delivery required but not available",
   /*061*/ "Not a valid file name.",
   /*062*/ "Unable to Create a Directory in this part.",
   /*063*/ "That file already exists.",
   /*064*/ "The directory is not empty.",
   /*065*/ "The wrong type of part for this operation.",
   /*066*/ "The max len for this file is too small.",
   /*067*/ "This is not a write once bank.",
   /*068*/ "The file can not be found.",
   /*069*/ "There is not enough space availabe.",
   /*070*/ "There is not a page to match that bit in the bitmap.",
   /*071*/ "There are no jobs for EPROM parts.",
   /*072*/ "Function not supported to modify attributes.",
   /*073*/ "Handle is not in use.",
   /*074*/ "Tring to read a write only file.",
   /*075*/ "There is no handle available for use.",
   /*076*/ "The directory provided is an invalid directory.",
   /*077*/ "Handle does not exist.",
   /*078*/ "Serial Number did not match with current job.",
   /*079*/ "Can not program EPROM because a non-EPROM part on the network.",
   /*080*/ "Write protect redirection byte is set.",
   /*081*/ "There is an inappropriate directory length.",
   /*082*/ "The file has already been terminated.",
   /*083*/ "Failed to read memory page of iButton part.",
   /*084*/ "Failed to match scratchpad of iButton part.",
   /*085*/ "Failed to erase scratchpad of iButton part.",
   /*086*/ "Failed to read scratchpad of iButton part.",
   /*087*/ "Failed to execute SHA function on SHA iButton.",
   /*088*/ "SHA iButton did not return a status completion byte.",
   /*089*/ "Write data page failed.",
   /*090*/ "Copy secret into secret memory pages failed.",
   /*091*/ "Bind unique secret to iButton failed.",
   /*092*/ "Could not install secret into user token.",
   /*093*/ "Transaction Incomplete: signature did not match.",
   /*094*/ "Transaction Incomplete: could not sign service data.",
   /*095*/ "User token did not provide a valid authentication response.",
   /*096*/ "Failed to answer a challenge on the user token.",
   /*097*/ "Failed to create a challenge on the coprocessor.",
   /*098*/ "Transaction Incomplete: service data was not valid.",
   /*099*/ "Transaction Incomplete: service data was not updated.",
   /*100*/ "Unrecoverable, catastrophic service failure occured.",
   /*101*/ "Load First Secret from scratchpad data failed.",
   /*102*/ "Failed to match signature of user's service data.",
   /*103*/ "Subkey out of range for the DS1991.",
   /*104*/ "Block ID out of range for the DS1991"
   };

// CChildView

CChildView::CChildView()
{
}

CChildView::~CChildView()
{
}

BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_TIMER()
	ON_WM_PAINT()
	ON_WM_CREATE()
	ON_WM_SIZE()
	ON_LBN_DBLCLK(IDR_LISTBOX,OnListToggle)
END_MESSAGE_MAP()

// CChildView message handlers

BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs) 
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle |= WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW|CS_DBLCLKS, 
		::LoadCursor(NULL, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW+1), NULL);

	return TRUE;
}

void CChildView::OnPaint() 
{
	CPaintDC dc(this); // device context for painting
	
	// TODO: Add your message handler code here
	
	// Do not call CWnd::OnPaint() for painting messages
}

int CChildView::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
	if (CWnd ::OnCreate(lpCreateStruct) == -1)
		return -1;
	listbox.Create(LBS_NOTIFY|LBS_NOINTEGRALHEIGHT|WS_CHILD|WS_VISIBLE,CRect(23,56,100,100),this,IDR_LISTBOX);
	editbox.CreateEx(CRect(0,0,0,0),this,1);
	pool.Init(this,PORT1);

	port = 0;
	strcpy( portname, "com1" );
	
	blTestDev = false;
	blStop = true;
	
	CString csString1;
	CStdioFile outFile;
	CFileException fileException;
	if ( !outFile.Open("srv.cfg",CFile::modeRead ) )
	{
		MessageBox("Не могу найти файл srv.cfg!","Файловая ошибка",MB_ICONERROR | MB_OK);
		::exit(0); 
	}else
	{
		outFile.ReadString(csString1);
		strcpy(&strCouplerNames[0][0],csString1);

		outFile.ReadString(csString1);
		strcpy(&strCouplerNames[1][0],csString1);

		outFile.ReadString(csString1);
		strcpy(&strCouplerNames[2][0],csString1);

		outFile.ReadString(csString1);
		strcpy(&strCouplerNames[3][0],csString1);

		outFile.ReadString(csDevNum);

		outFile.ReadString(csString1);
		IntSleepSend = 0;
		IntSleepSend = atoi(csString1);

		outFile.Close();
		iCouplerN = 4;	//количество в стволе Couplers
		blWireState = false;
		Init1_Wire();
	}
	return 0;
}

void CChildView::OnSize(UINT nType, int cx, int cy) 
{
	CWnd ::OnSize(nType, cx, cy);
	listbox.MoveWindow(0,0,cx/2,cy,TRUE) ;
	editbox.MoveWindow(cx/2,0,cx/2,cy,TRUE) ;
}

void CChildView::OnListToggle()
{
        //CSendDlg dlg;
		//dlg = new CSendDlg;
		//BOOL ret = dlg->Create(IDD_SEND_DLG);
        //dlg.m_data="message to send";
		//SendDlgItemMessage (IDC_COMBO1,  CB_ADDSTRING,  0, (LPARAM)"message to send");
		//dlg.m_cmb_cntr->AddString("message to send");
		//dlg->ShowWindow(SW_SHOW);
		//dlg.iSend = 1;
		//dlg.DoModal();
        //if( dlg->DoModal() == IDOK )
        //{
                //int i=listbox.GetCurSel();
                //pool.Send(i,dlg.m_data);
        //}
}

BOOL CChildView::UpdateView()
{
	CString ip,name;
	unsigned int port=PORT1;
	CClientSocket *pSock;

	listbox.ResetContent();
	POSITION pos = pool.connectionList.GetHeadPosition();
	for (int i=0;i < pool.connectionList.GetCount();i++)
	{
		pSock=(CClientSocket*)pool.connectionList.GetNext(pos);
		pSock->GetPeerName(ip,port);
		name=CString(pSock->Name+"  ip:"+ip);
		listbox.AddString(name);
	}
	return TRUE;
}

void CChildView::UpdateLog(LPCTSTR lpszString)
{
	editbox.AddText(lpszString);	
}

void CChildView::Init1_Wire(void)
{
	if(!blWireState)
	{
		int cnt = 0;
		while(cnt < 5)
		{
			if (!owAcquire(port,portname))
				cnt++;
			else
				break;
			
			Sleep(3000);
		}
		if (cnt >= 5)
		{
			char zstr[256];
			int err = owGetErrorNum();
			sprintf(zstr,"Error %d: %s\r\n",err,owErrorMsg[err]);
			MessageBox(zstr,"Ошибка контроллера ML97G",MB_ICONERROR | MB_OK);
			::exit(0); 
		}else
		{
			blWireState = true;
			if(blTestDev)
				pool.Send2All("Устройство Ml-97G открыто!#");
			
		}
	}else
	{
		if(blTestDev)
			pool.Send2All("Устройство Ml-97G уже открыто!#");
	}
}

void CChildView::TestWire(void)
{
	pool.Send2All("clear#");
	Sleep(150);
	TestDevWire();
	Sleep(150);
}

void CChildView::TestDevWire()
{
	int num,cnt,result,i,j;
	char strSwtSN[80];
	char zstr[256];
	uchar a[3];

	num = FindDevices(port, &CouplerTm[0], SWITCH_FAMILY, MAXDEVICES);
	cnt = num;
	while(cnt > 0)
	{
		for(i=0; i<8; i++)
		{
			sprintf(zstr,"%02X", CouplerTm[cnt-1][i]);
			if(i == 0)
				strcpy( strSwtSN, zstr );
			else
				strcat( strSwtSN, zstr );
			result = strcmp( &strCouplerNames[1][0], strSwtSN );
			if( result == 0 )
			{
				for (j = 0; j < 8; j++)
					CouplerBs[0][j] = CouplerTm[cnt-1][j];
				SetSwitch1F(port, CouplerTm[cnt-1], DIRECT_MAIN_ON, 0, a, TRUE);
				FindDevices(port, &CouplerF1[0], SWITCH_FAMILY, MAXDEVICES);
			}
		}
		cnt--;
		strcat( strSwtSN, "#" );
		pool.Send2All(strSwtSN);
		Sleep(IntSleepSend);
	}

	for(i=0; i<8; i++)
	{
		sprintf(zstr,"%02X", CouplerF1[0][i]);
		if(i == 0)
			strcpy( strSwtSN, zstr );
		else
			strcat( strSwtSN, zstr );
	}
	strcat( strSwtSN, "#" );
	pool.Send2All(strSwtSN);
	Sleep(IntSleepSend);

	SetSwitch1F(port, CouplerF1[0], DIRECT_MAIN_ON, 0, a, TRUE);
	Sleep(IntSleepSend);
	iSwitchMnFl1 = FindDevices(port, &SwitchMn_1[0], SWITCH_FAMILY_S, MAXDEVICES);
	Sleep(IntSleepSend);
	StateSwitch( &SwitchMn_1[0],iSwitchMnFl1 );

	SetSwitch1F(port, CouplerF1[0], AUXILARY_ON, 2, a, TRUE);
	Sleep(IntSleepSend);
	iSwitchAxFl1 = FindDevices(port, &SwitchAx_1[0], SWITCH_FAMILY_S, MAXDEVICES);
	Sleep(IntSleepSend);
	StateSwitch( &SwitchAx_1[0],iSwitchAxFl1 );
	
	Sleep(IntSleepSend);
	SetSwitch1F(port, CouplerF1[0], ALL_LINES_OFF, 0, a, TRUE);

	Sleep(IntSleepSend);
	SetSwitch1F(port, CouplerBs[0], AUXILARY_ON, 2, a, TRUE);
	Sleep(IntSleepSend);
	FindDevices(port, &CouplerF2[0], SWITCH_FAMILY, MAXDEVICES);

	for(i=0; i<8; i++)
	{
		sprintf(zstr,"%02X", CouplerF2[0][i]);
		if(i == 0)
			strcpy( strSwtSN, zstr );
		else
			strcat( strSwtSN, zstr );
	}
	strcat( strSwtSN, "#" );
	pool.Send2All(strSwtSN);
	Sleep(IntSleepSend);

	SetSwitch1F(port, CouplerF2[0], DIRECT_MAIN_ON, 0, a, TRUE);
	Sleep(IntSleepSend);
	iSwitchMnFl2 = FindDevices(port, &SwitchMn_2[0], SWITCH_FAMILY_S, MAXDEVICES);
	Sleep(IntSleepSend);
	StateSwitch( &SwitchMn_2[0],iSwitchMnFl2 );

	Sleep(IntSleepSend);
	SetSwitch1F(port, CouplerF2[0], AUXILARY_ON, 2, a, TRUE);
	Sleep(IntSleepSend);
	iSwitchAxFl2 = FindDevices(port, &SwitchAx_2[0], SWITCH_FAMILY_S, MAXDEVICES);
	Sleep(IntSleepSend);
	StateSwitch( &SwitchAx_2[0],iSwitchAxFl2 );
	
	Sleep(IntSleepSend);
	SetSwitch1F(port, CouplerF2[0], ALL_LINES_OFF, 0, a, TRUE);

	Sleep(IntSleepSend);
	CouplerClose(&strCouplerNames[1][0]);
}

void CChildView::StateSwitch(unsigned char FamilySN[][8], int cnt)
{
	int i;
	short infobyte=0;                  
	short clear=0;                  
	char zstr[256];
	char strSwtSN[80];
	char strSwtSt[80];

	while(cnt>0)
	{
		for(i=0; i<8; i++)
		{
			sprintf(zstr,"%02X", FamilySN[cnt-1][i]);
			if(i == 0)
				strcpy( strSwtSN, zstr );
			else
				strcat( strSwtSN, zstr );
		}
		owSerialNum(port, FamilySN[cnt-1], FALSE);
		infobyte = ReadSwitch12(port,clear);
		if(infobyte!=-1)
		{
			if(infobyte & 0x08)
			{
				strcpy( strSwtSt, strSwtSN );
				strcat( strSwtSt, " 1B#" );
				pool.Send2All(strSwtSt);
			}
			else
			{
				if(blTestDev)
				{
					strcpy( strSwtSt, strSwtSN );
					strcat( strSwtSt, " 0B#" );
					pool.Send2All(strSwtSt);
				}
			}
		
		Sleep(IntSleepSend);
			if(infobyte & 0x04)
			{
				strcpy( strSwtSt, strSwtSN );
				strcat( strSwtSt, " 1A#" );
				pool.Send2All(strSwtSt);
			}
			else
			{
	  			if(blTestDev)
				{	
					strcpy( strSwtSt, strSwtSN );
					strcat( strSwtSt, " 0A#" );
					pool.Send2All(strSwtSt);
				}
			}
		}
		cnt--;
	}
}

void CChildView::Close1_Wire(void)
{
	if(blWireState)
	{
		owRelease(port);
		pool.Send2All("Устройство Ml-97G закрыто!#");
		blWireState = false;
	}else pool.Send2All("Устройство Ml-97G уже закрыто!#");
}

void CChildView::CouplerClose(char *SerialNum)
{
	int cnt,num,i,result;
	char strSwtSN[80];
	char zstr[256];
	uchar a[3];

	num = iCouplerN;
	while(num>0)
	{
		for(i=0; i<8; i++)
		{
			sprintf(zstr,"%02X", CouplerTm[num-1][i]);
			if(i == 0)
				strcpy( strSwtSN, zstr );
			else
				strcat( strSwtSN, zstr );
		}
		result = strcmp( SerialNum, strSwtSN );
		if( result == 0 )
		{
			cnt = SetSwitch1F(port, CouplerTm[num-1], ALL_LINES_OFF, 0, a, TRUE);
			if(cnt == 1)
				break;
		}
		num--;
	}
}

void CChildView::OnTimer(UINT nIDEvent)
{
	switch (nIDEvent)
	{
	case 1:
		if(editbox.GetLineCount() > 200)
		{
			CString strFile,strText,strLine;
			CStdioFile outFile;
			CFileException fileException;
			
			strFile = DateMsg();
			if ( !outFile.Open(strFile, CFile::modeCreate | CFile::modeWrite ) )
			{
				TRACE( "Can't open file %s, error = %u\n", strFile, fileException.m_cause );
				outFile.Close();
				MessageBox("File error","");
			}else
			{
				KillTimer(1);
				int i, nLineCount = editbox.GetLineCount();
				for (i=0;i < nLineCount;i++)
				{
					int len = editbox.LineLength(editbox.LineIndex(i));
					editbox.GetLine(i, strText.GetBuffer(len), len);
					strText.ReleaseBuffer(len);
					strLine.Format(TEXT("%s\n"), strText);
					outFile.WriteString(strLine);
				}
				outFile.Close();
				editbox.SetSel(0, -1);
				editbox.Clear();
			}
		}
		TestDevWire();
	break;
	}
}

void CChildView::StartGuard(void)
{
	if(blWireState)
	{
		owRelease(port);
		blWireState = false;
		Init1_Wire();
	}else Init1_Wire();
	Sleep(500);
	
	TestDevWire();
	SetTimer(1, 1000, 0);
	blStop = false;
}

void CChildView::StopGuard(void)
{
	blStop = true;	//stop timer
	KillTimer(1);
	if(blWireState)
	{
		owRelease(port);
		blWireState = false;
	}
}

CString CChildView::DateMsg()
{
	CString date;
	CTime t = CTime::GetCurrentTime();
	date.Format("Guard date %d_%02d_%02d time %02d_%02d_%02d",	t.GetYear(),
				 												t.GetMonth(),
																t.GetDay(),
																t.GetHour(),
																t.GetMinute(),
																t.GetSecond());
	return date;
}