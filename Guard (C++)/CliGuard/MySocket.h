#if !defined(AFX_MYSOCKET_H__7E47728D_93F1_11D4_8163_CAFF6EB92413__INCLUDED_)
#define AFX_MYSOCKET_H__7E47728D_93F1_11D4_8163_CAFF6EB92413__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// MySocket.h : header file
//
#include "DlgMain.h"


/////////////////////////////////////////////////////////////////////////////
// MySocket command target

//class MySocket : public CAsyncSocket
class MySocket : public CSocket
{
// Attributes
public:

// Operations
public:
	MySocket();
	MySocket(CDlgMain *pDlg) {m_pDlg = pDlg;};

	virtual ~MySocket();

// Overrides
public:
	CDlgMain *m_pDlg;
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(MySocket)
	public:
	virtual void OnClose(int nErrorCode);
	virtual void OnReceive(int nErrorCode);
	//}}AFX_VIRTUAL

	// Generated message map functions
	//{{AFX_MSG(MySocket)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

// Implementation
protected:
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MYSOCKET_H__7E47728D_93F1_11D4_8163_CAFF6EB92413__INCLUDED_)
