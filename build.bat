@ECHO off

set "modname=AntonHat"
set "fromrelativepath=bin\Debug\netstandard2.0"
set "destination=D:\Program Files (x86)\Steam\steamapps\common\Brutal Orchestra\BepInEx\plugins"

dotnet build

echo:
@echo Copying files...
copy "%~dp0\%fromrelativepath%\%modname%.dll" "%destination%"

echo:
@echo Done!
pause