#if !defined(AFX_INSYS_H__80EEBD63_485B_417F_86D9_A168D7B1F998__INCLUDED_)
#define AFX_INSYS_H__80EEBD63_485B_417F_86D9_A168D7B1F998__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// InSys.h : header file
//
#include "DlgRec.h"
/////////////////////////////////////////////////////////////////////////////
// CInSys dialog

class CInSys : public CDialog
{
// Construction
public:
	bool m_bMinAuto;
	bool m_bRecIni;
	bool m_bMfnCnl;
	bool m_bDev1Chs;
	CInSys(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CInSys)
	enum { IDD = IDD_DLG_INP_SYS };
	CStatic	m_stcVolume2;
	CStatic	m_stcLevel2;
	CSliderCtrl	m_VoLevel2;
	CSliderCtrl	m_VoLevel1;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CInSys)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	virtual BOOL OnCommand(WPARAM wParam, LPARAM lParam);
	//}}AFX_VIRTUAL

// Implementation
protected:
	BOOL OnInitDialog();
	HICON m_hIcon;
	
	// Generated message map functions
	//{{AFX_MSG(CInSys)
	virtual void OnOK();
	afx_msg void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	afx_msg void OnSelendokComboDev1();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_INSYS_H__80EEBD63_485B_417F_86D9_A168D7B1F998__INCLUDED_)
