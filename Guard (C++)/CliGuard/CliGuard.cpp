// CliGuard.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "CliGuard.h"
#include "MainFrm.h"
#include ".\cliguard.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CCliGuardApp

BEGIN_MESSAGE_MAP(CCliGuardApp, CWinApp)
	ON_COMMAND(ID_APP_ABOUT, OnAppAbout)
	ON_COMMAND(ID_FLOOR, OnFloor)
	ON_COMMAND(ID_SENSOR2, OnSensor2)
	ON_COMMAND(ID_SENSOR3, OnSensor3)
	ON_COMMAND(ID_SETPARAM, OnSetparam)
END_MESSAGE_MAP()


// CCliGuardApp construction

CCliGuardApp::CCliGuardApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CCliGuardApp object

CCliGuardApp theApp;

// CCliGuardApp initialization

BOOL CCliGuardApp::InitInstance()
{
	HWND hWnd;
	hWnd=FindWindow(NULL, "Охрана объекта");
	if(hWnd)
	{
		if(IsIconic(hWnd))
			ShowWindow(hWnd,SW_RESTORE);
		SetForegroundWindow(hWnd);
		return FALSE;
	}
	// InitCommonControls() is required on Windows XP if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	InitCommonControls();

	CWinApp::InitInstance();

	if (!AfxSocketInit())
	{
		AfxMessageBox(IDP_SOCKETS_INIT_FAILED);
		return FALSE;
	}

	// Initialize OLE libraries
	if (!AfxOleInit())
	{
		AfxMessageBox(IDP_OLE_INIT_FAILED);
		return FALSE;
	}
	AfxEnableControlContainer();
	// Standard initialization
	// If you are not using these features and wish to reduce the size
	// of your final executable, you should remove from the following
	// the specific initialization routines you do not need
	// Change the registry key under which our settings are stored
	// TODO: You should modify this string to be something appropriate
	// such as the name of your company or organization

	SetRegistryKey(_T("Local AppWizard-Generated Applications"));
	
	//CNewMenu::SetMenuDrawMode(CNewMenu::STYLE_XP_2003);
	// To create the main window, this code creates a new frame window
	// object and then sets it as the application's main window object
	CMainFrame* pFrame = new CMainFrame;
	m_pMainWnd = pFrame;
	// create and load the frame with its resources
	pFrame->LoadFrame(IDR_MAINFRAME,
		FWS_ADDTOTITLE, NULL,
		NULL);
	// The one and only window has been initialized, so show and update it
	pFrame->ShowWindow(SW_SHOW);
	pFrame->UpdateWindow();
	// call DragAcceptFiles only if there's a suffix
	//  In an SDI app, this should occur after ProcessShellCommand
	m_pDlgMain = new CDlgMain();
	BOOL ret = m_pDlgMain->Create(IDD_DIALOG_MAIN);
	m_pDlgMain->ShowWindow(SW_SHOW);
	
	return TRUE;
}


// CCliGuardApp message handlers



// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()

// App command to run the dialog
void CCliGuardApp::OnAppAbout()
{
	CAboutDlg aboutDlg;
	aboutDlg.DoModal();
	this->m_pMainWnd->ShowWindow(SW_SHOW);
	//m_pDlgMain-;
}


// CCliGuardApp message handlers


void CCliGuardApp::OnFloor()
{
	m_pDlgMain->ObjectFill();
}

void CCliGuardApp::OnSensor2()
{
	m_pDlgMain->SensorFill(2);
}

void CCliGuardApp::OnSensor3()
{
	m_pDlgMain->SensorFill(3);
}

void CCliGuardApp::OnSetparam()
{
	m_pDlgInput.DoModal();
}

CWnd* CCliGuardApp::GetMainWnd()
{
	// TODO: Add your specialized code here and/or call the base class

	return CWinApp::GetMainWnd();
}

BOOL CCliGuardApp::OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo)
{
	// TODO: Add your specialized code here and/or call the base class

	return CWinApp::OnCmdMsg(nID, nCode, pExtra, pHandlerInfo);
}
