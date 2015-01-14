// DlgMain.cpp : implementation file
//

#include "stdafx.h"
#include "CliGuard.h"
#include "DlgMain.h"
#include "MySocket.h"
#include ".\dlgmain.h"

// CDlgMain dialog

IMPLEMENT_DYNAMIC(CDlgMain, CDialog)
CDlgMain::CDlgMain(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgMain::IDD, pParent)
{
	m_strConnection = _T("DSN=Guard;UID=MyUserId;PWD=MyPassword;");
	
	m_strCmdText = _T("SELECT * FROM Swtloop WHERE(SwitState = 0) ORDER BY SwitID");
	
	m_pRs = NULL;
		
	m_iCntRec = 0;
	m_sDlgSwitID = 0;
	m_sDlgCoupID = 0;
	m_fDlgSwitState = FALSE;
	m_strDlgSwitName = _T("");
	m_strDlgSwitPlace = _T("");
	m_strDlgSwitImg = _T("");
	m_strDlgSwitNote = _T("");
}

CDlgMain::~CDlgMain()
{
	CString strCmnd = "stop";
	pSocket->Send(strCmnd,strCmnd.GetLength());
	Sleep(300);
	m_pRs->Close();  //= NULL;
	m_pRsFind->Close(); //= NULL;
	m_pRsObj->Close(); //= NULL;
	m_pConn->Close();
}

void CDlgMain::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_BUTTON_CLEAR, m_cClkear);
	DDX_Control(pDX, IDC_BUTTON_START, m_cStart);
	DDX_Control(pDX, IDC_BUTTON_STOP, m_cStop);
}

BEGIN_MESSAGE_MAP(CDlgMain, CDialog)

ON_BN_CLICKED(IDC_BUTTON_START, OnBnClickedButtonStart)
ON_BN_CLICKED(IDC_BUTTON_STOP, OnBnClickedButtonStop)
ON_BN_CLICKED(IDC_BUTTON_CLEAR, OnBnClickedButtonClear)
ON_LBN_SELCHANGE(IDC_LIST_DEV, OnLbnSelchangeListDev)

//ON_WM_ACTIVATE()
END_MESSAGE_MAP()


// CDlgMain message handlers

BOOL CDlgMain::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  Add extra initialization here
	
	blConnect = false;
	blStart = false;
	blTest = false;
	blSensorFill = false;
	
	m_cClkear.SetIcon(AfxGetApp()->LoadIcon(IDI_ICON_DEL));
	m_cStop.EnableWindow(FALSE);

	OpenRecordSet();
	ObjectRecordSet();
	m_pRsFind = m_pRs->Clone(adLockUnspecified);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

void CDlgMain::AddText(LPCTSTR lpszString)
{
	
	if(blStart)
	{
		_bstr_t bstrName, strFindID, bstrTime;
		char strPlace[50];
		char strTime[10];
		long recCount = 0;
		struct tm *newtime;
		__time64_t long_time;
		char *token;
		char seps[] = ":";
		int iTimeHrB, iTimeMinB;
		bool blTimeSt;
		
		strFindID = lpszString;
		m_pRsFind->Filter = "SwitName = '"+strFindID+"'";
		recCount = m_pRsFind->GetRecordCount();
		if(recCount == 1)
		{
			bstrName = m_pRsFind->Fields->Item["SwitPlace"]->Value;
			strcpy(strPlace, bstrName);
			bstrTime = m_pRsFind->Fields->Item["SwitTime"]->Value;
				strcpy(strTime, bstrTime);
				token = strtok( strTime, seps );
				iTimeHrB = atoi(token);
				token = strtok( NULL, seps );
				iTimeMinB = atoi(token);
				token = NULL;
			blTimeSt = false;
			_time64( &long_time );                /* Get time as long integer. */
			newtime = _localtime64( &long_time ); /* Convert to local time. */
				if( newtime->tm_hour > iTimeHrB )
				{
					blTimeSt = true;
				}else
				{
					if( newtime->tm_hour == iTimeHrB )
					{
						if( newtime->tm_min >= iTimeMinB )
							blTimeSt = true;
					}
				}
			if(SendDlgItemMessage(IDC_LIST_DEV,LB_FINDSTRING,0,(LPARAM)strPlace) == -1 && blTimeSt == true)
			{
				RecReport(strPlace, lpszString);
				SendDlgItemMessage(IDC_LIST_DEV,LB_ADDSTRING,0,(LPARAM)strPlace);
				if(!m_pDlgImage->blPlay)
					m_pDlgImage->StartPlaySound();
			}
		}
		m_pRsFind->Filter = (long)adFilterNone;
	}
}

