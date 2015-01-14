// InSys.cpp : implementation file
//

#include "stdafx.h"
#include "RecTalk.h"
#include "InSys.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CInSys dialog


CInSys::CInSys(CWnd* pParent /*=NULL*/)
	: CDialog(CInSys::IDD, pParent)
{
	//{{AFX_DATA_INIT(CInSys)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}


void CInSys::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CInSys)
	DDX_Control(pDX, IDC_STATIC_VOLUME2, m_stcVolume2);
	DDX_Control(pDX, IDC_STATIC_LEVEL2, m_stcLevel2);
	DDX_Control(pDX, IDC_VOLUME_LEVEL2, m_VoLevel2);
	DDX_Control(pDX, IDC_VOLUME_LEVEL1, m_VoLevel1);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CInSys, CDialog)
	//{{AFX_MSG_MAP(CInSys)
	ON_WM_HSCROLL()
	ON_CBN_SELENDOK(IDC_COMBO_DEV1, OnSelendokComboDev1)
	//}}AFX_MSG_MAP
	
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CInSys message handlers

BOOL CInSys::OnInitDialog()
{
	CDialog::OnInitDialog();
	
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	m_bRecIni = false;
	m_bMinAuto = false;
	m_bMfnCnl = false;
	m_bDev1Chs = false;

	SendDlgItemMessage(IDC_VOLUME_LEVEL1, TBM_SETRANGE, 
						(WPARAM)TRUE, MAKELONG(0, 120));
	SendDlgItemMessage(IDC_VOLUME_LEVEL2, TBM_SETRANGE, 
						(WPARAM)TRUE, MAKELONG(0, 120));
	int rtr = 0;
	char vl[3];

	rtr = GetPrivateProfileInt("Params", "AutoStart", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(rtr == 1)
		SendDlgItemMessage(IDC_CHECK_AUTO,BM_SETCHECK ,BST_CHECKED,0);
	else
		SendDlgItemMessage(IDC_CHECK_AUTO,BM_SETCHECK ,BST_UNCHECKED,0);
	
	rtr = GetPrivateProfileInt("Params", "SysTray", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(rtr == 1)
		SendDlgItemMessage(IDC_CHECK_MIN,BM_SETCHECK ,BST_CHECKED,0);
	else
		SendDlgItemMessage(IDC_CHECK_MIN,BM_SETCHECK ,BST_UNCHECKED,0);

	rtr = GetPrivateProfileInt("Params", "MfnCanal", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(rtr == 1)
	{
		SendDlgItemMessage(IDC_CHECK_MFN,BM_SETCHECK ,BST_CHECKED,0);
		m_VoLevel2.EnableWindow(FALSE);
		m_stcLevel2.EnableWindow(FALSE);
		m_stcVolume2.EnableWindow(FALSE);
	}
	else
	{
		SendDlgItemMessage(IDC_CHECK_MFN,BM_SETCHECK ,BST_UNCHECKED,0);
		m_VoLevel2.EnableWindow(TRUE);
		m_stcLevel2.EnableWindow(TRUE);
		m_stcVolume2.EnableWindow(TRUE);
	}

	rtr = GetPrivateProfileInt("Params", "Volume1", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	SendDlgItemMessage(IDC_VOLUME_LEVEL1, TBM_SETPOS,(WPARAM)TRUE,rtr);
	_itoa(rtr, vl, 10);
	SetDlgItemText(IDC_STATIC_VOLUME1,vl);

	rtr = GetPrivateProfileInt("Params", "Volume2", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	SendDlgItemMessage(IDC_VOLUME_LEVEL2, TBM_SETPOS,(WPARAM)TRUE,rtr);
	_itoa(rtr, vl, 10);
	SetDlgItemText(IDC_STATIC_VOLUME2,vl);

	int i, NumWaveDevs;
    
	NumWaveDevs = waveInGetNumDevs ();
      
    for (i = 0; i < NumWaveDevs + 1; i++)
	{
        
		WAVEINCAPS DevCaps;

        waveInGetDevCaps (i - 1, &DevCaps, sizeof (DevCaps));
        SendDlgItemMessage (IDC_COMBO_DEV1,  CB_ADDSTRING,  0, (LPARAM)DevCaps.szPname);
    }

	rtr = GetPrivateProfileInt("Params", "DeviceRecord1", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(rtr > 0)
	{
		_itoa(rtr, vl, 10);
		SetDlgItemText(IDC_STATIC_DEV1,vl);
		rtr--;
		_itoa(rtr, vl, 10);
		SendDlgItemMessage (IDC_COMBO_DEV1,  CB_SETCURSEL,  rtr, 0);	//отображает значение по индексу
	}

	return TRUE;
}

void CInSys::OnOK() 
{
	if(m_bRecIni)
	{
		int rtr = 0;
		char vl[3];
		rtr = SendDlgItemMessage(IDC_VOLUME_LEVEL1,TBM_GETPOS,0,0);
		_itoa(rtr, vl, 10);
		WritePrivateProfileString("Params", "Volume1", vl, 
			                      "\\SoundDisp\\rectalk.ini");

		rtr = SendDlgItemMessage(IDC_VOLUME_LEVEL2,TBM_GETPOS,0,0);
		_itoa(rtr, vl, 10);
		WritePrivateProfileString("Params", "Volume2", vl, 
			                      "\\SoundDisp\\rectalk.ini");
	}
	
	if(m_bMinAuto || m_bMfnCnl || m_bDev1Chs)
		MessageBox("Для внесения изменений перезагрузите программу!",
					"Закрыть программу", MB_OK|MB_ICONINFORMATION);
	CDialog::OnOK();
}

BOOL CInSys::OnCommand(WPARAM wParam, LPARAM lParam) 
{
	switch (wParam)
	{
	case IDC_CHECK_AUTO:
		m_bMinAuto = true;
		if(SendDlgItemMessage(IDC_CHECK_AUTO,BM_GETCHECK,0,0) == BST_CHECKED)
		{
			WritePrivateProfileString("Params", "AutoStart", "1", 
			                      "\\SoundDisp\\rectalk.ini");
			//CButton* pStatus = (CButton*) 
			//AfxGetApp()->m_pMainWnd->GetDescendantWindow(IDC_BUTTON_REC1);
			//pStatus->EnableWindow(FALSE);
		}
		else
		{
			WritePrivateProfileString("Params", "AutoStart", "0", 
			                      "\\SoundDisp\\rectalk.ini");
			//CButton* pStatus = (CButton*) 
			//AfxGetApp()->m_pMainWnd->GetDescendantWindow(IDC_BUTTON_REC1);
			//pStatus->EnableWindow(TRUE);
		}
	break;
	case IDC_CHECK_MIN:
		m_bMinAuto = true;
		if(SendDlgItemMessage(IDC_CHECK_MIN,BM_GETCHECK,0,0) == BST_CHECKED)
		{
			WritePrivateProfileString("Params", "SysTray", "1", 
			                      "\\SoundDisp\\rectalk.ini");
		}
		else
		{
			WritePrivateProfileString("Params", "SysTray", "0", 
			                      "\\SoundDisp\\rectalk.ini");
		}
	break;
	case IDC_CHECK_MFN:
		m_bMfnCnl = true;
		if(SendDlgItemMessage(IDC_CHECK_MFN,BM_GETCHECK,0,0) == BST_CHECKED)
		{
			WritePrivateProfileString("Params", "MfnCanal", "1", 
			                      "\\SoundDisp\\rectalk.ini");
			m_VoLevel2.EnableWindow(FALSE);
			m_stcLevel2.EnableWindow(FALSE);
			m_stcVolume2.EnableWindow(FALSE);
		}
		else
		{
			WritePrivateProfileString("Params", "MfnCanal", "0", 
			                      "\\SoundDisp\\rectalk.ini");
			m_VoLevel2.EnableWindow(TRUE);
			m_stcLevel2.EnableWindow(TRUE);
			m_stcVolume2.EnableWindow(TRUE);
		}
	break;
	}
	
	return CDialog::OnCommand(wParam, lParam);
}

void CInSys::OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar) 
{
int rtr = 0;
char vl[3];

	int pos = pScrollBar->GetDlgCtrlID(); 
	switch (pos)
	{
	case IDC_VOLUME_LEVEL1:
			m_bRecIni = true;
			rtr = SendDlgItemMessage(IDC_VOLUME_LEVEL1,TBM_GETPOS,0,0);
			_itoa(rtr, vl, 10);
			SetDlgItemText(IDC_STATIC_VOLUME,vl);
	break;
	case IDC_VOLUME_LEVEL2:
			m_bRecIni = true;
			char vl[3];
			rtr = SendDlgItemMessage(IDC_VOLUME_LEVEL2,TBM_GETPOS,0,0);
			_itoa(rtr, vl, 10);
			SetDlgItemText(IDC_STATIC_VOLUME2,vl);
	break;
	}
	CDialog::OnHScroll(nSBCode, nPos, pScrollBar);
}

void CInSys::OnSelendokComboDev1() 
{
	CString str1;
	LRESULT rtr1 = SendDlgItemMessage (IDC_COMBO_DEV1,  CB_GETCURSEL,  0, 0);
	rtr1++;
	str1.Format("%d",rtr1);
	SetDlgItemText(IDC_STATIC_DEV1,str1);
	WritePrivateProfileString("Params", "DeviceRecord1", str1, 
			                      "\\SoundDisp\\rectalk.ini");
	m_bDev1Chs = true;
}
