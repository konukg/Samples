#pragma once

#include "resource.h"
#include "afxwin.h"
#include "DlgImage.h"

//#import "msado15.dll" rename_namespace("ADOCG") rename("EOF", "EndOfFile")
//using namespace ADOCG;
#import "msado15.dll" \
    no_namespace rename("EOF", "EndOfFile")

#include "icrsint.h"
inline void TESTHR(HRESULT x) {if FAILED(x) _com_issue_error(x);};

class CCustomRs : 
	public CADORecordBinding
{
BEGIN_ADO_BINDING(CCustomRs)
	ADO_FIXED_LENGTH_ENTRY(1, adSmallInt, m_sSwitID, lSwitIDStatus, FALSE)
	ADO_FIXED_LENGTH_ENTRY(2, adSmallInt, m_sCoupID, lCoupIDStatus, FALSE)
	ADO_FIXED_LENGTH_ENTRY(3, adBoolean, m_fSwitState, lSwitStateStatus, FALSE)
	ADO_VARIABLE_LENGTH_ENTRY2(4, adVarWChar, m_wszSwitName, sizeof(m_wszSwitName), lSwitNameStatus, FALSE)
	ADO_VARIABLE_LENGTH_ENTRY2(5, adVarWChar, m_wszSwitPlace, sizeof(m_wszSwitPlace), lSwitPlaceStatus, FALSE)
	ADO_VARIABLE_LENGTH_ENTRY2(6, adVarWChar, m_wszSwitImg, sizeof(m_wszSwitImg), lSwitImgStatus, FALSE)
	ADO_VARIABLE_LENGTH_ENTRY2(7, adVarWChar, m_wszSwitNote, sizeof(m_wszSwitNote), lSwitNoteStatus, FALSE)
END_ADO_BINDING()

protected:
	SHORT m_sSwitID;
	ULONG lSwitIDStatus;
	SHORT m_sCoupID;
	ULONG lCoupIDStatus;
	VARIANT_BOOL m_fSwitState;
	ULONG lSwitStateStatus;
	WCHAR m_wszSwitName[21];
	ULONG lSwitNameStatus;
	WCHAR m_wszSwitPlace[51];
	ULONG lSwitPlaceStatus;
	WCHAR m_wszSwitImg[51];
	ULONG lSwitImgStatus;
	WCHAR m_wszSwitNote[51];
	ULONG lSwitNoteStatus;
};
// CDlgMain dialog
class MySocket;
class CDlgMain : public CDialog, public CCustomRs
{
	DECLARE_DYNAMIC(CDlgMain)

public:
	CDlgImage* m_pDlgImage;
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
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	int m_iCntRec;
	int iSwCntDev;
	int iTestCntDev;
	bool blTest;
	bool blSensorFill;
	bool blStart;
	bool blConnect;
	CString	Name;
	CString	ip;
	MySocket *pSocket;
	
	virtual BOOL OnInitDialog();
	
	SHORT m_sDlgSwitID;
	SHORT m_sDlgCoupID;
	BOOL m_fDlgSwitState;
	CString m_strDlgSwitName;
	CString m_strDlgSwitPlace;
	CString m_strDlgSwitImg;
	CString m_strDlgSwitNote;
	
	afx_msg void OnBnClickedButtonStart();
	afx_msg void OnBnClickedButtonStop();
	void AddText(LPCTSTR lpszString);
	void RecReport(LPCTSTR lpszStrMsg, LPCTSTR lpszStrNote);
	afx_msg void OnBnClickedButtonClear();

	CButton m_cClkear;
	CButton m_cStart;
	CButton m_cStop;
	
	void ConnectToServer(void);
	afx_msg void OnLbnSelchangeListDev();
	void OpenRecordSet(void);
	void ObjectRecordSet(void);
	void ObjectFill(void);
	void ObjectSelChange(void);
	void SensorFill(int iFloorNumber);
};
