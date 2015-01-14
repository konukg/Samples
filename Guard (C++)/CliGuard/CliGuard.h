// CliGuard.h : main header file for the CliGuard application
//
#include "DlgMain.h"
#include "DlgInput.h"

#pragma once

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols


// CCliGuardApp:
// See CliGuard.cpp for the implementation of this class
//

class CCliGuardApp : public CWinApp
{
public:
	CCliGuardApp();


// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

public:
	CDlgMain* m_pDlgMain;
	CDlgInput m_pDlgInput;
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
	afx_msg void OnFloor();
	afx_msg void OnSensor2();
	afx_msg void OnSensor3();
	afx_msg void OnSetparam();
	virtual CWnd* GetMainWnd();
	virtual BOOL OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo);
};

extern CCliGuardApp theApp;