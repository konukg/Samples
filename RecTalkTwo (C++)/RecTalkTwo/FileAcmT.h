#if !defined(AFX_FILEACMT_H__4A63D800_D531_4B0F_9717_E23C0ACA7F71__INCLUDED_)
#define AFX_FILEACMT_H__4A63D800_D531_4B0F_9717_E23C0ACA7F71__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// FileAcmT.h : header file
//



/////////////////////////////////////////////////////////////////////////////
// CFileAcmT thread

class CFileAcmT : public CWinThread
{
	DECLARE_DYNCREATE(CFileAcmT)
protected:
	//CFileAcmT();           // protected constructor used by dynamic creation

// Attributes
public:
	int m_iByteRec2;
	HWND m_hDlgRec;
	int m_iClsFile2;
	bool m_StartAcm;
	int m_DestOffset2;
	HANDLE m_hFile2;
	char m_FileName2[50];
	BYTE m_btRec2[65536];
	
	WAVEFORMATEX m_wfx;
	ACMFORMATCHOOSE m_afc;
	ACMSTREAMHEADER m_Header2;
	HACMSTREAM m_hStream2;
	DWORD m_Flags2;

	CFileAcmT();
// Operations
public:
	CString DateFile2();
	void UpdateFile2(HANDLE hFile, WAVEFORMATEX *wfx, int DataSize);
	void ProcessAcm2();
	void InitAcm2();
	int InitFile2(HANDLE hFile, LPWAVEFORMATEX pwfx);
// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFileAcmT)
	public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();
	//}}AFX_VIRTUAL

// Implementation
protected:
	
	virtual ~CFileAcmT();

	// Generated message map functions
	//{{AFX_MSG(CFileAcmT)
		// NOTE - the ClassWizard will add and remove member functions here.
	afx_msg void OnFileRec2(WPARAM wParam, LPARAM lParam);
	//}}AFX_MSG
	

	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FILEACMT_H__4A63D800_D531_4B0F_9717_E23C0ACA7F71__INCLUDED_)
