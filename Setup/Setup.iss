; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "StopWatch"
#define MyAppVersion "3.6.0.2"
#define MyAppPublisher "MRB"
#define MyAppExeName "stopwatch.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{C0E02AE8-9900-45CA-A233-119C1C433D7C}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={localappdata}\{#MyAppName}-app
;DisableDirPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename=StopWatch-{#MyAppVersion}
SetupIconFile=..\stopwatch\watch1.ico
UninstallDisplayIcon="{app}\setup.ico"
Compression=lzma/ultra
SolidCompression=yes       
 OutputDir=".\"      
; OutputDir="B:\stopwatch\Updates\"
; OutputDir="d:\stopwatch\Updates\"
PrivilegesRequired=lowest

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl" 

[Dirs]  
Name: "{app}"; Permissions: users-full; 

[Files]
Source: "..\stopwatch\bin\Release\stopwatch.exe"; DestDir: "{app}"; Flags: ignoreversion  
Source: "..\stopwatch\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\stopwatch\bin\Release\*.ttf"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\stopwatch\setup.ico"; DestDir: "{app}"; Flags: ignoreversion 
 
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]  
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"   
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{sendto}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Parameters:"/sendto"; WorkingDir: "{app}";

[Run]
;Filename: "{cmd}"; Parameters:"/c """"{app}\{#MyAppName}"" /host borhan"""; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; WorkingDir: "{app}"; Flags: runhidden runasoriginaluser nowait runasoriginaluser
Filename: "{app}\{#MyAppName}.exe"; Parameters:"/host borhan"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; WorkingDir: "{app}"; Flags:  runasoriginaluser nowait runasoriginaluser
