// RecTalk.h : main header file for the RECTALK application
//

#if !defined(AFX_RECTALK_H__3E7272DC_C536_42AB_A9BC_5BF1D0F62D61__INCLUDED_)
#define AFX_RECTALK_H__3E7272DC_C536_42AB_A9BC_5BF1D0F62D61__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols
#include "RecSound.h"
#include "FileAcm.h"
#include "FileAcmT.h"
#include "DlgRec.h"

#define WM_RECORDSOUND_STARTRECORDING WM_USER+500
#define WM_RECORDSOUND_WRITETHREAD WM_USER+501
#define WM_RECORDSOUND_STARTWRITE WM_USER+503
#define WM_RECORDSOUND_STOPWRITE WM_USER+504
#define WM_RECORDSOUND_WRITEFILE1 WM_USER+502
#define WM_RECORDSOUND_WRITEFILE2 WM_USER+505
#define WM_RECORDSOUND_CLOSEREC WM_USER+506
/////////////////////////////////////////////////////////////////////////////
// CRecTalkApp:
// See RecTalk.cpp for the implementation of this class
//

class CRecTalkApp : public CWinApp
{
public:
	CRecTalkApp();
	
// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CRecTalkApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

public:
	
	void OnAcmSel();
	void OnInputSys();
	bool InitFileAcm();
	bool InitFileAcmT();
	//{{AFX_MSG(CRecTalkApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
protected:
	CDlgRec* m_pDialog;
	void OnExitRec();
	bool InitRecord();
	CRecSound* m_pRecSound;
	CFileAcm* m_pFileAcm;
	CFileAcmT* m_pFileAcmT;
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_RECTALK_H__3E7272DC_C536_42AB_A9BC_5BF1D0F62D61__INCLUDED_)
