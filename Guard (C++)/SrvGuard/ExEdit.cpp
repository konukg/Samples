// ExEdit.cpp : implementation file
//

#include "stdafx.h"
#include "SrvGuard.h"
#include "ExEdit.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CExEdit

CExEdit::CExEdit()
{
}

CExEdit::~CExEdit()
{
}


BEGIN_MESSAGE_MAP(CExEdit, CEdit)
	//{{AFX_MSG_MAP(CExEdit)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CExEdit message handlers

BOOL CExEdit::CreateEx(const RECT &rect, CWnd *pParentWnd, UINT nID)
{
	return Create( ES_MULTILINE | ES_AUTOVSCROLL| WS_CHILD | WS_VISIBLE | WS_BORDER|WS_VSCROLL,
      rect, pParentWnd, nID);
}

BOOL CExEdit::AddText(LPCTSTR lpszString)
{
	int len=GetWindowTextLength();
	SetSel(len,len);
	SendMessage(WM_CHAR,WPARAM(13),LPARAM(28));
	ReplaceSel(lpszString);
	return TRUE ;
	
}
