// DlgMain.cpp : implementation file
//

#include "stdafx.h"
#include "GdkSmdr.h"
#include "DlgMain.h"
#include ".\dlgmain.h"


// CDlgMain dialog

IMPLEMENT_DYNAMIC(CDlgMain, CDialog)
CDlgMain::CDlgMain(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgMain::IDD, pParent)
{
	//m_strConnection = _T("Provider=MSDASQL.1;Persist Security Info=False;Data Source=GdkSmdrMdb");
	//m_strCmdText = _T("Report");
	m_strConnection = _T("DSN=GdkSmdrMdb;UID=MyUserId;PWD=MyPassword;");
	
	m_strCmdText = _T("SELECT * FROM Report ORDER BY RprId");

	m_pRs = NULL;
	m_iCntRec = 0;	
	m_lDlgRprId = 0;
	m_strDlgRprNuber = _T("");
	m_strDlgRprLine = _T("");
	m_oledtDlgRprTimeLen = 0L;
	m_oledtDlgRprDate = 0L;
	m_oledtDlgRprTimeCall = 0L;
	m_strDlgRprNmRing = _T("");
	m_strDlgRprTypeRing = _T("");

	m_blRegList = false;
}

CDlgMain::~CDlgMain()
{
	m_pRs = NULL;
}

void CDlgMain::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST2, m_cListCtrl);
}


BEGIN_MESSAGE_MAP(CDlgMain, CDialog)
	ON_MESSAGE(WM_COMM_RXCHAR, OnCommunication)
	ON_BN_CLICKED(IDC_BUTTON_TEST, OnBnClickedButtonTest)
//	ON_WM_SHOWWINDOW()
//ON_WM_ACTIVATE()
//ON_WM_ACTIVATEAPP()
ON_BN_CLICKED(IDC_BUTTON_START, OnBnClickedButtonStart)
ON_BN_CLICKED(IDC_BUTTON_STOP, OnBnClickedButtonStop)
ON_BN_CLICKED(IDC_BUTTON_CLEAR, OnBnClickedButtonClear)
END_MESSAGE_MAP()


// CDlgMain message handlers

