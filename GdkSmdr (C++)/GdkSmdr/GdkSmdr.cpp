// GdkSmdr.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "GdkSmdr.h"
#include "MainFrm.h"
#include ".\gdksmdr.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CGdkSmdrApp

BEGIN_MESSAGE_MAP(CGdkSmdrApp, CWinApp)
	ON_COMMAND(ID_APP_ABOUT, OnAppAbout)
	ON_COMMAND(ID_APP_SHOW, OnShowApp)
END_MESSAGE_MAP()


// CGdkSmdrApp construction

CGdkSmdrApp::CGdkSmdrApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CGdkSmdrApp object

CGdkSmdrApp theApp;

// CGdkSmdrApp initialization

BOOL CGdkSmdrApp::InitInstance()
{
	// InitCommonControls() is required on Windows XP if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	InitCommonControls();

	CWinApp::InitInstance();

	// Initialize OLE libraries
	if (!AfxOleInit())
	{
		AfxMessageBox(IDP_OLE_INIT_FAILED);
		return FALSE;
	}
	
	HWND hWnd;
	hWnd=FindWindow(NULL, "Запись SMDR GDK-162");
	if(hWnd)
	{
		if(IsIconic(hWnd))
			ShowWindow(hWnd,SW_RESTORE);
		SetForegroundWindow(hWnd);
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
	// To create the main window, this code creates a new frame window
	// object and then sets it as the application's main window object
	//CMainFrame* 
		pFrame = new CMainFrame;
	if (!pFrame)
		return FALSE;
	m_pMainWnd = pFrame;
	// create and load the frame with its resources
	pFrame->LoadFrame(IDR_MAINFRAME,
		WS_OVERLAPPEDWINDOW | FWS_ADDTOTITLE, NULL,
		NULL);
	// The one and only window has been initialized, so show and update it
	pFrame->m_TrayIcon.ShowIcon();
	m_showApp1 = pFrame->m_showApp;
	
	//pFrame->ShowWindow(SW_SHOW);
	//pFrame->UpdateWindow();
	
	// call DragAcceptFiles only if there's a suffix
	//  In an SDI app, this should occur after ProcessShellCommand
	m_pDlgMain = new CDlgMain();
	BOOL ret = m_pDlgMain->Create(IDD_DIALOG_MAIN);
	m_pDlgMain->ShowWindow(SW_SHOW);
	//m_pDlgMain->SendDlgItemMessage(IDC_CHECK1, BM_SETCHECK ,BST_CHECKED,0);
	return TRUE;
}


// CGdkSmdrApp message handlers



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
public:
	afx_msg void OnClose();
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	ON_WM_CLOSE()

END_MESSAGE_MAP()

// App command to run the dialog
void CGdkSmdrApp::OnAppAbout()
{
	CAboutDlg aboutDlg;
	aboutDlg.DoModal();
}


// CGdkSmdrApp message handlers


void CAboutDlg::OnClose()
{
	// TODO: Add your message handler code here and/or call default
	//MessageBeep(0);
	CDialog::OnClose();
}

BOOL CGdkSmdrApp::OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo)
{
	// TODO: Add your specialized code here and/or call the base class
	if(nID == WM_APP_HIDE)	//WM_CLOSE)	//ID_APP_SHOW)
	{
		//if(pFrame->m_showApp)
			MessageBeep(0);
	}
	return CWinApp::OnCmdMsg(nID, nCode, pExtra, pHandlerInfo);
}

void CGdkSmdrApp::OnShowApp(void)
{
	pFrame->ShowWindow(SW_SHOW);
	pFrame->m_TrayIcon.HideIcon();
	pFrame->m_showApp = true;
}