@echo off
REM Check for admin rights
net session >nul 2>&1
if ERRORLEVEL 1 GOTO AdminRightsRequired

REM Check if the dir already exists
dir /AD "%~dp0EcoDevViewDummyServer\bin\webroot" >nul 2>&1

if ERRORLEVEL 1 GOTO CreateSymlink

echo %~dp0EcoDevViewDummyServer\bin\webroot already exists. Please remove the directory and re-run this batch file.
pause
exit 2

:CreateSymlink
echo Create symlink...
mklink /D "%~dp0EcoDevViewDummyServer\bin\webroot" "%~dp0EcoDevView\webroot"
if ERRORLEVEL 1 GOTO SymlinkError
pause
exit 0

:SymlinkError
echo Error creating the symlink. Please consult mklink's output for further information.
pause
exit 3

:AdminRightsRequired
echo To create a symlink admin rights are required. Shift-Rightclick this batch file and select "Run as Administrator".
pause
exit 1