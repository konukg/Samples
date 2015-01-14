#include "stdafx.h"

#include "sinfo.h"
#include "MainFrm.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

void sb(CString info)
{
	CMainFrame *pWnd=(CMainFrame *)AfxGetMainWnd();
	pWnd->m_wndStatusBar.SetPaneText( 2, info );
}
