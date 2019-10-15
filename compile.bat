call "C:\Program Files\Mono\\bin\setmonopath.bat"
csc -target:library -platform:x64 -reference:System.Numerics.dll SGEMonoSample/Wrapper/* SGEMonoSample/Source/* -out:InternalMain.dll