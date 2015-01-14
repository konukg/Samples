#if !defined(AFX_DLGREC_H__40EC75D3_D04D_4E42_842B_5E303DD8CA29__INCLUDED_)
#define AFX_DLGREC_H__40EC75D3_D04D_4E42_842B_5E303DD8CA29__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// DlgRec.h : header file
//
#include "RecSound.h"
/////////////////////////////////////////////////////////////////////////////
// CDlgRec dialog

class CDlgRec : public CDialog
{
// Construction
public:
	int m_iRestart;
	int m_iAutoRec;
	ACMFORMATCHOOSE m_afc;
	CDlgRec(CWnd* pParent = NULL);   // standard constructor
	
// Dialog Data
	//{{AFX_DATA(CDlgRec)
	enum { IDD = IDD_DIALOG_REC };
	CButton	m_buttonStop2;
	CButton	m_buttonRec2;
	CButton	m_buttonStop1;
	CButton	m_buttonRec1;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDlgRec)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	void OnTimer(UINT nIDEvent);
	BOOL OnInitDialog();
	//CRecThr* m_pRecord;
	// Generated message map functions
	//{{AFX_MSG(CDlgRec)
	afx_msg void OnButtonRec1();
	afx_msg void OnButtonStop1();
	afx_msg void OnButtonRec2();
	afx_msg void OnButtonStop2();
	afx_msg void OnButtonTest();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

public:
	LRESULT OnStopRecording(WPARAM wParam, LPARAM lParam);
	CRecSound* m_RecordThread;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DLGREC_H__40EC75D3_D04D_4E42_842B_5E303DD8CA29__INCLUDED_)