void CDlgMain::RecReport(LPCTSTR lpszStrMsg, LPCTSTR lpszStrNote)
{
	bstr_t strSQL;
	CString s1,s2,s3,s4,s5,s6,s7,sSum;
	
	s1 = "INSERT INTO Report (RptId,RptDate,RptMsg,RptNote) VALUES ('";
	m_iCntRec++;
	s2.Format("%d", m_iCntRec);
	CTime t = CTime::GetCurrentTime();
	s3.Format("%02d.%02d.%d",	t.GetDay(),
				 				t.GetMonth(),
								t.GetYear());
	
	s4.Format("%02d:%02d:%02d",	t.GetHour(),
								t.GetMinute(),
								t.GetSecond());
	s5 = s3 + " " + s4;
	s6 = lpszStrMsg;
	s7 = lpszStrNote;

	sSum = s1 + s2 + "','" + s5 + "','" + s6 + "','" + s7 + "')";
	strSQL = sSum;
	m_pConn->Execute(strSQL, NULL, adExecuteNoRecords);
}

void CDlgMain::OnBnClickedButtonStart()
{
	CString strCmnd = "start";
	_RecordsetPtr rstRpt = NULL;
	_bstr_t bstrName;
	CString strCmdText;

	SendDlgItemMessage(IDC_LIST_DEV,LB_RESETCONTENT ,0,0);
	if(!blConnect)
		ConnectToServer();
		
	if(blConnect)
	{
		m_pDlgImage->m_FilePathName = "start.wmf";
		m_pDlgImage->ShowWindow(SW_HIDE);
		m_pDlgImage->ShowWindow(SW_SHOWNOACTIVATE);

		strCmdText = _T("SELECT * FROM Report ORDER BY RptId");
		TESTHR(rstRpt.CreateInstance(__uuidof(Recordset)));
		rstRpt->CursorLocation = adUseClient;
		
		rstRpt->Open((LPCTSTR)strCmdText, _variant_t((IDispatch *) m_pConn, true), 
							adOpenKeyset, adLockOptimistic, adCmdText);
		if(rstRpt->GetRecordCount() > 0)
		{
			rstRpt->MoveLast();
			bstrName = rstRpt->Fields->Item["RptId"]->Value;
			m_iCntRec = atoi(bstrName);
		}else m_iCntRec = 0;
		rstRpt->Close();
		pSocket->Send(strCmnd,strCmnd.GetLength());
		m_cStart.EnableWindow(FALSE);
		m_cStop.EnableWindow(TRUE);
		blStart = true;
		blTest = false;
		blSensorFill = false;
		SetDlgItemText(IDC_STATIC_INFO,"Сработал датчик");
		RecReport("Старт системы охраны объекта", " ");
	}
}

void CDlgMain::OnBnClickedButtonStop()
{
	CString strCmnd = "stop";
	
	SendDlgItemMessage(IDC_LIST_DEV,LB_RESETCONTENT ,0,0);
	SetDlgItemText(IDC_STATIC_INFO,"Информация системы");
	if(blStart)
	{
		m_pDlgImage->DestroyWindow();
		m_pDlgImage = new CDlgImage();
		BOOL ret = m_pDlgImage->Create(IDD_DIALOG_IMAGE);
		m_pDlgImage->ShowWindow(SW_SHOW);

		pSocket->Send(strCmnd,strCmnd.GetLength());
	}
	m_cStart.EnableWindow(TRUE);
	m_cStop.EnableWindow(FALSE);
		
	blStart = false;
	RecReport("Стоп системы охраны объекта", " ");
	blConnect = false;
	delete pSocket;
	ObjectFill();
}

void CDlgMain::OnBnClickedButtonClear()
{
	if(blConnect)
	{
		m_pDlgImage->m_FilePathName = "start.wmf";
		m_pDlgImage->ShowWindow(SW_HIDE);
		m_pDlgImage->ShowWindow(SW_SHOWNOACTIVATE);
	}else
	{
		SetDlgItemText(IDC_STATIC_INFO,"Информация системы");
		m_pDlgImage->DestroyWindow();
		m_pDlgImage = new CDlgImage();
		BOOL ret = m_pDlgImage->Create(IDD_DIALOG_IMAGE);
		m_pDlgImage->ShowWindow(SW_SHOW);
	}
	SendDlgItemMessage(IDC_LIST_DEV,LB_RESETCONTENT ,0,0);	
}

void CDlgMain::ConnectToServer(void)
{
	pSocket = new MySocket(this);
    pSocket->Create();
	
	char buffer[4096];
	GetPrivateProfileString("Params", "Name", NULL,
            buffer, sizeof(buffer), "cliguard.ini");
	Name = buffer;
	GetPrivateProfileString("Params", "IP-adress", NULL,
            buffer, sizeof(buffer), "cliguard.ini");
	ip = buffer;
	if(pSocket->Connect(ip, 2049))
	{
		blConnect = true;
		pSocket->Send(Name,Name.GetLength());
		Sleep(100);
	}
	else
	{
		delete pSocket;
		blConnect = false;
		int err=pSocket->GetLastError();
		CString s ;
		s.Format("Не удалось установить связь с сервером охраны!");
		AfxMessageBox(s,MB_ICONSTOP);
	}
}

