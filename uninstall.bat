echo off
rem echo %~dp0

rem delete
@reg delete "HKEY_CLASSES_ROOT\*\shell\link" /f
@reg delete "HKEY_CLASSES_ROOT\Directory\shell\link" /f
@reg delete "HKEY_CLASSES_ROOT\*\ContextMenus\links" /f
@reg delete "HKEY_CLASSES_ROOT\*\ContextMenus\bg_links" /f
@reg delete "HKEY_CLASSES_ROOT\Directory\Background\shell\link" /f

@reg delete "HKEY_CURRENT_USER\Software\LinkSettings" /f
pause