set PLAYERBIN=C:\Program Files (x86)\SoundPlayer\
set SOUNDS=C:\Windows\Media\Sounds\SmallSounds\
set DEST=C:\Program Files (x86)\Coherent\Termulator\
set BIN=Termulator\bin\Debug
set DOC=Doc

xcopy /y %BIN%\Termulator.exe "%DEST%" 		|| goto :error
xcopy /y %BIN%\Library.dll "%DEST%" 			|| goto :error
xcopy /y %BIN%\Termulator.exe.config "%DEST%" 	|| goto :error
xcopy /y %DOC%\Termulator.pdf "%DEST%" 		|| goto :error

"%PLAYERBIN%Player.exe" "%SOUNDS%DRIP.WAV"

goto :exit

:error
"%PLAYERBIN%Player.exe" "%SOUNDS%BUZZ.WAV"

:exit
pause
