; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CInSys
LastTemplate=CWinThread
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "RecTalk.h"
LastPage=0

ClassCount=8
Class1=CRecTalkApp
Class3=CMainFrame
Class4=CAboutDlg

ResourceCount=11
Resource1=IDD_ABOUTBOX (Russian)
Resource2=IDD_DLG_INP_SYS (Russian)
Class2=CFileAcm
Resource3=IDR_POPUP_MENU (Russian)
Class5=CDlgRec
Resource4=IDR_MAINFRAME (Russian)
Class6=CRecSound
Resource5=IDD_DIALOG_REC (Russian)
Class7=CInSys
Class8=CFileAcmT
Resource6=IDD_ABOUTBOX
Resource7=IDR_POPUP_MENU
Resource8=IDD_DLG_INP_SYS
Resource9=IDD_DIALOG_REC
Resource10=IDR_MAINFRAME
Resource11=IDR_MAINFRAME (English (U.S.))

[CLS:CRecTalkApp]
Type=0
HeaderFile=RecTalk.h
ImplementationFile=RecTalk.cpp
Filter=N

[CLS:CMainFrame]
Type=0
HeaderFile=MainFrm.h
ImplementationFile=MainFrm.cpp
Filter=T
BaseClass=CFrameWnd
VirtualFilter=fWC
LastObject=CMainFrame




[CLS:CAboutDlg]
Type=0
HeaderFile=RecTalk.cpp
ImplementationFile=RecTalk.cpp
Filter=D
LastObject=CAboutDlg

