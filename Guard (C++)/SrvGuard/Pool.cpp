// Pool.cpp: implementation of the CPool class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "SrvGuard.h"
#include "Pool.h"
#include "ChildView.h"
#include "ClientSocket.h"
#include "ListeningSocket.h"
#include "sinfo.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CPool::CPool()
{
	m_pSocket = NULL;
}

CPool::~CPool()
{
	while(!connectionList.IsEmpty())
	{
        CClientSocket* pSocket = (CClientSocket*)connectionList.RemoveHead();
		if (pSocket!=NULL)
		{
			pSocket->Close();
			delete pSocket;
		}
	}
	m_pSocket->Close();
	delete m_pSocket;
}

int CPool::ProcessAccept()
{
	CClientSocket* pSocket = new CClientSocket(this);
    if (m_pSocket->Accept(*pSocket))
    {
        connectionList.AddTail(pSocket);
		sb("ProcessAccept");
		pHost->UpdateView();
    }
    else
		delete pSocket;

	return 0;
}

BOOL CPool::Init(void *phost,int port)
{
	pHost=(CChildView*)phost;
	m_pSocket = new CListeningSocket(this);
    if (m_pSocket->Create(port))
    {
		if(m_pSocket->Listen())
			return TRUE;
    }
    return FALSE;
}

void CPool::ProcessRead(CClientSocket *pSocket,CString msg)
{
	CString msgDate;

	msgDate = msg + DateMsg();
	sb(msg);
	pHost->UpdateView();
	pHost->editbox.AddText(pSocket->Name+": "+msgDate);

	char strOpen97g[]="open";
	char strClose97g[]="close";
	char strStartGuard[]="start";
	char strStopGuard[]="stop";
	char strTestGuard[]="test";
	int result;
	
	result = strcmp( strOpen97g, msg );
	if( result == 0 )
		pHost->Init1_Wire();

	result = strcmp( strTestGuard, msg );
	if( result == 0 )
	{
		if(pHost->blStop)
		{
			if(!pHost->blWireState)
				pHost->Init1_Wire();
			Sleep(150);
			pHost->blTestDev = true;
			pHost->TestWire();
			Sleep(250);
			if(pHost->blWireState)
				pHost->Close1_Wire();
			pHost->blTestDev = false;
		}
	}

	result = strcmp( strClose97g, msg );
	if( result == 0 )
		pHost->Close1_Wire();
		
	result = strcmp( strStartGuard, msg );
	if( result == 0 )
	{
		if(pHost->blStop)
		{
			pHost->StartGuard();
		}
	}

	result = strcmp( strStopGuard, msg );
	if( result == 0 )
	{
		//int cnt = connectionList.GetCount();
        if(!pHost->blStop && connectionList.GetCount() < 2)
		{
			pHost->StopGuard();
		}
	}
}

void CPool::ProcessClose(CClientSocket *pSocket)
{
    POSITION pos = connectionList.Find(pSocket);
    connectionList.RemoveAt(pos);
	delete pSocket;
	pHost->UpdateView();
    sb("Disconnecting");
}

BOOL CPool::Send(int index, CString data)
{
	if(data.GetLength()==0)
		return FALSE;
	CClientSocket *pSock;
	POSITION pos=connectionList.FindIndex(index);
	pSock=(CClientSocket*)connectionList.GetAt(pos);
	pSock->Send(data,data.GetLength());
	return TRUE;
}

BOOL CPool::Send2All(CString data)
{
	if(data.GetLength()==0)
		return FALSE;
	for(POSITION pos = connectionList.GetHeadPosition(); pos != NULL;)
	{
        CClientSocket* pSock = (CClientSocket*)connectionList.GetNext(pos);
		pSock->Send(data,data.GetLength());
	}
	return TRUE;
}

CString CPool::DateMsg()
{
	CString date;
	CTime t = CTime::GetCurrentTime();
	date.Format(" дата:%d.%02d.%02d время:%02d.%02d.%02d",	t.GetYear(),
				 											t.GetMonth(),
															t.GetDay(),
															t.GetHour(),
															t.GetMinute(),
															t.GetSecond());
	return date;
}