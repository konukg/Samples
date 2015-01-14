#pragma once

#include "SerialPort.h"
#include "afxwin.h"
// CDlgMain dialog

//#import "D:\Program Files\Common Files\system\ado\msado15.dll" rename_namespace("ADOCG") rename("EOF", "EndOfFile")
//using namespace ADOCG;
#import "msado15.dll" \
    no_namespace rename("EOF", "EndOfFile")

#include "icrsint.h"
//#include "afxcmn.h"

inline void TESTHR(HRESULT x) {if FAILED(x) _com_issue_error(x);};

class CCustomRs : 
	public CADORecordBinding
{
BEGIN_ADO_BINDING(CCustomRs)
	ADO_FIXED_LENGTH_ENTRY(1, adInteger, m_lRprId, lRprIdStatus, FALSE)
	ADO_VARIABLE_LENGTH_ENTRY2(2, adVarWChar, m_wszRprNuber, sizeof(m_wszRprNuber), lRprNuberStatus, FALSE)
	ADO_VARIABLE_LENGTH_ENTRY2(3, adVarWChar, m_wszRprLine, sizeof(m_wszRprLine), lRprLineStatus, FALSE)
	ADO_FIXED_LENGTH_ENTRY(4, adDate, m_dtRprTimeLen, lRprTimeLenStatus, FALSE)
	ADO_FIXED_LENGTH_ENTRY(5, adDate, m_dtRprDate, lRprDateStatus, FALSE)
	ADO_FIXED_LENGTH_ENTRY(6, adDate, m_dtRprTimeCall, lRprTimeCallStatus, FALSE)
	ADO_VARIABLE_LENGTH_ENTRY2(7, adVarWChar, m_wszRprNmRing, sizeof(m_wszRprNmRing), lRprNmRingStatus, FALSE)
	ADO_VARIABLE_LENGTH_ENTRY2(8, adVarWChar, m_wszRprTypeRing, sizeof(m_wszRprTypeRing), lRprTypeRingStatus, FALSE)
END_ADO_BINDING()

protected:
	LONG m_lRprId;
	ULONG lRprIdStatus;
	WCHAR m_wszRprNuber[11];
	ULONG lRprNuberStatus;
	WCHAR m_wszRprLine[11];
	ULONG lRprLineStatus;
	DATE m_dtRprTimeLen;
	ULONG lRprTimeLenStatus;
	DATE m_dtRprDate;
	ULONG lRprDateStatus;
	DATE m_dtRprTimeCall;
	ULONG lRprTimeCallStatus;
	WCHAR m_wszRprNmRing[21];
	ULONG lRprNmRingStatus;
	WCHAR m_wszRprTypeRing[51];
	ULONG lRprTypeRingStatus;
};

class CDlgMain : public CDialog, public CCustomRs
{
	DECLARE_DYNAMIC(CDlgMain)

public:
	bool	m_blRegList;
	long	m_iCntRec;

	LONG m_lDlgRprId;
	CString m_strDlgRprNuber;
	CString m_strDlgRprLine;
	COleDateTime m_oledtDlgRprTimeLen;
	COleDateTime m_oledtDlgRprDate;
	COleDateTime m_oledtDlgRprTimeCall;
	CString m_strDlgRprNmRing;
	CString m_strDlgRprTypeRing;

	void ListPhraseSend(CString strSnd[8]);
	void PhraseToken(CString strSnd);
	void InitCommPort();

	CDlgMain(CWnd* pParent = NULL);   // standard constructor
	virtual ~CDlgMain();

// Dialog Data
	enum { IDD = IDD_DIALOG_MAIN };

protected:
	void RefreshBoundData();
	void GenerateError(HRESULT hr, PWSTR pwszDescription);

	_RecordsetPtr m_pRs;
	_RecordsetPtr m_pRsFind;
	_RecordsetPtr m_pRsObj;
	_ConnectionPtr m_pConn;

	CString m_strConnection;
	CString m_strCmdText;

	CListBox		m_ListBox1;
	CEdit			m_Edit1;
	CSerialPort		m_Ports[4];
	CString			m_strReceived[4];
	
	afx_msg LONG OnCommunication(UINT, LONG);
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	void OpenRecordSet();
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedButtonTest();
	CListCtrl m_cListCtrl;

	afx_msg void OnBnClickedButtonStart();
	afx_msg void OnBnClickedButtonStop();
	afx_msg void OnBnClickedButtonClear();
};