[ACL:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_EDIT_COPY
Command2=ID_EDIT_PASTE
Command3=ID_EDIT_UNDO
Command4=ID_EDIT_CUT
Command5=ID_NEXT_PANE
Command6=ID_PREV_PANE
Command7=ID_EDIT_COPY
Command8=ID_EDIT_PASTE
Command9=ID_EDIT_CUT
Command10=ID_EDIT_UNDO
CommandCount=10

[MNU:IDR_MAINFRAME (Russian)]
Type=1
Class=CMainFrame
Command1=ID_RECDLG_EXIT
Command2=IDR_CONFIG_SYS
Command3=IDR_ACM_SEL
Command4=ID_APP_ABOUT
CommandCount=4

[TB:IDR_MAINFRAME (Russian)]
Type=1
Class=?
Command1=ID_APP_ABOUT
CommandCount=1

[DLG:IDD_DIALOG_REC (Russian)]
Type=1
Class=CDlgRec
ControlCount=12
Control1=IDOK,button,1073807361
Control2=IDCANCEL,button,1073807360
Control3=IDC_EDIT1_CN1,edit,1350567936
Control4=IDC_BUTTON_REC1,button,1342177344
Control5=IDC_BUTTON_STOP1,button,1342177344
Control6=IDC_STATIC,static,1342308352
Control7=IDC_EDIT1_CN2,edit,1350568064
Control8=IDC_BUTTON_REC2,button,1342177344
Control9=IDC_BUTTON_STOP2,button,1342177344
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC,button,1342177287
Control12=IDC_BUTTON_TEST,button,1073807360

[CLS:CDlgRec]
Type=0
HeaderFile=DlgRec.h
ImplementationFile=DlgRec.cpp
BaseClass=CDialog
Filter=D
LastObject=CDlgRec
VirtualFilter=dWC

[MNU:IDR_POPUP_MENU (Russian)]
Type=1
Class=?
Command1=ID_APP_SHOW_REC
Command2=IDR_CONFIG_SYS
Command3=IDR_ACM_SEL
Command4=ID_RECDLG_EXIT
CommandCount=4

[DLG:IDD_DLG_INP_SYS (Russian)]
Type=1
Class=CInSys
ControlCount=11
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1073807360
Control3=IDC_CHECK_AUTO,button,1342242819
Control4=IDC_CHECK_MIN,button,1342242819
Control5=IDC_STATIC,button,1342177287
Control6=IDC_VOLUME_LEVEL1,msctls_trackbar32,1342242816
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC_VOLUME1,static,1342312448
Control9=IDC_VOLUME_LEVEL2,msctls_trackbar32,1342242816
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC_VOLUME2,static,1342312448

[CLS:CInSys]
Type=0
HeaderFile=InSys.h
ImplementationFile=InSys.cpp
BaseClass=CDialog
Filter=D
LastObject=IDC_VOLUME_LEVEL1
VirtualFilter=dWC

[CLS:CRecSound]
Type=0
HeaderFile=RecSound.h
ImplementationFile=RecSound.cpp
BaseClass=CWinThread
Filter=N

[CLS:CFileAcm]
Type=0
HeaderFile=FileAcm.h
ImplementationFile=FileAcm.cpp
BaseClass=CWinThread
Filter=N
VirtualFilter=TC
LastObject=CFileAcm

[DLG:IDD_ABOUTBOX (Russian)]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[CLS:CFileAcmT]
Type=0
HeaderFile=FileAcmT.h
ImplementationFile=FileAcmT.cpp
BaseClass=CWinThread
Filter=N

[DLG:IDD_DLG_INP_SYS]
Type=1
Class=CInSys
ControlCount=16
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1073807360
Control3=IDC_CHECK_AUTO,button,1342242819
Control4=IDC_CHECK_MIN,button,1342242819
Control5=IDC_STATIC,button,1342177287
Control6=IDC_VOLUME_LEVEL1,msctls_trackbar32,1342242816
Control7=IDC_STATIC_LEVEL1,static,1342308352
Control8=IDC_STATIC_VOLUME1,static,1342312448
Control9=IDC_VOLUME_LEVEL2,msctls_trackbar32,1342242816
Control10=IDC_STATIC_LEVEL2,static,1342308352
Control11=IDC_STATIC_VOLUME2,static,1342312448
Control12=IDC_CHECK_MFN,button,1342242819
Control13=IDC_COMBO_DEV1,combobox,1344339970
Control14=IDC_STATIC_DEV1,static,1342312449
Control15=IDC_STATIC,static,1342308352
Control16=IDC_STATIC,static,1342308352

[TB:IDR_MAINFRAME]
Type=1
Class=?
Command1=ID_APP_ABOUT
CommandCount=1

[MNU:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_RECDLG_EXIT
Command2=IDR_CONFIG_SYS
Command3=IDR_ACM_SEL
Command4=ID_APP_ABOUT
CommandCount=4

[MNU:IDR_POPUP_MENU]
Type=1
Class=?
Command1=ID_APP_SHOW_REC
Command2=IDR_CONFIG_SYS
Command3=IDR_ACM_SEL
CommandCount=3

[DLG:IDD_DIALOG_REC]
Type=1
Class=?
ControlCount=12
Control1=IDOK,button,1073807361
Control2=IDCANCEL,button,1073807360
Control3=IDC_EDIT1_CN1,edit,1350567936
Control4=IDC_BUTTON_REC1,button,1342177344
Control5=IDC_BUTTON_STOP1,button,1342177344
Control6=IDC_STATIC,static,1342308352
Control7=IDC_EDIT1_CN2,edit,1350568064
Control8=IDC_BUTTON_REC2,button,1342177344
Control9=IDC_BUTTON_STOP2,button,1342177344
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC,button,1342177287
Control12=IDC_BUTTON_TEST,button,1073807360

[DLG:IDD_ABOUTBOX]
Type=1
Class=?
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[ACL:IDR_MAINFRAME (English (U.S.))]
Type=1
Class=?
Command1=ID_EDIT_COPY
Command2=ID_EDIT_PASTE
Command3=ID_EDIT_UNDO
Command4=ID_EDIT_CUT
Command5=ID_NEXT_PANE
Command6=ID_PREV_PANE
Command7=ID_EDIT_COPY
Command8=ID_EDIT_PASTE
Command9=ID_EDIT_CUT
Command10=ID_EDIT_UNDO
CommandCount=10