void CDlgMain::OnLbnSelchangeListDev()
{
	if(blConnect || blSensorFill)
	{
		char bfStrDev[30];
		
		LRESULT pos = SendDlgItemMessage(IDC_LIST_DEV,LB_GETCURSEL,0,0);
		SendDlgItemMessage(IDC_LIST_DEV,LB_GETTEXT,pos,(LPARAM)bfStrDev);
		if(strlen(bfStrDev)>0)
		{
			_bstr_t bstrName, strFindID;
			char strDirImg[50];
			long recCount = 0;
			
			strFindID = bfStrDev;
			m_pRs->Filter = "SwitPlace = '"+strFindID+"'";
			recCount = m_pRs->GetRecordCount();
			if(recCount == 1)
			{
				if(m_pDlgImage->blPlay)
					m_pDlgImage->StopPlaySound();
				if(m_pRs->Fields->Item["SwitImg"]->ActualSize)
				{
					bstrName = m_pRs->Fields->Item["SwitImg"]->Value;
					strcpy(strDirImg, bstrName);
					m_pDlgImage->m_FilePathName = strDirImg;
					m_pDlgImage->ShowWindow(SW_HIDE);
					m_pDlgImage->ShowWindow(SW_SHOWNOACTIVATE);
				}else
				{
					m_pDlgImage->DestroyWindow();
					m_pDlgImage = new CDlgImage();
					BOOL ret = m_pDlgImage->Create(IDD_DIALOG_IMAGE);
					m_pDlgImage->ShowWindow(SW_SHOW);
				}
			}
			m_pRs->Filter = (long)adFilterNone;
		}
	}else
		ObjectSelChange();
}

void CDlgMain::ObjectSelChange(void)
{
	char bfStrDev[30];
		
	LRESULT pos = SendDlgItemMessage(IDC_LIST_DEV,LB_GETCURSEL,0,0);
	SendDlgItemMessage(IDC_LIST_DEV,LB_GETTEXT,pos,(LPARAM)bfStrDev);
	if(strlen(bfStrDev)>0)
	{
		_bstr_t bstrName, strFindID;
		char strDirImg[50];
		long recCount = 0;
			
		strFindID = bfStrDev;
		m_pRsObj->Filter = "ObjectName = '"+strFindID+"'";
		recCount = m_pRsObj->GetRecordCount();
		if(recCount == 1)
		{
			if(m_pRsObj->Fields->Item["ObjectImg"]->ActualSize)
			{
				bstrName = m_pRsObj->Fields->Item["ObjectImg"]->Value;
				strcpy(strDirImg, bstrName);
				m_pDlgImage->m_FilePathName = strDirImg;
				m_pDlgImage->ShowWindow(SW_HIDE);
				m_pDlgImage->ShowWindow(SW_SHOWNOACTIVATE);
			}else
			{
				m_pDlgImage->DestroyWindow();
				m_pDlgImage = new CDlgImage();
				BOOL ret = m_pDlgImage->Create(IDD_DIALOG_IMAGE);
				m_pDlgImage->ShowWindow(SW_SHOW);
			}
		}
		m_pRsObj->Filter = (long)adFilterNone;
	}
}

void CDlgMain::OpenRecordSet(void)
{
	HRESULT hr = NOERROR;
	IADORecordBinding *piAdoRecordBinding = NULL;
		
	try
	{
		bstr_t strCnn;
		strCnn = m_strConnection;
		TESTHR(m_pConn.CreateInstance(__uuidof(Connection)));
		m_pConn->Open (strCnn, "", "", adConnectUnspecified);
		
		TESTHR(m_pRs.CreateInstance(__uuidof(Recordset)));
		m_pRs->CursorLocation = adUseClient;
		m_pRs->Open((LPCTSTR)m_strCmdText, _variant_t((IDispatch *) m_pConn, true), 
					adOpenStatic, adLockOptimistic, adCmdText);
		
		TESTHR(m_pRs->QueryInterface(__uuidof(IADORecordBinding), (LPVOID *)&piAdoRecordBinding));
		TESTHR(piAdoRecordBinding->BindToRecordset(this));

		RefreshBoundData();
	}
	catch (_com_error &e)
	{
		GenerateError(e.Error(), e.Description());
	}

	if (piAdoRecordBinding)
		piAdoRecordBinding->Release();
}

