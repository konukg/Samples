// Pool.h: interface for the CPool class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_POOL_H__16AB8E98_FA95_11D4_8163_817FEAD65F16__INCLUDED_)
#define AFX_POOL_H__16AB8E98_FA95_11D4_8163_817FEAD65F16__INCLUDED_

//#include "ClientSocket.h"	// Added by ClassView


#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CChildView;
class CListeningSocket;
class CClientSocket;

class CPool  
{
public:
	CPool();
	virtual ~CPool();
	BOOL Init(void *phost,int port);
	BOOL Send2All(CString data);
	BOOL Send(int index,CString data);
	void ProcessClose(CClientSocket *pSocket);
	void ProcessRead(CClientSocket* pSocket,CString msg);
	int ProcessAccept();
	CPtrList connectionList;
	CString DateMsg();

protected:
	CListeningSocket *m_pSocket;
	CChildView * pHost;
	//void* pHost;
};

#endif // !defined(AFX_POOL_H__16AB8E98_FA95_11D4_8163_817FEAD65F16__INCLUDED_)
