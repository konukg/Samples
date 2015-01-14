#if !defined(AFX_FILEACM_H__2E5CC52F_F56E_491D_AC53_5D43262EF363__INCLUDED_)
#define AFX_FILEACM_H__2E5CC52F_F56E_491D_AC53_5D43262EF363__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// FileAcm.h : header file
//



/////////////////////////////////////////////////////////////////////////////
// CFileAcm thread

class CFileAcm : public CWinThread
{
	DECLARE_DYNCREATE(CFileAcm)
protected:
	//CFileAcm();           // protected constructor used by dynamic creation

// Attributes
public:
	HANDLE m_hEvent1;
	int m_iByteRec1;
	int m_iByteEnd;
	HWND m_hDlgRec;
	int m_iClsFile1;
	bool m_StartAcm;
	int m_DestOffset1;
	HANDLE m_hFile1;
	char m_FileName1[50];
	BYTE m_btRec1[32768];
	
	WAVEFORMATEX m_wfx;
	ACMFORMATCHOOSE m_afc;
	ACMSTREAMHEADER m_Header1;
	HACMSTREAM m_hStream1;
	DWORD m_Flags1;
	
	CFileAcm();
// Operations
public:
	
	void ProcessAcm1();
	void InitAcm1();
	CString DateFile1();
	void UpdateFile1(HANDLE hFile, WAVEFORMATEX *wfx, int DataSize);
	int InitFile1(HANDLE hFile, LPWAVEFORMATEX pwfx);
// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFileAcm)
	public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();
	//}}AFX_VIRTUAL

// Implementation
protected:
	//void OnTimer(UINT nIDEvent);
	virtual ~CFileAcm();

	// Generated message map functions
	//{{AFX_MSG(CFileAcm)
		// NOTE - the ClassWizard will add and remove member functions here.
	afx_msg void OnFileRec1(WPARAM wParam, LPARAM lParam);
	//}}AFX_MSG
	
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FILEACM_H__2E5CC52F_F56E_491D_AC53_5D43262EF363__INCLUDED_)
