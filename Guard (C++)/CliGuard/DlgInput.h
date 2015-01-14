#pragma once


// CDlgInput dialog

class CDlgInput : public CDialog
{
	DECLARE_DYNAMIC(CDlgInput)

public:
	CDlgInput(CWnd* pParent = NULL);   // standard constructor
	virtual ~CDlgInput();

// Dialog Data
	enum { IDD = IDD_DIALOG_INPUT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonConnect();
	afx_msg void OnBnClickedButtonSend();
	virtual BOOL OnInitDialog();
};
