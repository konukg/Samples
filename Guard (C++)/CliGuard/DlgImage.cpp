// DlgImage.cpp : implementation file
//

#include "stdafx.h"
#include "CliGuard.h"
#include "DlgImage.h"


// CDlgImage dialog

IMPLEMENT_DYNAMIC(CDlgImage, CDialog)
CDlgImage::CDlgImage(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgImage::IDD, pParent)
{
	m_FilePathName = "";
}

CDlgImage::~CDlgImage()
{
}

void CDlgImage::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CDlgImage, CDialog)
	ON_WM_PAINT()
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CDlgImage message handlers

void CDlgImage::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	// TODO: Add your message handler code here
	// Do not call CDialog::OnPaint() for painting messages
	if(strlen(m_FilePathName)>1)
		m_Picture.Load(m_FilePathName);	//"e:\\test.jpg");
	m_Picture.UpdateSizeOnDC(&dc);
	GetClientRect(&m_ClientRect);
	m_Picture.Show(&dc, CRect(0,0, m_ClientRect.right, m_ClientRect.bottom));
	//m_Picture.Show(&dc, CRect(0,0,400,300));
	blPlay = false;
}

void CDlgImage::OnTimer(UINT nIDEvent)
{
	switch (nIDEvent)
	{
	case 1:
		sndPlaySound("ringout.wav", SND_ASYNC);
	break;
	}
}

void CDlgImage::StartPlaySound(void)
{
	SetTimer(1, 1000, 0);
	blPlay = true;
}

void CDlgImage::StopPlaySound(void)
{
	KillTimer(1);
	blPlay = false;
}
