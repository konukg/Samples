#pragma once
#include "afxwin.h"

// CSendDlg dialog

class CSendDlg : public CDialog
{
	DECLARE_DYNAMIC(CSendDlg)

public:
	CSendDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CSendDlg();

// Dialog Data
	enum { IDD = IDD_SEND_DLG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	int iSend;
	CComboBox m_cmb_cntr;
	afx_msg void OnBnClickedButtonTest();
	virtual BOOL OnInitDialog();
};
