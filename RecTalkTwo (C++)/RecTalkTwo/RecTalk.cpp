// RecTalk.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "RecTalk.h"
#include "MainFrm.h"
#include "InSys.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CRecTalkApp

BEGIN_MESSAGE_MAP(CRecTalkApp, CWinApp)
	//{{AFX_MSG_MAP(CRecTalkApp)
	ON_COMMAND(ID_APP_ABOUT, OnAppAbout)
	ON_COMMAND(IDR_CONFIG_SYS, OnInputSys)
	ON_COMMAND(IDR_ACM_SEL, OnAcmSel)
	ON_COMMAND(ID_RECDLG_EXIT, OnExitRec)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CRecTalkApp construction

CRecTalkApp::CRecTalkApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CRecTalkApp object

CRecTalkApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CRecTalkApp initialization

BOOL CRecTalkApp::InitInstance()
{
	HWND hWnd;
	hWnd=FindWindow(NULL, "Звукозапись");
	if(hWnd)
	{
		if(IsIconic(hWnd))
			ShowWindow(hWnd,SW_RESTORE);
		SetForegroundWindow(hWnd);
		return FALSE;
	}

	AfxEnableControlContainer();

	// Standard initialization
	// If you are not using these features and wish to reduce the size
	//  of your final executable, you should remove from the following
	//  the specific initialization routines you do not need.

//#ifdef _AFXDLL
	//Enable3dControls();			// Call this when using MFC in a shared DLL
//#else
	//Enable3dControlsStatic();	// Call this when linking to MFC statically
//#endif

	// Change the registry key under which our settings are stored.
	// TODO: You should modify this string to be something appropriate
	// such as the name of your company or organization.
	SetRegistryKey(_T("Local AppWizard-Generated Applications"));


	// To create the main window, this code creates a new frame window
	// object and then sets it as the application's main window object.

	CMainFrame* pFrame = new CMainFrame;
	m_pMainWnd = pFrame;

	// create and load the frame with its resources

	pFrame->LoadFrame(IDR_MAINFRAME,
		WS_OVERLAPPEDWINDOW | FWS_ADDTOTITLE, NULL,
		NULL);

	// The one and only window has been initialized, so show and update it.
	int rtr = GetPrivateProfileInt("Params", "SysTray", 0, 
			                      "\\SoundDisp\\rectalk.ini");
	if(rtr == 1)
		pFrame->m_TrayIcon.ShowIcon();
	else
	{
		pFrame->ShowWindow(SW_SHOW);
		pFrame->UpdateWindow();
		pFrame->m_TrayIcon.HideIcon();
	}

	InitRecord();
	InitFileAcm();
	InitFileAcmT();

	if(m_pRecSound)
	{
		m_pRecSound->PostThreadMessage(WM_RECORDSOUND_WRITETHREAD,
										(WPARAM)m_pFileAcmT, (LPARAM)m_pFileAcm);
	}
	
	m_pDialog = new CDlgRec();
	m_pDialog->m_RecordThread = m_pRecSound;
	BOOL ret = m_pDialog->Create(IDD_DIALOG_REC);
	m_pDialog->ShowWindow(SW_SHOW);
	m_pRecSound->m_hDlgRec = m_pDialog->m_hWnd;
	
	m_pFileAcm->m_afc = m_pDialog->m_afc;
	m_pFileAcm->m_hDlgRec = m_pDialog->m_hWnd;
	m_pFileAcm->InitAcm1();

	m_pFileAcmT->m_afc = m_pDialog->m_afc;
	m_pFileAcmT->m_hDlgRec = m_pDialog->m_hWnd;
	m_pFileAcmT->InitAcm2();

	return TRUE;
}

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();
	
// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
		// No message handlers
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

// App command to run the dialog
void CRecTalkApp::OnAppAbout()
{
	CAboutDlg aboutDlg;
	aboutDlg.DoModal();
}

/////////////////////////////////////////////////////////////////////////////
// CRecTalkApp message handlers


bool CRecTalkApp::InitRecord()
{
	m_pRecSound = new CRecSound();
	m_pRecSound->CreateThread();
	return TRUE;
}

bool CRecTalkApp::InitFileAcm()
{
	m_pFileAcm = new CFileAcm();
	m_pFileAcm->CreateThread();
	return TRUE;
}

bool CRecTalkApp::InitFileAcmT()
{
	m_pFileAcmT = new CFileAcmT();
	m_pFileAcmT->CreateThread();
	return TRUE;
}

