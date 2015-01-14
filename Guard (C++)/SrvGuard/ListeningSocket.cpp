// ListeningSocket.cpp : implementation file
//

#include "stdafx.h"
#include "SrvGuard.h"
#include "ListeningSocket.h"
#include "Pool.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CListeningSocket

CListeningSocket::CListeningSocket(CPool* ppool)
{
	pPool=ppool;
}

CListeningSocket::~CListeningSocket()
{
}


// Do not edit the following lines, which are needed by ClassWizard.
#if 0
BEGIN_MESSAGE_MAP(CListeningSocket, CSocket)
	//{{AFX_MSG_MAP(CListeningSocket)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
#endif	// 0

/////////////////////////////////////////////////////////////////////////////
// CListeningSocket member functions

void CListeningSocket::OnAccept(int nErrorCode) 
{
	// TODO: Add your specialized code here and/or call the base class
	pPool->ProcessAccept();
	CSocket::OnAccept(nErrorCode);
}
