// DlgInput.cpp : implementation file
//

#include "stdafx.h"
#include "CliGuard.h"
#include "DlgInput.h"
#include ".\dlginput.h"


// CDlgInput dialog

IMPLEMENT_DYNAMIC(CDlgInput, CDialog)
CDlgInput::CDlgInput(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgInput::IDD, pParent)
{
}

CDlgInput::~CDlgInput()
{
}

void CDlgInput::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CDlgInput, CDialog)
	ON_BN_CLICKED(IDC_BUTTON_CONNECT, OnBnClickedButtonConnect)
	ON_BN_CLICKED(IDC_BUTTON_SEND, OnBnClickedButtonSend)
END_MESSAGE_MAP()


// CDlgInput message handlers

BOOL CDlgInput::OnInitDialog()
{
	CDialog::OnInitDialog();

	char buffer[4096];
	GetPrivateProfileString("Params", "Name", NULL,
            buffer, sizeof(buffer), "cliguard.ini");
	SetDlgItemText(IDC_EDIT_NAME,buffer);
	
	GetPrivateProfileString("Params", "IP-adress", NULL,
            buffer, sizeof(buffer), "cliguard.ini"); 
	SetDlgItemText(IDC_EDIT_IP,buffer);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

void CDlgInput::OnBnClickedButtonConnect()
{
	//ConnectToServer();
}

void CDlgInput::OnBnClickedButtonSend()
{
	/*CString m_sMessage;

	blTest = true;
	GetDlgItemText(IDC_EDIT_MSG,m_sMessage);
	if(pSocket!=NULL&&m_sMessage.GetLength()!=0)
		pSocket->Send(m_sMessage,80);*/
}


