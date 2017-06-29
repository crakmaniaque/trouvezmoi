## Introduction
This is a simple but powerful number finding game writen in C#.NET and open-source for your entertainement.
It has some interesting functionality such as multi-mode game, random game, custom game and stats management and saving.
The game is writen in French. Feel free to fork the code and provide an English version, I'll appreciate.

## Dependencies
Originaly, the game does not require any special librairies to work.

*As of version 0.10.0, it requires the CursesSharp library DLL*, which is a port of Unix curses for .NET. You can easily find it on the official website.

Executables provided in this repo are compiled with VS 2012 (and VS 2017 starting of 0.10.0) and target .NET Framework 2.0, so it's easily forwards compatible to .NET 3.5 and (possibily?) 4.0 and higher. The only possible problem with 4.0+ compatibility is the use of the `System.Windows.Forms.Application` interface for many uses. It could be easily fixed.

## More info
As of version 0.10.0, Windows Forms are no longer used and have been replaced with much convenient console dialogs.

Also as of 0.10.0, Trouvez-moi saves game statistics and achievements on disk using a local file. This file is located at `%HOMEPATH%\Documents\trouvezm0.sav` or whatever the `Environment.SpecialFolder.Personal` value is set. To reset the game, simply delete this file. If directory is unwritable, saving with silently fails.

## Known bugs
Starting from 0.10.0, when a `Console.CancelKeyPress` event is fired, Trouvez-moi attempts to get back to previous state if it's possible. To achieve that, it requires to input a line break each time CTRL+C is fired (it's easier for coding menus). This sometimes causes unexpected behaviour but doesn't crash the executable. I'll fix this soon.

Also, the game is not concepted to work in a PowerShell environment. For an unknown reason they're many random crashes at some points of the game when the executable is launched from PowerShell. Use cmd.exe instead.
