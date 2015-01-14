// ClientSocket.cpp : implementation file
//

#include "stdafx.h"
#include "SrvGuard.h"
#include "ClientSocket.h"
#include "Pool.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CClientSocket

CClientSocket::CClientSocket(CPool* ppool)
{
	pPool=ppool;
}

CClientSocket::~CClientSocket()
{
}


// Do not edit the following lines, which are needed by ClassWizard.
#if 0
BEGIN_MESSAGE_MAP(CClientSocket, CSocket)
	//{{AFX_MSG_MAP(CClientSocket)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
#endif	// 0

/////////////////////////////////////////////////////////////////////////////
// CClientSocket member functions

void CClientSocket::OnReceive(int nErrorCode) 
{

    char buff[4096];

	static CString message;
    int nRead;
    nRead = Receive(buff, 4096);
    switch (nRead)
    {
        case 0:
			Close();
			break;
        case SOCKET_ERROR:
			//AfxMessageBox ("Error occurred");
			//Close();
         break;
         default:
			buff[nRead] = 0; 
			message=buff;
			if(Name.GetLength()==0)
			{
				Name=buff;
				Send("Connection Ok!",14);
			}
			pPool->ProcessRead(this,message);
			//if (buffer.CompareNoCase("bye") == 0 ) ShutDown();
	}
	CSocket::OnReceive(nErrorCode);
}

void CClientSocket::OnClose(int nErrorCode) 
{
	pPool->ProcessClose(this);
	CSocket::OnClose(nErrorCode);
}


