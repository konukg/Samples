// FileAcm.cpp : implementation file
//

#include "stdafx.h"
#include "RecTalk.h"
#include "FileAcm.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CFileAcm

IMPLEMENT_DYNCREATE(CFileAcm, CWinThread)

CFileAcm::CFileAcm()
{
	int Frequency = 8000;
	int BitCount = 8;
	int Channels = 1;
	int DeviceID = 0;
		
	m_wfx.wFormatTag = WAVE_FORMAT_PCM;
	m_wfx.nChannels = (WORD) Channels;
	m_wfx.nSamplesPerSec = (DWORD) Frequency;
	m_wfx.wBitsPerSample = (WORD) BitCount;
	m_wfx.nBlockAlign = (WORD)(BitCount/8*Channels);
	m_wfx.nAvgBytesPerSec = m_wfx.nSamplesPerSec*m_wfx.nBlockAlign;
    m_wfx.cbSize = 0;

	m_hFile1 = INVALID_HANDLE_VALUE;
	m_DestOffset1 = 0;
	m_StartAcm = false;
	m_iByteEnd = 0;
}

CFileAcm::~CFileAcm()
{
}

BOOL CFileAcm::InitInstance()
{
	// TODO:  perform and per-thread initialization here
	return TRUE;
}

int CFileAcm::ExitInstance()
{
	// TODO:  perform any per-thread cleanup here
	GlobalFree(m_Header1.pbSrc);
	GlobalFree(m_Header1.pbDst);
		//if(m_Header1.pbSrc != 0) free(m_Header1.pbSrc);
	acmStreamUnprepareHeader(m_hStream1, &m_Header1, 0);
	acmStreamClose(m_hStream1, 0);
	
	CloseHandle(m_hFile1);
	DeleteFile("\\SoundDisp\\Temp\\a.cn1");

	return CWinThread::ExitInstance();
}

BEGIN_MESSAGE_MAP(CFileAcm, CWinThread)
	//{{AFX_MSG_MAP(CFileAcm)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	ON_THREAD_MESSAGE(WM_RECORDSOUND_WRITEFILE1, OnFileRec1)
	//}}AFX_MSG_MAP
	
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFileAcm message handlers

void CFileAcm::InitAcm1()
{
	char TempFileName1[50] = "\\SoundDisp\\Temp\\a.cn1";
	m_hFile1 = CreateFile(TempFileName1, GENERIC_WRITE, 0, 0, 
							CREATE_ALWAYS, FILE_ATTRIBUTE_ARCHIVE, 0);
	InitFile1(m_hFile1, m_afc.pwfx);
	
	if(MMSYSERR_NOERROR!=acmStreamOpen(&m_hStream1, 0, &m_wfx, 
			m_afc.pwfx, 0, 0, 0, ACM_STREAMOPENF_NONREALTIME))
		MessageBox(m_hDlgRec, "Не удаётся открыть поток преобразования данных",
						"Ошибка преобразования канал 1", MB_OK|MB_ICONSTOP);
		
	ZeroMemory((void*) &m_Header1, sizeof(m_Header1));
	m_Header1.cbStruct = sizeof(m_Header1);
	int SourceSize = 32768, DestSize = SourceSize; 
	acmStreamSize(m_hStream1, SourceSize, (LPDWORD)&DestSize, 
						ACM_STREAMSIZEF_SOURCE);
	m_Header1.pbSrc = (BYTE*)GlobalAlloc(GMEM_FIXED, SourceSize);
	m_Header1.pbDst = (BYTE*)GlobalAlloc(GMEM_FIXED, DestSize);
	//m_Header1.pbSrc = (LPBYTE) malloc(SourceSize+DestSize);
	//m_Header1.pbDst = m_Header1.pbSrc + SourceSize;
	m_Header1.cbSrcLength = SourceSize;
	m_Header1.cbDstLength = DestSize;
	if(MMSYSERR_NOERROR!=acmStreamPrepareHeader(m_hStream1, &m_Header1, 0))
		MessageBox(m_hDlgRec, "Не удаётся подготовить заголовок потока",
						"Ошибка преобразования канал 1", MB_OK|MB_ICONSTOP);	
	
		//m_Flags1 = ACM_STREAMCONVERTF_BLOCKALIGN;
		m_Flags1 = ACM_STREAMCONVERTF_START;
}

