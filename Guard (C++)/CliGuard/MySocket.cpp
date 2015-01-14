// MySocket.cpp : implementation file
//

#include "stdafx.h"
//#include "s_client.h"
#include "MySocket.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// MySocket

MySocket::MySocket()
{
}

MySocket::~MySocket()
{
}


// Do not edit the following lines, which are needed by ClassWizard.
#if 0
BEGIN_MESSAGE_MAP(MySocket, CAsyncSocket)
	//{{AFX_MSG_MAP(MySocket)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
#endif	// 0

/////////////////////////////////////////////////////////////////////////////
// MySocket member functions

void MySocket::OnClose(int nErrorCode) 
{
    //m_pDlg->m_cConnect->EnableWindow(TRUE);
	m_pDlg->blConnect = false;
	m_pDlg->OnBnClickedButtonStop();
	AfxMessageBox("Потерена связь с сервером охраны!");
    //delete this;
	CSocket::OnClose(nErrorCode);
}

void MySocket::OnReceive(int nErrorCode) 
{
	char st[4096];
	bool blClear = true;
	
	int r=Receive(st,4096);
	st[r]='\0';
	//int result = strcmp( st, "clear" );
	if( strcmp( st, "clear#" ) == 0 )
	{
		m_pDlg->SendDlgItemMessage(IDC_LIST_DEV,LB_RESETCONTENT ,0,0);
		blClear = false;
	}
	char *token;
	char seps[]   = "#";
	token = strtok( st, seps );
	while( token != NULL )
	{
		
		if(blClear)
			m_pDlg->AddText(token);		// remov "clear" from List
		token = strtok( NULL, seps );
	}

	CSocket::OnReceive(nErrorCode);
}
