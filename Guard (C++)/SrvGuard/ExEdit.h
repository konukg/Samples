#if !defined(AFX_EXEDIT_H__AAC3C040_FD30_11D4_8163_A938CBD13616__INCLUDED_)
#define AFX_EXEDIT_H__AAC3C040_FD30_11D4_8163_A938CBD13616__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ExEdit.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CExEdit window

class CExEdit : public CEdit
{
// Construction
public:
	CExEdit();

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CExEdit)
	//}}AFX_VIRTUAL

// Implementation
public:
	BOOL AddText(LPCTSTR lpszString );
	BOOL CreateEx(const RECT& rect, CWnd* pParentWnd, UINT nID);
	virtual ~CExEdit();

	// Generated message map functions
protected:
	//{{AFX_MSG(CExEdit)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_EXEDIT_H__AAC3C040_FD30_11D4_8163_A938CBD13616__INCLUDED_)
