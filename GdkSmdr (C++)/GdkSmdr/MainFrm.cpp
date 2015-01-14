// MainFrm.cpp : implementation of the CMainFrame class
//

#include "stdafx.h"
#include "GdkSmdr.h"

#include "MainFrm.h"
#include ".\mainfrm.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define	WM_ICON_NOTIFY			WM_APP+100

#ifndef SYSTEMTRAY_USEW2K
#define NIIF_WARNING 0
#endif
//#define	WM_ICON_NOTIFY			WM_USER+100
// CMainFrame

IMPLEMENT_DYNAMIC(CMainFrame, CFrameWnd)

BEGIN_MESSAGE_MAP(CMainFrame, CFrameWnd)
	//ON_COMMAND(ID_APP_SHOW, OnShowApp)
	ON_WM_CREATE()
	ON_WM_SETFOCUS()
	
	ON_WM_CLOSE()
END_MESSAGE_MAP()

static UINT indicators[] =
{
	ID_SEPARATOR,           // status line indicator
	ID_INDICATOR_CAPS,
	ID_INDICATOR_NUM,
	ID_INDICATOR_SCRL,
};


// CMainFrame construction/destruction

CMainFrame::CMainFrame()
{
	// TODO: add member initialization code here
}

CMainFrame::~CMainFrame()
{
}


int CMainFrame::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CFrameWnd::OnCreate(lpCreateStruct) == -1)
		return -1;
		
	if (!m_wndToolBar.CreateEx(this, TBSTYLE_FLAT, WS_CHILD | WS_VISIBLE | CBRS_TOP
		| CBRS_GRIPPER | CBRS_TOOLTIPS | CBRS_FLYBY | CBRS_SIZE_DYNAMIC) ||
		!m_wndToolBar.LoadToolBar(IDR_MAINFRAME))
	{
		TRACE0("Failed to create toolbar\n");
		return -1;      // fail to create
	}

	if (!m_wndStatusBar.Create(this) ||
		!m_wndStatusBar.SetIndicators(indicators,
		  sizeof(indicators)/sizeof(UINT)))
	{
		TRACE0("Failed to create status bar\n");
		return -1;      // fail to create
	}
	// TODO: Delete these three lines if you don't want the toolbar to be dockable
	m_wndToolBar.EnableDocking(CBRS_ALIGN_ANY);
	EnableDocking(CBRS_ALIGN_ANY);
	DockControlBar(&m_wndToolBar);

	if (!m_TrayIcon.Create(NULL,                            // Parent window
                           WM_ICON_NOTIFY,                  // Icon notify message to use
                           _T("Gdk-162 SMDR - нажмите правую кнопку!"),  // tooltip
                           AfxGetApp()->LoadIcon(IDR_MAINFRAME),  // Icon to use
						   IDR_POPUP_MENU_GDK))                // ID of tray icon
	return -1;
	m_exitApp = false;
	m_showApp = false;

	return 0;
}

BOOL CMainFrame::PreCreateWindow(CREATESTRUCT& cs)
{
	if( !CFrameWnd::PreCreateWindow(cs) )
		return FALSE;
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	//cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	//cs.lpszClass = AfxRegisterWndClass(0);
	cs.style = WS_OVERLAPPED | WS_CAPTION | FWS_ADDTOTITLE
		| WS_SYSMENU;	// | WS_MINIMIZEBOX;

	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	cs.cx = 600;
	cs.cy = 400;

	return TRUE;
}


// CMainFrame diagnostics

#ifdef _DEBUG
void CMainFrame::AssertValid() const
{
	CFrameWnd::AssertValid();
}

void CMainFrame::Dump(CDumpContext& dc) const
{
	CFrameWnd::Dump(dc);
}

#endif //_DEBUG


// CMainFrame message handlers

void CMainFrame::OnSetFocus(CWnd* /*pOldWnd*/)
{
	// forward focus to the view window
	//m_wndView.SetFocus();
}

BOOL CMainFrame::OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo)
{
	// let the view have first crack at the command
	//if (m_wndView.OnCmdMsg(nID, nCode, pExtra, pHandlerInfo))
		//return TRUE;

	// otherwise, do default handling
	return CFrameWnd::OnCmdMsg(nID, nCode, pExtra, pHandlerInfo);
}

BOOL CMainFrame::OnCommand(WPARAM wParam, LPARAM lParam)
{
	// TODO: Add your specialized code here and/or call the base class
	switch (wParam)
	{
	case ID_APP_EXIT:
		int rtr = MessageBox( "Вы хотите закрыть программу?",
						"Закрыть программу", MB_YESNO|MB_ICONSTOP);
		if(rtr == 6)
		{
			m_TrayIcon.HideIcon();
			UpdateWindow();
			m_exitApp = true;
		}
	break;
	}

	return CFrameWnd::OnCommand(wParam, lParam);
}

/*void CMainFrame::OnShowApp(void)
{
	ShowWindow(SW_SHOW);
	m_TrayIcon.HideIcon();
	m_showApp = true;
}*/

void CMainFrame::OnClose()
{
	// TODO: Add your message handler code here and/or call default
	if(m_exitApp)
	{
		m_TrayIcon.HideIcon();
		CFrameWnd::OnClose();
	}
	else
	{
		//AfxGetApp()->m_pActiveWnd->SendMessage(WM_APP_HIDE, 0, 0);
		//PostMessage(WM_APP_HIDE, 0, 0);
		ShowWindow(SW_HIDE);
		m_showApp = false;
		m_TrayIcon.ShowIcon();
		UpdateWindow();
		
	}
}
