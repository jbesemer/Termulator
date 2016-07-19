set timestamp=%DATE:/=-%@%TIME::=.%
set timestamp=%timestamp: =%
set ZIPNAME=Termulator From Home %timestamp%.zip
set ZIPPER=C:\Program Files\7-Zip\7z.exe
set DEST=C:\Users\jb\Google Drive\Software
set PLAYERBIN=C:\Program Files (x86)\SoundPlayer\
set SOUNDS=C:\Windows\Media\Sounds\SmallSounds\

"%PLAYERBIN%Player.exe" "%SOUNDS%DRIP.WAV"

del "%ZIPNAME%" || goto :error
"%ZIPPER%" a "%ZIPNAME%" . -xr!bin -xr!obj -xr!*.exe -xr!*.zip -xr!"Distribution Image" || goto :error
copy "%ZIPNAME%" "%DEST%" || goto :error
del "%ZIPNAME%" || goto :error

"%PLAYERBIN%Player.exe" "%SOUNDS%DING.WAV"

goto :exit

:error
"%PLAYERBIN%Player.exe" "%SOUNDS%BUZZ.WAV"

:exit
pause
