// GdkSmdr.h : main header file for the GdkSmdr application
//
#pragma once

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols
#include "DlgMain.h"
#include "MainFrm.h"

// CGdkSmdrApp:
// See GdkSmdr.cpp for the implementation of this class
//

class CGdkSmdrApp : public CWinApp
{
public:
	CMainFrame* pFrame;
	bool			m_showApp1;
	CDlgMain* m_pDlgMain;

	CGdkSmdrApp();


// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

public:
	void OnShowApp(void);
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
	virtual BOOL OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo);
};

extern CGdkSmdrApp theApp;