void CRecTalkApp::OnInputSys()
{
//int result;
//TCHAR szAdmName[] = "administrator";
//TCHAR szSamName[512];
//DWORD dwSize = sizeof(szSamName);
	
	//GetUserName(szSamName, &dwSize);
	//result = strcmp(szAdmName, szSamName);
	//if(result >= 0)
	//{
		CInSys dlg;
		dlg.DoModal();
	
		int rtr = 0;
		/*rtr = GetPrivateProfileInt("Params", "AutoStart", 0, 
			                      "\\SoundDisp\\rectalk.ini");
		if(rtr == 1)
			m_pDialog->m_pRecord->PostThreadMessage(WM_RECORDSOUND_STARTRECORDING, 0, 0L);
		else
			m_pDialog->m_pRecord->m_Stop = true;
		*/
		rtr = GetPrivateProfileInt("Params", "Volume1", 0, 
									  "\\SoundDisp\\rectalk.ini");
		m_pRecSound->m_iRecLevel1 = rtr;

		rtr = GetPrivateProfileInt("Params", "Volume2", 0, 
									  "\\SoundDisp\\rectalk.ini");
		m_pRecSound->m_iRecLevel2 = rtr;
	//}
	//else
	//{
		//MessageBox(m_pMainWnd->m_hWnd, "У вас нет прав изменить конфигурацию",
					//"Закрыть программу", MB_OK|MB_ICONSTOP);
	//}
}

void CRecTalkApp::OnAcmSel()
{
CFile myFile;
CFileException fileException;
ACMFORMATCHOOSE afc;
WAVEFORMATEX wfx;
int Frequency, BitCount, Channels;

	//int result;
	//TCHAR szAdmName[] = "administrator";
	//TCHAR szSamName[512];
	//DWORD dwSize = sizeof(szSamName);
	
	//GetUserName(szSamName, &dwSize);
	//result = 1;	//strcmp(szAdmName, szSamName);
	//if(result >= 0)
	//{
		ZeroMemory((void*) &afc, sizeof(afc));
		afc.cbStruct = sizeof(afc);
		int MaxSize = sizeof(WAVEFORMATEX);
		acmMetrics(0, ACM_METRIC_MAX_SIZE_FORMAT, (void*)&MaxSize);
		afc.pwfx = (WAVEFORMATEX*) malloc(MaxSize);
		try
		{
			afc.hwndOwner = m_pMainWnd->m_hWnd;
			afc.cbwfx = MaxSize;
		if ( !myFile.Open("\\SoundDisp\\acm.dat", CFile::modeRead, &fileException ))
		{
			afc.fdwEnum = ACM_FORMATENUMF_CONVERT;
		}
		else
		{
			afc.fdwEnum = ACM_FORMATENUMF_CONVERT;
			afc.fdwStyle = ACMFORMATCHOOSE_STYLEF_INITTOWFXSTRUCT;
			myFile.Read(afc.pwfx,MaxSize);
			myFile.Close();
		}
			Frequency = 8000;
			BitCount = 8;
			Channels = 1;
		
			wfx.wFormatTag = WAVE_FORMAT_PCM;
			wfx.nChannels = (WORD) Channels;
			wfx.nSamplesPerSec = (DWORD) Frequency;
			wfx.wBitsPerSample = (WORD) BitCount;
			wfx.nBlockAlign = (WORD)(BitCount/8*Channels);
			wfx.nAvgBytesPerSec = wfx.nSamplesPerSec*wfx.nBlockAlign;
			wfx.cbSize = 0;

			afc.pwfxEnum = &wfx;
			afc.pszTitle = "Укажите новый формат";
			if(MMSYSERR_NOERROR==acmFormatChoose(&afc))
			{
				CFile inFile("\\SoundDisp\\acm.dat",
								CFile::modeCreate|CFile::modeWrite|CFile::typeBinary);
				inFile.Write(afc.pwfx, MaxSize);
				inFile.Close();
				MessageBox( m_pMainWnd->m_hWnd,"Необходимо перезагрузить приложение",
							"Установка нового кодека", MB_OK|MB_ICONSTOP);
			}
			
		}
		catch(char *Message)
		{
			MessageBox(m_pMainWnd->m_hWnd, Message, "Ошибка!", MB_ICONSTOP);
		}
		free((void*) afc.pwfx);
	//}
	//else
	//{
		//MessageBox(m_pMainWnd->m_hWnd, "У вас нет прав закрыть программу",
					//"Закрыть программу", MB_OK|MB_ICONSTOP);
	//}
}

void CRecTalkApp::OnExitRec()
{
	//int result = 1;
	//TCHAR szAdmName[] = "administrator";
	//TCHAR szSamName[512];
	//DWORD dwSize = sizeof(szSamName);
	
	//PostMessage(m_pDialog->m_hWnd,WM_RECORDSOUND_STOPWRITE,0,0);

	//GetUserName(szSamName, &dwSize);
	//result = strcmp(szAdmName, szSamName);	//result = 1;
	//if(result >= 0)
	//{
		PostMessage(m_pDialog->m_hWnd,WM_RECORDSOUND_STOPWRITE,0,0);
		//AfxGetApp()->m_pMainWnd->PostMessage(WM_QUIT,0,0);
	//}
	//else
	//{
		//MessageBox(m_pMainWnd->m_hWnd, "У вас нет прав закрыть программу",
					//"Закрыть программу", MB_OK|MB_ICONSTOP);
	//}
}
