echo off
rem echo %~dp0

rem file
@reg add "HKEY_CLASSES_ROOT\*\shell\link" /f /v MUIVerb /t REG_SZ /d "�ļ�����"
@reg add "HKEY_CLASSES_ROOT\*\shell\link" /f /v ExtendedSubCommandsKey /t REG_SZ /d "*\ContextMenus\links"

rem directory
@reg add "HKEY_CLASSES_ROOT\Directory\shell\link" /f /v MUIVerb /t REG_SZ /d "�ļ�����"
@reg add "HKEY_CLASSES_ROOT\Directory\shell\link" /f /v ExtendedSubCommandsKey /t REG_SZ /d "*\ContextMenus\links"


rem select file
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\links\Shell\001cmd" /f /v MUIVerb /t REG_SZ /d "ѡ��"
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\links\Shell\001cmd\command" /f /t REG_SZ /d "%~dp0Link.exe select -p \"%%1\""

@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\links\Shell\005seperator" /f /v CommandFlags /t REG_DWORD /d 00000008

rem create file link
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\links\Shell\006cmd" /f /v MUIVerb /t REG_SZ /d "����"
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\links\Shell\006cmd\command" /f /t REG_SZ /d "%~dp0Link.exe create -p \"%%1\""

rem create file link (admin)
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\links\Shell\007cmd" /f /v MUIVerb /t REG_SZ /d "����(����ԱȨ��)"
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\links\Shell\007cmd\command" /f /t REG_SZ /d "%~dp0Linkex.exe create -p \"%%1\""



rem Background
@reg add "HKEY_CLASSES_ROOT\Directory\Background\shell\link" /f /v MUIVerb /t REG_SZ /d "�ļ�����"
@reg add "HKEY_CLASSES_ROOT\Directory\Background\shell\link" /f /v ExtendedSubCommandsKey /t REG_SZ /d "*\ContextMenus\bg_links"


rem select file
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\bg_links\Shell\001cmd" /f /v MUIVerb /t REG_SZ /d "ѡ��"
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\bg_links\Shell\001cmd\command" /f /t REG_SZ /d "%~dp0Link.exe select -p \"%%V\""

@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\bg_links\Shell\005seperator" /f /v CommandFlags /t REG_DWORD /d 00000008

rem create file link
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\bg_links\Shell\006cmd" /f /v MUIVerb /t REG_SZ /d "����"
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\bg_links\Shell\006cmd\command" /f /t REG_SZ /d "%~dp0Link.exe create -p \"%%V\""

rem create file link (admin)
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\bg_links\Shell\007cmd" /f /v MUIVerb /t REG_SZ /d "����(����ԱȨ��)"
@reg add "HKEY_CLASSES_ROOT\*\ContextMenus\bg_links\Shell\007cmd\command" /f /t REG_SZ /d "%~dp0Linkex.exe create -p \"%%V\""

pause