void CFileAcm::OnFileRec1(WPARAM wParam, LPARAM lParam)
{
	int iClsFile = (int) wParam;
	if(iClsFile == 0)
	{
		m_iClsFile1 = 0;
		m_iByteRec1 = 32768;
		m_Header1.cbSrcLength = 32768;
	}
	else
	{
		m_iByteRec1 = iClsFile;
		m_iClsFile1 = 1;
		m_Header1.cbSrcLength = m_iByteRec1;
		m_Flags1 |= ACM_STREAMCONVERTF_END;
	}
	memcpy(m_Header1.pbSrc,(LPBYTE)lParam,m_iByteRec1);
	
	//clock_t start, finish;
	//int duration;

	//SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN2,"");
	//start = clock();
		ProcessAcm1();
	//finish = clock();
	//duration = (finish - start);
	//char buffer[32];
	//_itoa(duration, buffer, 10);
	//SetDlgItemText(m_hDlgRec,IDC_EDIT1_CN2,buffer);

	//return TRUE;
}

void CFileAcm::ProcessAcm1()
{
	m_Flags1 |= ACM_STREAMCONVERTF_BLOCKALIGN;

	DWORD BytesReaded;
	if(MMSYSERR_NOERROR!=acmStreamConvert(m_hStream1, &m_Header1, m_Flags1))
		MessageBox(m_hDlgRec, "Ошибка во время преобразования данных",
						"Ошибка преобразования канал 1", MB_OK|MB_ICONSTOP);
					
	WriteFile(m_hFile1, m_Header1.pbDst, m_Header1.cbDstLengthUsed, &BytesReaded, 0);
	m_DestOffset1 += m_Header1.cbDstLengthUsed;

	if(m_iClsFile1 == 1)
	{
		DWORD dwSize = (GetFileSize (m_hFile1, NULL)-6500);
		SetFilePointer(m_hFile1, dwSize, NULL, FILE_BEGIN);
		SetEndOfFile(m_hFile1);
		m_DestOffset1 -= 6500;	//28672
		UpdateFile1(m_hFile1, m_afc.pwfx, m_DestOffset1);
		CloseHandle(m_hFile1);
		m_hFile1 = INVALID_HANDLE_VALUE;
		m_DestOffset1 = 0;
		CString FileName, FileDate;
		FileDate = DateFile1();
		FileName = "\\SoundDisp\\Sound1\\" + FileDate + ".wav";
		MoveFile("\\SoundDisp\\Temp\\a.cn1", FileName);
		
		GlobalFree(m_Header1.pbSrc);
		GlobalFree(m_Header1.pbDst);
		//if(m_Header1.pbSrc != 0) free(m_Header1.pbSrc);
		acmStreamUnprepareHeader(m_hStream1, &m_Header1, 0);
		acmStreamClose(m_hStream1, 0);
		
		InitAcm1();
	}
}

int CFileAcm::InitFile1(HANDLE hFile, LPWAVEFORMATEX pwfx)
{
	DWORD Data, BytesWritten;
	WriteFile(hFile, "RIFF    WAVEfmt ", 16, &BytesWritten, 0);
	if(WAVE_FORMAT_PCM==pwfx->wFormatTag) Data = sizeof(PCMWAVEFORMAT);
	else Data = sizeof(WAVEFORMATEX) + pwfx->cbSize;
	WriteFile(hFile, &Data, sizeof(Data), &BytesWritten, 0);
	WriteFile(hFile, pwfx, Data, &BytesWritten, 0);
	WriteFile(hFile, "data    ", 8, &BytesWritten, 0);

	return 28+sizeof(PCMWAVEFORMAT);
}

void CFileAcm::UpdateFile1(HANDLE hFile, WAVEFORMATEX *wfx, int DataSize)
{
	int FmtSize;
	if(WAVE_FORMAT_PCM==wfx->wFormatTag) FmtSize = sizeof(PCMWAVEFORMAT);
	else FmtSize = sizeof(WAVEFORMATEX) + wfx->cbSize;
	DWORD BytesWritten;
	DWORD Data = DataSize+FmtSize+12;
	SetFilePointer(hFile, 4, 0, FILE_BEGIN);
	WriteFile(hFile, &Data, sizeof(Data), &BytesWritten, 0);
	SetFilePointer(hFile, FmtSize+24, 0, FILE_BEGIN);
	WriteFile(hFile, &DataSize, sizeof(DataSize), &BytesWritten, 0);
}

CString CFileAcm::DateFile1()
{
	CString date;
	CTime t = CTime::GetCurrentTime();
	date.Format("%d.%02d.%02d#%02d.%02d.%02d",	t.GetYear(),
				 								t.GetMonth(),
												t.GetDay(),
												t.GetHour(),
												t.GetMinute(),
												t.GetSecond());
	return date;
}