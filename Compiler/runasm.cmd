call "c:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"
copy test.asm ..\..\..\Test\ASMRunner\test.asm
msbuild ..\..\..\Test\ASMRunner\ASMRunner.vcxproj /t:Rebuild /p:Configuration=Debug
..\..\..\Test\ASMRunner\Debug\ASMRunner.exe
