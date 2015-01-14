// SrvGuard.h : main header file for the SrvGuard application
//
#pragma once

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols


// CSrvGuardApp:
// See SrvGuard.cpp for the implementation of this class
//
#define PORT1 2049

class CSrvGuardApp : public CWinApp
{
public:
	CSrvGuardApp();


// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CSrvGuardApp theApp;