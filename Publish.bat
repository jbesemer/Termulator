set DEST=C:\Program Files (x86)\Coherent\Termulator\
set BIN=Termulator\bin\Debug
set PLAYERBIN=C:\Program Files (x86)\SoundPlayer\
set SOUNDS=C:\Windows\Media\Sounds\SmallSounds\

copy "%BIN%\Termulator.exe" "%DEST%" || goto :error
copy "%BIN%\Termulator.exe.config" "%DEST%" || goto :error
copy "%BIN%\Termulator.pdf" "%DEST%" || goto :error

"%PLAYERBIN%Player.exe" "%SOUNDS%DRIP.WAV"

goto :exit

:error
"%PLAYERBIN%Player.exe" "%SOUNDS%CLANK.WAV"

:exit
pause
