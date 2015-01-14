#pragma once

#include "Picture.h"

// CDlgImage dialog

class CDlgImage : public CDialog
{
	DECLARE_DYNAMIC(CDlgImage)

public:
	bool blPlay;
	CString  m_FilePathName;
	CRect   m_ClientRect;
	CPicture m_Picture;
	CDlgImage(CWnd* pParent = NULL);   // standard constructor
	virtual ~CDlgImage();

// Dialog Data
	enum { IDD = IDD_DIALOG_IMAGE };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnPaint();
	void OnTimer(UINT nIDEvent);
	void StartPlaySound(void);
	void StopPlaySound(void);
};
