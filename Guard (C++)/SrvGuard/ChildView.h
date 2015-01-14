// ChildView.h : interface of the CChildView class
//
#include "Pool.h"	// Added by ClassView
#include "ExEdit.h"	// Added by ClassView

#pragma once


// CChildView window

class CChildView : public CWnd
{
// Construction
public:
	CChildView();

// Attributes
public:
	unsigned char SwitchAx_2[15][8];
	unsigned char SwitchMn_2[15][8];
	unsigned char SwitchAx_1[15][8];
	unsigned char SwitchMn_1[15][8];
	unsigned char CouplerF2[15][8];
	unsigned char CouplerF1[15][8];
	unsigned char CouplerBs[15][8];
	int iSwitchAxFl2;
	int iSwitchMnFl2;
	int iSwitchAxFl1;
	int iSwitchMnFl1;
	int IntSleepSend;

	CString csDevNum;
	int iSwitchAcFl1;
		
	unsigned char CouplerTm[15][8];
	
	int iCouplerN;
	char strCouplerNames[5][20];
	
	bool blStop;
	bool blTestDev;
	bool blWireState;
	int port;
	int iSwitchN;
	char portname[5];
	
// Operations
public:

// Overrides
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

// Implementation
public:
	void UpdateLog(LPCTSTR lpszString);
	BOOL UpdateView();
	virtual ~CChildView();
	CExEdit editbox;
	CListBox listbox;
	CPool pool;
	
	// Generated message map functions
protected:
	void OnTimer(UINT nIDEvent);
	afx_msg void OnPaint();
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnListToggle();
	DECLARE_MESSAGE_MAP()
public:
	void Init1_Wire(void);
	void TestWire(void);
	void TestDevWire();
	void StateSwitch(unsigned char FamilySN[][8],int cnt);
	void CouplerClose(char *SerialNum);
	void Close1_Wire(void);
	void StartGuard(void);
	void StopGuard(void);
	CString DateMsg();
};

