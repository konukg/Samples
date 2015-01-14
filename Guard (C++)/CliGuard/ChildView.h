// ChildView.h : interface of the CChildView class
//
#include "DlgMain.h"

#pragma once


// CChildView window

class CChildView : public CWnd
{
// Construction
public:
	CChildView();

	CDlgMain* m_pDlgMain;
// Attributes
public:

// Operations
public:

// Overrides
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

// Implementation
public:
	virtual ~CChildView();

	// Generated message map functions
protected:
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()
};

