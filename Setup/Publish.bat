set DEST=C:\Program Files (x86)\Coherent\Termulator
set BIN=Termulator\bin\Debug
set DOC=DOC
set PLAYERBIN=C:\Program Files (x86)\SoundPlayer\
set SOUNDS=C:\Windows\Media\Sounds\SmallSounds\

cd ..

copy "%BIN%\Termulator.exe" "%DEST%" || goto :error
copy "%BIN%\Termulator.exe.config" "%DEST%" || goto :error
copy "%BIN%\SharedLibrary.dll" "%DEST%" || goto :error
copy "%BIN%\Library.dll" "%DEST%" || goto :error
copy "%BIN%\Newtonsoft.Json.dll" "%DEST%" || goto :error
copy "%BIN%\Newtonsoft.Json.xml" "%DEST%" || goto :error
copy "%DOC%\Termulator.pdf" "%DEST%" || goto :error

"%PLAYERBIN%Player.exe" "%SOUNDS%DRIP.WAV"

goto :exit

:error
"%PLAYERBIN%Player.exe" "%SOUNDS%CLANK.WAV"

:exit
pause
