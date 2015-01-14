#if !defined(AFX_CLIENTSOCKET_H__16AB8E9A_FA95_11D4_8163_817FEAD65F16__INCLUDED_)
#define AFX_CLIENTSOCKET_H__16AB8E9A_FA95_11D4_8163_817FEAD65F16__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ClientSocket.h : header file
//

class CPool;

/////////////////////////////////////////////////////////////////////////////
// CClientSocket command target

class CClientSocket : public CSocket
{
/*
		DECLARE_DYNAMIC(CClientSocket);
private:
        CClientSocket(const CClientSocket& rSrc);         // no implementation
        void operator=(const CClientSocket& rSrc);  // no implementation
*/
// Attributes
public:

// Operations
public:
	CClientSocket(CPool* ppool);
	virtual ~CClientSocket();

// Overrides
public:
	CString Name;
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CClientSocket)
	public:
	virtual void OnReceive(int nErrorCode);
	virtual void OnClose(int nErrorCode);
	//}}AFX_VIRTUAL

	// Generated message map functions
	//{{AFX_MSG(CClientSocket)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

// Implementation
protected:
	CPool* pPool;
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CLIENTSOCKET_H__16AB8E9A_FA95_11D4_8163_817FEAD65F16__INCLUDED_)
