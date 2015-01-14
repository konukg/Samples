// MainFrm.cpp : implementation of the CMainFrame class
//

#include "stdafx.h"
#include "CliGuard.h"

#include "MainFrm.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CMainFrame

IMPLEMENT_DYNAMIC(CMainFrame, CNewFrameWnd)

BEGIN_MESSAGE_MAP(CMainFrame, CNewFrameWnd)
	ON_WM_CREATE()
	ON_WM_SETFOCUS()
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
		
	if (!m_wndToolBar.CreateEx(this, TBSTYLE_FLAT|TBSTYLE_TRANSPARENT, WS_CHILD | WS_VISIBLE | CBRS_TOP
    | CBRS_GRIPPER | CBRS_TOOLTIPS | CBRS_FLYBY | CBRS_SIZE_DYNAMIC | CCS_VERT) ||
    !m_wndToolBar.LoadToolBar(IDR_MAINFRAME))
	{
		TRACE0("Failed to create toolbar\n");
		return -1;      // fail to create
	}

	/*CRect temp;
	// adjust the size of button with text
	m_wndToolBar.GetItemRect(0,&temp);
	m_wndToolBar.SetSizes(CSize(temp.Width(),temp.Height()),CSize(16,15));
	m_wndToolBar.LoadHiColor(MAKEINTRESOURCE(IDR_MAINFRAME));*/

	if (!m_wndStatusBar.Create(this) ||
		!m_wndStatusBar.SetIndicators(indicators,
		  sizeof(indicators)/sizeof(UINT)))
	{
		TRACE0("Failed to create status bar\n");
		return -1;      // fail to create
	}
	// TODO: Delete these three lines if you don't want the toolbar to be dockable
	//m_wndToolBar.EnableDocking(CBRS_ALIGN_ANY);
	//EnableDocking(CBRS_ALIGN_ANY);
	//DockControlBar(&m_wndToolBar);
	CNewMenu::SetMenuDrawMode(CNewMenu::STYLE_XP_2003);
	
	CMenu* pSysMenu = GetSystemMenu(FALSE);
    //pSysMenu->AppendMenu(MF_SEPARATOR);
	pSysMenu->DeleteMenu(4,MF_BYPOSITION);

	if(m_SystemNewMenu.m_hMenu)
    {
      ::DestroyMenu(m_SystemNewMenu.Detach());
    }
    m_SystemNewMenu.Attach(::GetSystemMenu(m_hWnd,FALSE));

	m_DefaultNewMenu.LoadToolBar(IDR_MAINFRAME);
  
	//m_SystemNewMenu.SetMenuTitle(_T("SDI Sample"),MFT_LINE|MFT_CENTER|MFT_ROUND);
	//m_SystemNewMenu.SetMenuTitle(_T("MDI Sample"),MFT_SUNKEN|MFT_LINE|MFT_CENTER|MFT_SIDE_TITLE,IDR_MAINFRAME);
	//m_SystemNewMenu.SetMenuTitleColor(RGB(255,0,0),CLR_DEFAULT,RGB(0,0,255));

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
		| WS_SYSMENU | WS_MINIMIZEBOX;

	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	//cs.lpszClass = AfxRegisterWndClass(0);

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

void CMainFrame::OnSetFocus(CWnd* /*pOldWnd*/)
{
	// forward focus to the view window
	m_wndToolBar.SetFocus();
}