BOOL CDlgMain::OnInitDialog()
{
	
	CDialog::OnInitDialog();

	m_ListBox1.SubclassDlgItem(IDC_LIST1, this);
	m_Edit1.SubclassDlgItem(IDC_EDIT1, this);

	m_cListCtrl.InsertColumn(0, _T("п/п №"), LVCFMT_LEFT, 50);
	m_cListCtrl.InsertColumn(1, _T("№ тел."), LVCFMT_LEFT, 55);
	m_cListCtrl.InsertColumn(2, _T("Линия"), LVCFMT_LEFT, 50);
	m_cListCtrl.InsertColumn(3, _T("Тариф"), LVCFMT_LEFT, 50);
	m_cListCtrl.InsertColumn(4, _T("Дата"), LVCFMT_LEFT, 60);
	m_cListCtrl.InsertColumn(5, _T("Время"), LVCFMT_LEFT, 50);
	m_cListCtrl.InsertColumn(6, _T("Звонок"), LVCFMT_LEFT, 100);
	m_cListCtrl.InsertColumn(7, _T("Тип"), LVCFMT_LEFT, 50);
	// init the ports
	
	InitCommPort();
	OpenRecordSet();
	
	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

void CDlgMain::InitCommPort()
{
	int i = 0;
	
	if (m_Ports[i].InitPort(this, i + 1))
	{
		m_Edit1.SetWindowText("№1");
		m_Ports[i].StartMonitoring();
	}
	else
	{
		// port not found
		m_Edit1.SetWindowText("Нет");
		//m_ListBox1.EnableWindow(FALSE);
	}
}

LONG CDlgMain::OnCommunication(WPARAM ch, LPARAM port)
{
	if (port <= 0 || port > 4)
		return -1;

	if (ch == 13)
	{
		CString stTemp, stSn;
		stTemp.Format("%s", m_strReceived[port-1]);
		stSn = stTemp.Left(stTemp.GetLength());
		int iRecN = atoi(stSn);	//m_ListBox1.AddString(stSn);
		if(iRecN > 0)
			PhraseToken(stSn);
		//m_ListBox1.SetSel(m_ListBox1.GetCount()-1, TRUE);

		(m_strReceived[port-1]).Empty();
	}
	else
	{
		if(ch != 10)
			m_strReceived[port-1] += (char)ch;
	}

	return 0;
}

void CDlgMain::PhraseToken(CString strSnd)
{
	char buf[200];
	char *token;
	char seps[] = " ";
	CString	strListToken[8], strTemp;

	strcpy(buf, strSnd);
	token = strtok( buf, seps );
	int i = 0;
	bool blTkn = false;
	while( i < 7)	//token != NULL )
	{
		/* While there are tokens in "string" */
		//m_ListBox1.AddString(token);
		//strcpy(strListToken[i], token);
		if(token == NULL )
		{
			blTkn = true;
			break;
		}
		strListToken[i].Format(token);
		i++;
		/* Get next token: */
		token = strtok( NULL, seps );
	}

	if(!blTkn)
	{
		strListToken[4] = strListToken[4] + " " + strListToken[5];
		strListToken[7] = strListToken[6].Left(1);
		strTemp = strListToken[6].Mid(1, strListToken[6].GetLength());
		strListToken[6] = strTemp;

		int iRecN = atoi(strListToken[0]);
		if(iRecN > 0 && iRecN < 10000)
		{
			if(m_blRegList)
				ListPhraseSend(strListToken);

			bstr_t strSQL;
			CString s1,s2,sSum;
			
			s1 = "INSERT INTO Report (RprId,RprNumber,RprLine,RprTimeLen,RprDate,RprNmRing,RprTypeRing) VALUES ('";
			m_iCntRec++;
			s2.Format("%d", m_iCntRec);
			sSum = s1 + s2 + "','" + strListToken[1] + "','" + strListToken[2] + "','" + strListToken[3] + "','" 
				+ strListToken[4] + "','" + strListToken[6] + "','" + strListToken[7] + "')";	//+ strListToken[4] + "','" + strListToken[5] + "','" + strListToken[6] + "','" + strListToken[7] + "')";
			strSQL = sSum;
			m_pConn->Execute(strSQL, NULL, adExecuteNoRecords);
		}
	}
}

void CDlgMain::ListPhraseSend(CString strSnd[8])
{
	LV_ITEM lvi;
	TCHAR szItem[256];
	lvi.mask = LVIF_TEXT;

	int i = 0;
	lvi.iItem = i;
	lvi.iSubItem = 0;
	strcpy(szItem, strSnd[0]);
	lvi.pszText = szItem;
	m_cListCtrl.InsertItem(&lvi);

	lvi.iSubItem = 1;
	strcpy(szItem, strSnd[1]);
	lvi.pszText = szItem;
	m_cListCtrl.SetItem(&lvi);

	lvi.iSubItem = 2;
	strcpy(szItem, strSnd[2]);
	lvi.pszText = szItem;
	m_cListCtrl.SetItem(&lvi);

	lvi.iSubItem = 3;
	strcpy(szItem, strSnd[3]);
	lvi.pszText = szItem;
	m_cListCtrl.SetItem(&lvi);
	
	lvi.iSubItem = 4;
	strcpy(szItem, strSnd[4]);
	lvi.pszText = szItem;
	m_cListCtrl.SetItem(&lvi);

	lvi.iSubItem = 5;
	strcpy(szItem, strSnd[5]);
	lvi.pszText = szItem;
	m_cListCtrl.SetItem(&lvi);

	lvi.iSubItem = 6;
	strcpy(szItem, strSnd[6]);
	lvi.pszText = szItem;
	m_cListCtrl.SetItem(&lvi);

	lvi.iSubItem = 7;
	strcpy(szItem, strSnd[7]);
	lvi.pszText = szItem;
	m_cListCtrl.SetItem(&lvi);
}

void CDlgMain::OnBnClickedButtonTest()
{
	/*CString strToken;

	strToken.Format("4386  230 17    00:19 05/18/05 16:25 O230183            **");
	PhraseToken(strToken);*/
	
	//AfxGetApp()
	
}

void CDlgMain::OpenRecordSet()
{
	HRESULT hr = NOERROR;
	IADORecordBinding *piAdoRecordBinding = NULL;
	_bstr_t bstrName;
		
	try
	{
		bstr_t strCnn;
		strCnn = m_strConnection;
		TESTHR(m_pConn.CreateInstance(__uuidof(Connection)));
		m_pConn->Open (strCnn, "", "", adConnectUnspecified);
		
		TESTHR(m_pRs.CreateInstance(__uuidof(Recordset)));
		m_pRs->CursorLocation = adUseClient;
		m_pRs->Open((LPCTSTR)m_strCmdText, _variant_t((IDispatch *) m_pConn, true), 
					adOpenStatic, adLockOptimistic, adCmdText);
		
		TESTHR(m_pRs->QueryInterface(__uuidof(IADORecordBinding), (LPVOID *)&piAdoRecordBinding));
		//TESTHR(piAdoRecordBinding->BindToRecordset(this));

		RefreshBoundData();
		if(m_pRs->GetRecordCount() > 0)
		{
			m_pRs->MoveLast();
			bstrName = m_pRs->Fields->Item["RprId"]->Value;
			m_iCntRec = atol(bstrName);
		}else m_iCntRec = 0;
	}
	catch (_com_error &e)
	{
		GenerateError(e.Error(), e.Description());
	}

	if (piAdoRecordBinding)
		piAdoRecordBinding->Release();
}

void CDlgMain::RefreshBoundData()
{
	if (adFldOK == lRprIdStatus)
		m_lDlgRprId = m_lRprId;
	else
		m_lDlgRprId = 0;
	if (adFldOK == lRprNuberStatus)
		m_strDlgRprNuber = m_wszRprNuber;
	else
		m_strDlgRprNuber = _T("");
	if (adFldOK == lRprLineStatus)
		m_strDlgRprLine = m_wszRprLine;
	else
		m_strDlgRprLine = _T("");
	if (adFldOK == lRprTimeLenStatus)
		m_oledtDlgRprTimeLen = m_dtRprTimeLen;
	else
		m_oledtDlgRprTimeLen = 0L;
	if (adFldOK == lRprDateStatus)
		m_oledtDlgRprDate = m_dtRprDate;
	else
		m_oledtDlgRprDate = 0L;
	if (adFldOK == lRprTimeCallStatus)
		m_oledtDlgRprTimeCall = m_dtRprTimeCall;
	else
		m_oledtDlgRprTimeCall = 0L;
	if (adFldOK == lRprNmRingStatus)
		m_strDlgRprNmRing = m_wszRprNmRing;
	else
		m_strDlgRprNmRing = _T("");
	if (adFldOK == lRprTypeRingStatus)
		m_strDlgRprTypeRing = m_wszRprTypeRing;
	else
		m_strDlgRprTypeRing = _T("");

	UpdateData(FALSE);
}

void CDlgMain::GenerateError(HRESULT hr, PWSTR pwszDescription)
{
	CString strError;

	strError.Format("Run-time error '%d (%x)'", hr, hr);
	strError += "\n\n";
	strError += pwszDescription;

	AfxMessageBox(strError);
}


void CDlgMain::OnBnClickedButtonStart()
{
	// TODO: Add your control notification handler code here
	m_blRegList = true;
}

void CDlgMain::OnBnClickedButtonStop()
{
	// TODO: Add your control notification handler code here
	m_blRegList = false;
}

void CDlgMain::OnBnClickedButtonClear()
{
	// TODO: Add your control notification handler code here
	m_cListCtrl.DeleteAllItems();
}
