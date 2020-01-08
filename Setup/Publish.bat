set DEST=C:\Program Files (x86)\Coherent\Termulator
set BIN=Termulator\bin\Debug
set DOC=DOC
set PLAYERBIN=C:\Program Files (x86)\SoundPlayer\
set SOUNDS=C:\Windows\Media\Sounds\SmallSounds\
set COPY=xcopy /y

cd ..

%COPY% "%BIN%\Termulator.exe" "%DEST%" 		|| goto :error
%COPY% "%BIN%\Termulator.exe.config" "%DEST%"	|| goto :error
%COPY% "%BIN%\SharedLibrary.dll" "%DEST%" 	|| goto :error
%COPY% "%BIN%\Library.dll" "%DEST%"			|| goto :error
%COPY% "%BIN%\Newtonsoft.Json.dll" "%DEST%"	|| goto :error
%COPY% "%BIN%\Newtonsoft.Json.xml" "%DEST%"	|| goto :error
%COPY% "%DOC%\Termulator.pdf" "%DEST%"		|| goto :error

"%PLAYERBIN%Player.exe" "%SOUNDS%DRIP.WAV"

goto :exit

:error
"%PLAYERBIN%Player.exe" "%SOUNDS%CLANK.WAV"

:exit
pause