void CDlgMain::ObjectRecordSet(void)
{
	HRESULT hr = NOERROR;
	IADORecordBinding *piAdoRecordBinding = NULL;
	CString strCmdText;

	strCmdText = _T("SELECT * FROM Objects ORDER BY ObjectID");
	try
	{
		bstr_t strCnn;
		strCnn = m_strConnection;
		TESTHR(m_pConn.CreateInstance(__uuidof(Connection)));
		m_pConn->Open (strCnn, "", "", adConnectUnspecified);
		
		TESTHR(m_pRsObj.CreateInstance(__uuidof(Recordset)));
		m_pRsObj->CursorLocation = adUseClient;
		m_pRsObj->Open((LPCTSTR)strCmdText, _variant_t((IDispatch *) m_pConn, true), 
					adOpenStatic, adLockOptimistic, adCmdText);
		
		m_pDlgImage = new CDlgImage();
		BOOL ret = m_pDlgImage->Create(IDD_DIALOG_IMAGE);
		m_pDlgImage->ShowWindow(SW_SHOW);
		ObjectFill();
	}
	catch (_com_error &e)
	{
		GenerateError(e.Error(), e.Description());
	}
	
}

void CDlgMain::ObjectFill(void)
{
	if(!blStart)
	{
		blSensorFill = false;
		if(m_pRsObj->GetRecordCount() > 0)
		{
			_bstr_t bstrName;
			char strName[50];

			SendDlgItemMessage(IDC_LIST_DEV,LB_RESETCONTENT ,0,0);
			while (!m_pRsObj->EndOfFile)
			{
				bstrName = m_pRsObj->Fields->Item["ObjectName"]->Value;
				strcpy(strName, bstrName);
				SendDlgItemMessage(IDC_LIST_DEV,LB_ADDSTRING,0,(LPARAM)strName);
				m_pRsObj->MoveNext();
			}
			SetDlgItemText(IDC_STATIC_INFO,"Охраняемые объекты");
			SendDlgItemMessage(IDC_LIST_DEV,LB_SETCURSEL,0,0);
			ObjectSelChange();
		}
	}
}

void CDlgMain::SensorFill(int iFloorNumber)
{
	if(!blStart)
	{
		_bstr_t bstrName, strFindID;
		char strPlace[50];
		long recCount = 0;
		
		strFindID = iFloorNumber;
		m_pRsFind->Filter = "CoupID = '"+strFindID+"'";
		recCount = m_pRsFind->GetRecordCount();
		if(recCount > 0)
		{
			SetDlgItemText(IDC_STATIC_INFO,"Датчики охраны объекта");
			SendDlgItemMessage(IDC_LIST_DEV,LB_RESETCONTENT ,0,0);
			blSensorFill = true;
			while (!m_pRsFind->EndOfFile)
			{
				bstrName = m_pRsFind->Fields->Item["SwitPlace"]->Value;
				strcpy(strPlace, bstrName);
				SendDlgItemMessage(IDC_LIST_DEV,LB_ADDSTRING,0,(LPARAM)strPlace);
				m_pRsFind->MoveNext();
			}
		}
		m_pRsFind->Filter = (long)adFilterNone;
		SendDlgItemMessage(IDC_LIST_DEV,LB_SETCURSEL,0,0);
		OnLbnSelchangeListDev();
	}
}

void CDlgMain::RefreshBoundData()
{
	if (adFldOK == lSwitIDStatus)
		m_sDlgSwitID = m_sSwitID;
	else
		m_sDlgSwitID = 0;
	if (adFldOK == lCoupIDStatus)
		m_sDlgCoupID = m_sCoupID;
	else
		m_sDlgCoupID = 0;
	if (adFldOK == lSwitStateStatus)
		m_fDlgSwitState = VARIANT_FALSE == m_fSwitState ? FALSE : TRUE;
	else
		m_fDlgSwitState = FALSE;
	if (adFldOK == lSwitNameStatus)
		m_strDlgSwitName = m_wszSwitName;
	else
		m_strDlgSwitName = _T("");
	if (adFldOK == lSwitPlaceStatus)
		m_strDlgSwitPlace = m_wszSwitPlace;
	else
		m_strDlgSwitPlace = _T("");
	if (adFldOK == lSwitImgStatus)
		m_strDlgSwitImg = m_wszSwitImg;
	else
		m_strDlgSwitImg = _T("");
	if (adFldOK == lSwitNoteStatus)
		m_strDlgSwitNote = m_wszSwitNote;
	else
		m_strDlgSwitNote = _T("");

	UpdateData(FALSE);
}

void CDlgMain::GenerateError(HRESULT hr, PWSTR pwszDescription)
{
	CString strError;

	strError.Format("Run-time error '%d (%x)'", hr, hr);
	strError += "\n\n";
	strError += pwszDescription;

	AfxMessageBox(strError);
}


