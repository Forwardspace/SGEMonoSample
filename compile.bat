call "C:\Program Files\Mono\\bin\setmonopath.bat"
csc -target:library -reference:System.Numerics.dll SGEMonoSample/Wrapper/* SGEMonoSample/Source/* -out:InternalMain.dll