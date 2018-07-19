;#pragma option -v+

#define SetupVersion "1.0.7.0"
#define MyAppName "Termulator"
#define MyAppExeName "Termulator.exe"
#define MyGroupName "Termulator"
#define AppVersion GetFileVersion('..\Termulator\bin\Debug\Termulator.exe')

;; these next items are customized for Debug vs. Release builds
;;  MyBuildFolder is where the EXEs and DLLs, et al., come from
;;  MyGroupName is the installation folder name and the "group" for menus, icons
;;  MyOutputFilenameSuffix is so the debug build can have "Debug" in the setup filename

#define MyBuildFolder "..\Termulator\bin\Debug"
#define MyOutputFilenameSuffix "Debug_Setup"
;;release;; #define MyBuildFolder "..\Termulator\bin\Release"
;;release;; #define MyOutputFilenameSuffix "Release_Setup"

;; AppSrcBaseFolder is path for release notes and help file
#define AppSrcBaseFolder "..\Termulator"
#define SetupFolder = "."

#define MyAppPublisher "Coherent, Inc."
#define MyAppURL "http://www.coherent.com/"
#define MyAppCopyright "Copyright (C) 2013-2017 Coherent, Inc."

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
; using following GUID for the DEBUG install
; AppId={{EC89E8D2-9D0E-4CF7-BB85-D4DE3FBF786A} used in SetupVersion "1.0.5.1" and below
AppId={{C41B2CE3-A46B-49F4-B28A-BCFEA2055772}
AppName="{#MyAppName}_Debug"
AppVersion={#AppVersion}
AppPublisher=Coherent, Inc.
AppPublisherURL=http://www.coherent.com/
AppSupportURL=http://www.coherent.com/
AppUpdatesURL=http://www.coherent.com/
DefaultDirName="{pf}\Coherent\{#MyGroupName}"
DefaultGroupName="\Coherent\{#MyGroupName}"
OutputDir=.
OutputBaseFilename={#MyAppName}_v{#AppVersion}_{#MyOutputFilenameSuffix}
SetupIconFile="setup.ico"
;Compression=lzma
SolidCompression=yes
VersionInfoVersion={#SetupVersion}
VersionInfoProductVersion={#SetupVersion}
VersionInfoProductName="{#MyAppName} Setup"
VersionInfoCopyright="{#MyAppCopyright}"
UninstallDisplayName="Coherent Termulator v{#AppVersion} (Debug)"
;; copyright info also set below, in CopyrightLabel

WindowVisible=no
UseSetupLdr=yes
UsePreviousAppDir=no
UsePreviousGroup=no

;;;; Tools/Configure Sign Tool must define "byparam=$p"
SignTool=porprima $q{#SourcePath}\Misc\SignTool.exe$q sign /f $q{#SourcePath}\Misc\mycert.pfx$q /p c0herent.0 /t http://timestamp.verisign.com/scripts/timstamp.dll $f
;;;;;;;;;;;SignTool=byparam $q{#SourcePath}\Misc\SignTool.exe$q sign /f $q{#SourcePath}\Misc\mycert.pfx$q /p C0herent.0 /t http://timestamp.verisign.com/scripts/timstamp.dll $f

[Icons]
Name: "{group}\{#MyGroupName}"; Filename: "{app}\{#MyAppExeName}"; WorkingDir: "{app}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; WorkingDir: "{app}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; OnlyBelowVersion: 0,6.1

[Files]
Source: "{#MyBuildFolder}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyBuildFolder}\{#MyAppExeName}.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyBuildFolder}\Library.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\SharedLibrary\SharedLibrary\bin\Debug\SharedLibrary.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Doc\Termulator.pdf"; DestDir: "{app}"; Flags: ignoreversion isreadme

Source: "RequiredSoftware\NDP451-KB2858728-x86-x64-AllOS-ENU.exe"; DestDir: "{tmp}"; Flags: ignoreversion
source: "Misc\dpinst\MultiLin\*"; DestDir: "{tmp}\dpinst\MultiLin"; Flags: ignoreversion createallsubdirs recursesubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "it"; MessagesFile: "compiler:Languages/Italian.isl"
Name: "fr"; MessagesFile: "compiler:Languages/French.isl"
Name: "german"; MessagesFile: "compiler:Languages/German.isl"
Name: "heb"; MessagesFile: "compiler:Languages/Hebrew.isl"
Name: "jp"; MessagesFile: "compiler:Languages/Japanese.isl"

[CustomMessages]
english.NET =Microsoft .NET RunTime Library install:
english.NET2=Setup has detected that Microsoft .NET v4.5.50709 or greater is not installed.
english.NET3=This program requires the Microsoft .NET Runtime Libraries to be installed prior to running LabMax-Pro PC. Setup can install these files for you.
english.NET4=Do you want to do this?
it.NET =Microsoft NET Runtime Library installare:
it.NET2=Installazione ha rilevato che la Microsoft. NET v4.5.50709 o superiore non è installato.
it.NET3=Questo programma richiede Microsoft. NET Runtime Libraries per essere installato prima di eseguire LabMax-Pro PC. L'installazione Ã¨ possibile installare questi file per te.
it.NET4=Vuoi fare questo?
fr.NET =Microsoft .NET RunTime Library installer:
fr.NET2=Le programme d'installation a détecté que Microsoft. NET v4.5.50709 ou supérieur n'est pas installé.
fr.NET3=Ce programme nécessite Microsoft. NET Runtime Libraries être installé avant l'exécution LabMax-Pro PC. L'installation ne peut installer ces fichiers pour vous.
fr.NET4=Voulez-vous faire cela?
german.NET =. NET Microsoft Runtime Library zu installieren:
german.NET2=Setup hat festgestellt, dass Microsoft. NET v4.5.50709 oder h�her nicht installiert ist.
german.NET3=Dieses Programm ben�tigt das Microsoft. NET-Laufzeitbibliotheken, um vor der Ausf�hrung von LabMax-Pro PC installiert werden. Setup kann diese Dateien f�r Sie zu installieren.
german.NET4=Haben Sie dies tun wollen?
heb.NET = להתקין: Microsoft. NET Runtime Library
heb.NET2=ההתקנה זיהתה כי מיקרוסופט. NET v4.5.50709 או יותר אינו מותקן.
heb.NET3=תוכנית זו מחייבת את Microsoft. NET Runtime Libraries כדי להתקינו לפני הפעלת חיבור Coherent. התקנה ניתן להתקין את הקבצים האלו בשבילך.
heb.NET4=האם אתה רוצה לעשות את זה?

jp.NET =します。Microsoft NETランタイムライブラリのインストール：
jp.NET2=セットアップは、Microsoft。NET v4.5.50709以上がインストールされていないことが検出されました。
jp.NET3=このプログラムは、Microsoft。NETランタイムライブラリは、OBIS接続を実行する前にインストールする必要があります。セットアップは、あなたのためにこれらのファイルをインストールすることができます。
jp.NET4=これをやってみたいですか？

english.NETFAIL=Microsoft .NET RunTime Library install:
english.NETFAIL2=The file could not be executed.
it.NETFAIL =Microsoft .NET RunTime Library installare:
it.NETFAIL2=Il file non può essere eseguito.
fr.NETFAIL =Microsoft .NET RunTime Library installer:
fr.NETFAIL2=Le fichier n'a pas pu être exécutée.
german.NETFAIL =Microsoft. NET-Laufzeitbibliothek installieren
german.NETFAIL2=Die Datei konnte nicht ausgef�hrt werden.
heb.NETFAIL= להתקין: Microsoft. NET Runtime Library
heb.NETFAIL2=הקובץ לא יכול להיות מוצא להורג.
jp.NETFAIL=します。Microsoft NETランタイムライブラリのインストール：
jp.NETFAIL2=ファイルが実行できませんでした。

[Code]

function IsX64: Boolean;
begin
  Result := IsWin64 and (ProcessorArchitecture = paX64);
end;

function IsIA64: Boolean;
begin
  Result := IsWin64 and (ProcessorArchitecture = paIA64);
end;

function Isx86: Boolean;
begin
  Result := not IsWin64;
end;

{we don't support XP}
function IsXP: Boolean;
var
  Version: TWindowsVersion;
begin
  GetWindowsVersionEx(Version);

  // On Windows XP,
  if Version.NTPlatform and
     (Version.Major = 5) and
     (Version.Minor > 0) and (Version.Minor < 3) then
  begin
    Result := True;
  end
  else
    Result := False;
end;

procedure URLLabelOnClick(Sender: TObject);
var
  ErrorCode: Integer;
begin
  ShellExec('open', 'http://www.coherent.com', '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
end;

procedure CreateAboutButtonAndURLLabel(ParentForm: TSetupForm; CancelButton: TNewButton);
var
  {AboutButton: TNewButton;}
  URLLabel: TNewStaticText;
  CopyrightLabel: TNewStaticText;
begin
{
  AboutButton := TNewButton.Create(ParentForm);
  AboutButton.Left := ParentForm.ClientWidth - CancelButton.Left - CancelButton.Width;
  AboutButton.Top := CancelButton.Top;
  AboutButton.Width := CancelButton.Width;
  AboutButton.Height := CancelButton.Height;
  AboutButton.Caption := '&About...';
  AboutButton.OnClick := @AboutButtonOnClick;
  AboutButton.Parent := ParentForm;
}
  URLLabel := TNewStaticText.Create(ParentForm);
  URLLabel.Caption := 'www.coherent.com';
  URLLabel.Cursor := crHand;
  URLLabel.OnClick := @URLLabelOnClick;
  URLLabel.Parent := ParentForm;

  { Alter Font *after* setting Parent so the correct defaults are inherited first }
  CopyrightLabel := TNewStaticText.Create(ParentForm);
  CopyrightLabel.Caption := '{#MyAppCopyright}';
  {CopyrightLabel.Cursor := crHand;}
  CopyrightLabel.OnClick := @URLLabelOnClick;
  CopyrightLabel.Parent := ParentForm;
  { Alter Font *after* setting Parent so the correct defaults are inherited first }
  {CopyrightLabel.Font.Style := URLLabel.Font.Style; }
  {CopyrightLabel.Font.Color := clBlue;   }
  {CopyrightLabel.Top := URLLabel.Top + URLLabel.Height - URLLabel.Height - 0;}
  {CopyrightLabel.Left := URLLabel.Left + URLLabel.Width + ScaleX(20);}
  CopyrightLabel.Top := CancelButton.Top - 12;
  CopyrightLabel.Left := ParentForm.ClientWidth - CancelButton.Left - CancelButton.Width;

  URLLabel.Font.Style := URLLabel.Font.Style + [fsUnderline];
  URLLabel.Font.Color := clBlue;
  URLLabel.Top := CancelButton.Top + 3;
  URLLabel.Left := ParentForm.ClientWidth - CancelButton.Left - CancelButton.Width;

end;

procedure InitializeWizard();
var
  BackgroundBitmapImage: TBitmapImage;
  BackgroundBitmapText: TNewStaticText;
begin

  { Custom controls }

  CreateAboutButtonAndURLLabel(WizardForm, WizardForm.CancelButton);

  BackgroundBitmapImage := TBitmapImage.Create(MainForm);
  BackgroundBitmapImage.Left := 50;
  BackgroundBitmapImage.Top := 90;
  BackgroundBitmapImage.AutoSize := True;
  BackgroundBitmapImage.Bitmap := WizardForm.WizardBitmapImage.Bitmap;
  BackgroundBitmapImage.Parent := MainForm;

  BackgroundBitmapText := TNewStaticText.Create(MainForm);
  BackgroundBitmapText.Left := BackgroundBitmapImage.Left;
  BackgroundBitmapText.Top := BackgroundBitmapImage.Top + BackgroundBitmapImage.Height + ScaleY(8);
  BackgroundBitmapText.Caption := 'TBitmapImage';
  BackgroundBitmapText.Parent := MainForm;
end;

{ user defined data structure to hold .NET version information }
type NETVersionInfo = record
     MajorVersion: Cardinal;
     MinorVersion: Cardinal;
     BuildNumber: Cardinal;
end;

{---------------------------------------------------------------------
{ Function: GetVersionNumbersString(Version: String): NETVersionInfo
{ Description: Attempts to extract the .NET standard version nums
{ from the incoming string.
{
{ Returns: populated NETVersionInfo type with numbers on success.
{ otherwise returns ZEROs.
{ --------------------------------------------------------------------}
function GetVersionNumbersFromString(Version: String): NETVersionInfo;
var
yep : NETVersionInfo; { for lack of a better variable name :) }
Major: String;
Minor: String;
Build: String;
Buf: String;
idx: Integer;
begin
   Major := '';
   Minor := '';
   Build := '';
   Buf := '';
   yep.MajorVersion := 0;
   yep.MinorVersion := 0;
   yep.BuildNumber := 0;

   idx := Pos('.',Version);
   Major := Copy(Version,0,idx-1);
   Buf := Copy(Version,idx+1,20);

   idx := Pos('.',Buf);
   Minor := Copy(Buf,0,idx-1);

   Buf := Copy(Buf,idx+1,20);

   yep.MajorVersion := StrToInt(Major);
   yep.MinorVersion := StrToInt(Minor);
   yep.BuildNumber := StrToInt(Buf);
   Result := yep;
end;

function IsNETInstalled: Boolean;
var
  Exists: Boolean;
  VersionInfo: NETVersionInfo;
  VersionData: String;
  VersionName: String;
  ResultCode: Integer;
Begin
Exists := false;
VersionName := 'Version';
VersionData := '';
 if IsXP then
      Begin
              Exists := True;
      End
      else
      Begin
                  {test to see if .NET v 4.5.50709 value exists in the Full subkey }
                   if not Exists and RegKeyExists(HKEY_LOCAL_MACHINE,'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full') then begin
                      if RegQueryStringValue(HKLM,'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full',VersionName,VersionData) then begin
                         VersionInfo := GetVersionNumbersFromString(VersionData);
                         if VersionInfo.MajorVersion = 4 then begin
                           if VersionInfo.MinorVersion = 5 then begin
                              if VersionInfo.BuildNumber >= 50709 then begin
                                  Exists := true;
                              end;
                                  { MsgBox('Test2:' #13#13 + VersionData,  mbConfirmation, MB_OK); }
                            end else if VersionInfo.MinorVersion > 5 then
                                  Exists := true;
                        end else if VersionInfo.MajorVersion > 4 then
                                  Exists := true;
                      end;
                  end;
                  {if version doesn't exist, ask user to install the .NET package }
                  if not Exists then begin
                    if MsgBox(CustomMessage('NET') + #13#13 + CustomMessage('NET2') + #13#13 + CustomMessage('NET3') + #13#13 + CustomMessage('NET4'), mbConfirmation, MB_YESNO or MB_DEFBUTTON1) = idYes then begin
                      ExtractTemporaryFile('NDP451-KB2858728-x86-x64-AllOS-ENU.exe');
                      if not Exec(ExpandConstant('{tmp}\NDP451-KB2858728-x86-x64-AllOS-ENU.exe'), '', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode) then begin
                        MsgBox(CustomMessage('NETFAIL') + #13#13 + CustomMessage('NETFAIL2') + SysErrorMessage(ResultCode) + '.', mbError, MB_OK);

                       end else if Exists = false then
                          Exists := true;
                    end;
                    BringToFrontAndRestore();
                  end;
                  //if MsgBox(CustomMessage('APP')  , mbConfirmation, MB_YESNO or MB_DEFBUTTON1) = idNo   then
                 // begin
                  //    Exists := false;
                 // end;
       End;
                  Result := Exists

End;

{---------------------------------------------------------------------
{ Function:  CurStepChanged( CurStep: TSetupStep);
{ Description: This function is automatically called by the Inno Engine
{ if the function exists. Every time the next button is clicked.
{
{ Returns: true to continue on with installation else exit.
{
{ --------------------------------------------------------------------}
procedure CurStepChanged( CurStep: TSetupStep);
var
  Exists: Boolean;
  VersionData: String;
  VersionName: String;
begin
  {Log('NextButtonClick(' + IntToStr(CurStep) + ') called');     }
  Exists := false;
  VersionName := 'Version';
  VersionData := '';
  case CurStep of
    ssDone:
      begin
        IsNETInstalled();
      end;
  end;
  {Result := True;}
end;


[Run]
;Filename: "{app}\dotNetFx40_Client_x86_x64.exe"; Description: "{cm:LaunchProgram,Microsoft Runtime C Library Installer (required)}"; Flags: nowait postinstall skipifsilent
;Filename: "{app}\BLAM.exe"; Description: "{cm:LaunchProgram,BLAM}"; Flags: nowait postinstall skipifsilent
;Filename: {tmp}\dpinst\MultiLin\x86\dpinst.exe; Parameters: "/F /LM /SW /PATH {tmp}\CoherentDrivers\LabMax-Pro_SSIM_Driver"; WorkingDir: "{tmp}"; Flags: 32bit; Check: IsX86; StatusMsg: "Installing USB drivers ...";
;Filename: {tmp}\dpinst\MultiLin\amd64\dpinst.exe; Parameters: "/F /LM /SW /PATH {tmp}\CoherentDrivers\LabMax-Pro_SSIM_Driver"; WorkingDir: "{tmp}"; Flags: 64bit ; Check: IsX64;  StatusMsg: "Installing USB drivers ..."

;Filename: {tmp}\dpinst\MultiLin\x86\dpinst.exe; Parameters: "/F /LM /SW /PATH {tmp}\CoherentDrivers\PowerMax-Pro_USB_Sensor_Driver"; WorkingDir: "{tmp}"; Flags: 32bit; Check: IsX86; StatusMsg: "Installing USB drivers ...";
;Filename: {tmp}\dpinst\MultiLin\amd64\dpinst.exe; Parameters: "/F /LM /SW /PATH {tmp}\CoherentDrivers\PowerMax-Pro_USB_Sensor_Driver"; WorkingDir: "{tmp}"; Flags: 64bit ; Check: IsX64;  StatusMsg: "Installing USB drivers ..."
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;Filename: {tmp}\dpinst\MultiLin\ia64\dpinst.exe; Parameters: "/F /LM /SW /PATH {win}\inf\CoherentDrivers\obis"; WorkingDir: {tmp}; Check: IsIA64 ; StatusMsg: "Installing USB drivers ...";
;Filename: {tmp}\dpinst\MultiLin\amd64\dpinst.exe; Parameters: "/F /LM /SW /PATH {win}\inf\CoherentDrivers\obis"; WorkingDir: {tmp}; Flags: 64bit ; Check: IsX64;  StatusMsg: "Installing USB drivers ..."
;Filename: {tmp}\dpinst\MultiLin\x86\dpinst.exe; Parameters: "/F /LM /SW /PATH {win}\inf\CoherentDrivers\obis"; WorkingDir: {tmp}; Flags: 32bit; Check: IsX86;  StatusMsg: "Installing USB drivers ..."
;Filename: {tmp}\dpinst\MultiLin\amd64\dpinst.exe; Parameters: "/F /LM /SW /PATH {win}\inf\CoherentDrivers\stingray"; WorkingDir: {tmp}; Flags: 64bit ; Check: IsX64;  StatusMsg: "Installing USB drivers ..."
;Filename: {tmp}\dpinst\MultiLin\x86\dpinst.exe; Parameters: "/F /LM /SW /PATH {win}\inf\CoherentDrivers\stingray"; WorkingDir: {tmp}; Flags: 32bit; Check: IsX86;  StatusMsg: "Installing USB drivers ..."

