#if !defined(AFX_LISTENINGSOCKET_H__16AB8E99_FA95_11D4_8163_817FEAD65F16__INCLUDED_)
#define AFX_LISTENINGSOCKET_H__16AB8E99_FA95_11D4_8163_817FEAD65F16__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ListeningSocket.h : header file
//



/////////////////////////////////////////////////////////////////////////////
// CListeningSocket command target

class CPool;

class CListeningSocket : public CSocket
{
// Attributes
public:

// Operations
public:
	CListeningSocket(CPool* ppool);
	virtual ~CListeningSocket();

// Overrides
public:
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CListeningSocket)
	public:
	virtual void OnAccept(int nErrorCode);
	//}}AFX_VIRTUAL

	// Generated message map functions
	//{{AFX_MSG(CListeningSocket)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

// Implementation
protected:
	CPool* pPool;

};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_LISTENINGSOCKET_H__16AB8E99_FA95_11D4_8163_817FEAD65F16__INCLUDED_)
