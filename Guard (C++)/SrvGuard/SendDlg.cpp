// SendDlg.cpp : implementation file
//

#include "stdafx.h"
#include "SrvGuard.h"
#include "SendDlg.h"


// CSendDlg dialog

IMPLEMENT_DYNAMIC(CSendDlg, CDialog)
CSendDlg::CSendDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSendDlg::IDD, pParent)
{
	iSend = 0;
}

CSendDlg::~CSendDlg()
{
}

void CSendDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO1, m_cmb_cntr);
}


BEGIN_MESSAGE_MAP(CSendDlg, CDialog)
	ON_BN_CLICKED(IDC_BUTTON_TEST, OnBnClickedButtonTest)
END_MESSAGE_MAP()


// CSendDlg message handlers

void CSendDlg::OnBnClickedButtonTest()
{
	//m_cmb_cntr->AddString("11111");
}

BOOL CSendDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  Add extra initialization here
	int rtr;
	rtr = iSend;
	m_cmb_cntr.AddString("11111");

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}
