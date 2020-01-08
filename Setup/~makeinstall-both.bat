rem run inno compiler for both versions of app

set INNO=C:\Program Files (x86)\Inno Setup 5\Compil32.exe
set MAKE_DEBUG=MakeInstall Debug.iss
set MAKE_RELEASE=MakeInstall Release.iss
rem http://www.jrsoftware.org/ishelp/index.php?topic=compilercmdline

"%INNO%" /cc "%MAKE_DEBUG%"
"%INNO%" /cc "%MAKE_RELEASE%"
pause
