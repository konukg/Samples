// DlgRec.cpp : implementation file
//

#include "stdafx.h"
#include "RecTalk.h"
#include "DlgRec.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CDlgRec dialog


CDlgRec::CDlgRec(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgRec::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDlgRec)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
}


void CDlgRec::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDlgRec)
	DDX_Control(pDX, IDC_BUTTON_STOP2, m_buttonStop2);
	DDX_Control(pDX, IDC_BUTTON_REC2, m_buttonRec2);
	DDX_Control(pDX, IDC_BUTTON_STOP1, m_buttonStop1);
	DDX_Control(pDX, IDC_BUTTON_REC1, m_buttonRec1);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CDlgRec, CDialog)
	//{{AFX_MSG_MAP(CDlgRec)
	ON_BN_CLICKED(IDC_BUTTON_REC1, OnButtonRec1)
	ON_BN_CLICKED(IDC_BUTTON_STOP1, OnButtonStop1)
	ON_BN_CLICKED(IDC_BUTTON_REC2, OnButtonRec2)
	ON_BN_CLICKED(IDC_BUTTON_STOP2, OnButtonStop2)
	ON_MESSAGE(WM_RECORDSOUND_STOPWRITE,OnStopRecording)
	ON_WM_TIMER()
	ON_BN_CLICKED(IDC_BUTTON_TEST, OnButtonTest)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDlgRec message handlers

BOOL CDlgRec::OnInitDialog()
{
	CDialog::OnInitDialog();

CFile myFile;
CFileException fileException;

	m_buttonRec1.SetIcon(AfxGetApp()->LoadIcon(IDI_ICON_REC));
	m_buttonStop1.SetIcon(AfxGetApp()->LoadIcon(IDI_ICON_STOP));
	m_buttonStop1.EnableWindow(FALSE);
	m_buttonRec2.SetIcon(AfxGetApp()->LoadIcon(IDI_ICON_REC));
	m_buttonStop2.SetIcon(AfxGetApp()->LoadIcon(IDI_ICON_STOP));
	m_buttonStop2.EnableWindow(FALSE);
	
	ZeroMemory((void*) &m_afc, sizeof(m_afc));
	m_afc.cbStruct = sizeof(m_afc);
	int MaxSize = sizeof(WAVEFORMATEX);
	acmMetrics(0, ACM_METRIC_MAX_SIZE_FORMAT, (void*)&MaxSize);
	m_afc.pwfx = (WAVEFORMATEX*) malloc(MaxSize);
	try
	{			
		if ( !myFile.Open("\\SoundDisp\\acm.dat",
							CFile::modeRead|CFile::shareCompat, &fileException ))
		{
			//m_iACM = 0;
			MessageBox( "Необходимо выбрать тип кодека",
						"Установка кодека", MB_OK|MB_ICONSTOP);
		}
		else
		{
			myFile.Read(m_afc.pwfx,MaxSize);
			myFile.Close();
			
			//m_iACM = 1;
		}
	}
	catch(char *Message)
	{
		MessageBox(Message, "Ошибка!", MB_ICONSTOP);
	}

	m_iAutoRec = 0;
	m_iAutoRec = GetPrivateProfileInt("Params", "AutoStart", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(m_iAutoRec == 1)
	{
		m_buttonRec1.EnableWindow(FALSE);
		m_buttonRec2.EnableWindow(FALSE);
		//SetTimer(1, 60000, 0);
	}
	else
	{
		m_buttonRec1.EnableWindow(TRUE);
		m_buttonRec2.EnableWindow(TRUE);
	}
	
	int iMfhCnl = GetPrivateProfileInt("Params", "MfnCanal", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(iMfhCnl == 1)
		SetDlgItemText(IDC_EDIT1_CN2,"Канал отключен!");

	m_RecordThread->PostThreadMessage(WM_RECORDSOUND_STARTRECORDING, 0, 0L);
	m_iRestart = 1; //рестарт по таймеру 1 через 60 мин.
	
	return TRUE;
}

void CDlgRec::OnButtonRec1() 
{
	m_RecordThread->m_Stop1 = false;
	m_buttonRec1.EnableWindow(FALSE);
	m_buttonStop1.EnableWindow(TRUE);
}

void CDlgRec::OnButtonStop1() 
{
	m_RecordThread->m_Stop1 = true;
	m_buttonStop1.EnableWindow(FALSE);
	m_buttonRec1.EnableWindow(TRUE);
	m_RecordThread->m_RecordPosition1 = 0;
	SetDlgItemText(IDC_EDIT1_CN1,"");
}

void CDlgRec::OnButtonRec2() 
{
	m_RecordThread->m_Stop2 = false;
	m_buttonRec2.EnableWindow(FALSE);
	m_buttonStop2.EnableWindow(TRUE);
}

void CDlgRec::OnButtonStop2() 
{
	m_RecordThread->m_Stop2 = true;
	m_buttonStop2.EnableWindow(FALSE);
	m_buttonRec2.EnableWindow(TRUE);
	m_RecordThread->m_RecordPosition2 = 0;
	SetDlgItemText(IDC_EDIT1_CN2,"");
}

LRESULT CDlgRec::OnStopRecording(WPARAM wParam, LPARAM lParam)
{
	int rtr = wParam;
	
	if(rtr == 0)
	{
		m_RecordThread->m_blReStart = false;
		m_buttonStop1.EnableWindow(FALSE);
		m_buttonStop2.EnableWindow(FALSE);
	}else m_RecordThread->m_blReStart = true;
	
	m_RecordThread->m_Stop1 = true;
	m_RecordThread->m_Stop2 = true; 
	m_RecordThread->m_Stop = true;

	return TRUE;
}

void CDlgRec::OnTimer(UINT nIDEvent)
{
	switch (nIDEvent)
	{
	case 1:
		if(m_iRestart >= 60)
		{
			m_iRestart = 1;
			//OnStopRecording(1,0);
		}else m_iRestart++;
	break;
	}
}

void CDlgRec::OnButtonTest() 
{
	char TempDir[50] = "\\SoundDisp\\Temp";
	strcat(TempDir,tmpnam(0));
	
}
