#if !defined(AFX_RECSOUND_H__5F205BD2_4821_4A5F_9DF8_01CD4291AC2B__INCLUDED_)
#define AFX_RECSOUND_H__5F205BD2_4821_4A5F_9DF8_01CD4291AC2B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// RecSound.h : header file
//

#include "FileAcm.h"
#include "FileAcmT.h"

/////////////////////////////////////////////////////////////////////////////
// CRecSound thread
typedef __int64 big_int;

class CRecSound : public CWinThread
{
	DECLARE_DYNCREATE(CRecSound)
protected:
	//CRecSound();           // protected constructor used by dynamic creation

// Attributes
public:
	//char m_FileName1[256];
	//char m_FileName2[256];
	int m_iRecLevel1, m_iRecLevel2, m_iMfnCanal;
	bool m_Stop,m_Stop1,m_Stop2;
	int m_DeviceID;
	int m_Frequency;
	int m_BitCount;
	int m_Channels;

	HWND m_hDlgRec;
	big_int m_RecordPosition1, m_RecordPosition2;
	//CString TempFileName1,TempFileName2;
	HWAVEIN m_hRecord;
	WAVEFORMATEX m_wfx;
	CRecSound();
// Operations
public:
	bool m_blReStart;
	LRESULT StopRecording();
	void ProcessRecord();
	void OnProcessRecording(WPARAM wParam, LPARAM lParam);

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CRecSound)
	public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();
	//}}AFX_VIRTUAL

// Implementation
protected:
	CFileAcm* m_FileAcmThread;
	CFileAcmT* m_FileAcmThreadT;
	
	virtual ~CRecSound();
	
	// Generated message map functions
	//{{AFX_MSG(CRecSound)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG
	afx_msg void OnPtrSoundWriter(WPARAM wParam, LPARAM lParam);

	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_RECSOUND_H__5F205BD2_4821_4A5F_9DF8_01CD4291AC2B__INCLUDED_)
