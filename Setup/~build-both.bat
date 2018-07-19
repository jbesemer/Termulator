rem set MSVC=C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com
rem set MSVC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe"
rem "2017 !!NOT!! Community or 2015"
set MSVC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\devenv.com

set PROJECT=..\Termulator.sln

"%MSVC%" "%PROJECT%" /rebuild debug
"%MSVC%" "%PROJECT%" /rebuild release

pause
