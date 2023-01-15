@echo off
setlocal

rem Get the current directory of the install.bat file
set scriptDir=%~dp0

rem Set the 7dtd-build.bat file path
set appDir=%scriptDir%7dtd-build.bat

rem Check if the current directory is already in the PATH environment variable
set pathExist=false
for %%i in ("%PATH:;=" "%") do (
    if "%%~i"=="%appDir%" (
        set pathExist=true
        goto pathExistCheck
    )
)

:pathExistCheck
rem Add the current directory to the PATH environment variable if it doesn't exist
if "%pathExist%"=="false" (
    setx PATH "%PATH%;%appDir%"
)

rem Verify that the PATH environment variable was updated
echo %PATH%

endlocal
