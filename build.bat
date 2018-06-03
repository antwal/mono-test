@echo off

if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" (
	FOR /F "delims=" %%E in ('"%ProgramFiles(x86)%\Microsoft Visual Studio\installer\vswhere.exe" -latest -property installationPath') DO (
		set "MSBUILD_EXE=%%E\MSBuild\15.0\Bin\MSBuild.exe"
		if exist "!MSBUILD_EXE!" goto :build
	)
)

FOR %%E in (Enterprise, Professional, Community) DO (
	set "MSBUILD_EXE=%ProgramFiles(x86)%\Microsoft Visual Studio\2017\%%E\MSBuild\15.0\Bin\MSBuild.exe"
	if exist "!MSBUILD_EXE!" goto :build
)

REM Couldn't be located in the standard locations, expand search
FOR /F "delims=" %%E IN ('dir /b /ad "%ProgramFiles(x86)%\Microsoft Visual Studio\"') DO (
	set "MSBUILD_EXE=%ProgramFiles(x86)%\Microsoft Visual Studio\%%E\MSBuild\15.0\Bin\MSBuild.exe"
	if exist "!MSBUILD_EXE!" goto :build

	FOR /F "delims=" %%F IN ('dir /b /ad "%ProgramFiles(x86)%\Microsoft Visual Studio\%%E"') DO (
		set "MSBUILD_EXE=%ProgramFiles(x86)%\Microsoft Visual Studio\%%E\%%F\MSBuild\15.0\Bin\MSBuild.exe"
		if exist "!MSBUILD_EXE!" goto :build
	)
)

echo Could not find MSBuild 15
exit /b 1

:build

REM git submodule sync || goto :error
REM git submodule update --init --recursive || goto :error
REM "external\nuget-binary\NuGet.exe" restore Main.sln || goto :error

REM "%MSBUILD_EXE%" external\fsharpbinding\.paket\paket.targets /t:RestorePackages /p:PaketReferences="%~dp0external\fsharpbinding\MonoDevelop.FSharpBinding\paket.references"

set "CONFIG=Debug"
set "PLATFORM=Any CPU"

rem only perform integrated restore on RefactoringEssentials, it fails on the whole solution
REM "%MSBUILD_EXE%" external\RefactoringEssentials\RefactoringEssentials.sln /target:Restore %* || goto :error

REM "%MSBUILD_EXE%" mono-test\mono-test.sln /bl:MonoDevelop.binlog /m "/p:Configuration=%CONFIG%" "/p:Platform=%PLATFORM%" %* || goto :error
REM  & "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" .\mono-test.sln /m /p:Configuration=Release /p:Platform="Any CPU"
"%MSBUILD_EXE%" mono-test\mono-test.sln /m "/p:Configuration=%CONFIG%" "/p:Platform=%PLATFORM%" %* || goto :error
goto :eof

:error

for %%x in (%CMDCMDLINE%) do if /i "%%~x" == "/c" pause
exit /b %ERRORLEVEL%