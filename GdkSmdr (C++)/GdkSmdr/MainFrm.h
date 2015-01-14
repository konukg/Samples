// MainFrm.h : interface of the CMainFrame class
//


#pragma once

#include "ChildView.h"
#include "SystemTray.h"

#define	WM_SHOW_NOTIFY			WM_USER+101
#define	WM_APP_HIDE				WM_USER+102

class CMainFrame : public CFrameWnd
{
	
public:
	CMainFrame();
protected: 
	DECLARE_DYNAMIC(CMainFrame)

// Attributes
public:

// Operations
public:
	bool			m_showApp;
	CSystemTray m_TrayIcon;
// Overrides
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	virtual BOOL OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo);

// Implementation
public:
	virtual ~CMainFrame();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:  // control bar embedded members
	bool			m_exitApp;
	CStatusBar		m_wndStatusBar;
	CToolBar		m_wndToolBar;
	CChildView		m_wndView;

// Generated message map functions
protected:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnSetFocus(CWnd *pOldWnd);
	DECLARE_MESSAGE_MAP()
	virtual BOOL OnCommand(WPARAM wParam, LPARAM lParam);
public:
	//void OnShowApp(void);
	afx_msg void OnClose();
};


