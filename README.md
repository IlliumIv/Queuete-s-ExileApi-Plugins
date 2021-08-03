# ExileApi
An introduction can be found in the forum thread:
https://www.ownedcore.com/forums/mmo/path-of-exile/poe-bots-programs/940986-exileapi-3-14-release.html#post4300263

# Release
https://github.com/Queuete/ExileApi/releases

# Troubleshooting
1. The Hud wont start.
- Windows 10 is the only supported system
- download and install .NET 4.8 runtime https://dotnet.microsoft.com/downloa...-web-installer
- turn of your firewall and antivirus and try again. (Thanks @bobTheBuilder69)
- make sure the 64x PoE client is already running
- make sure you use the standalone client (NOT Steam, NOT Epic Launcher, NOT Taiwan, NOT Tencent)
- make sure you have VC++ Redistributable Runtime installed (https://support.microsoft.com/en-us/...al-c-downloads) (Thanks @snowhawk)

2. The Hud stopped working and presents an error when trying to start it.
- check if your config/settings.json file got corrupted. If thats the case you can replace the content with the config/dumpSettings.json file or completly delete it (then default settings will be generated).

3. There are visual offsets in rendering minion dots and everything other.
Windows Display options -> Scale and layout -> set to 100%
If that one does not work: Right Click Loader.exe > Properties > Compatibility > Change high DPI settings > Override high DPI scaling behavior > Application
(Thanks @Unknown_B)

4. MinimapIcons wont work.
- The IconBuilder plugin needs to be activated aswell.
- Change the Map Zoom slider in Poe -> Options -> UI -> Map Zoom (Thanks @JustRandomPlayer)

5. AutoQuit wont work.
You need to run the hud as admin for it to work.

6. PickIt and Stashie are very slow.
Check which Windows 10 version you are using. Win10 2004 is slow, reverting back to Win10 1909 solves this. (Thanks @uumas)

7. The Hug laggs, especially MinimapIcons and HealthBars
Probably one of your other plugins is very performance intense. Currently EliteBar is often the problem.

8. The Hud and SlimTrade wont work together
Slimtrade has the option to show a Config Button on the screen for easy configuration. If you disable the config button then the focus does not get stolen. (Thanks @Smartillian)

## Troubleshooting did not solve my issue
Create an issue with a detailed error description. Post the full error log (PoeHelper/Logs/Verbose-[date].log)
-> Posts about problems which dont do this will be ignored by me.

# How To Setup a Developer Version
Please look into the base repository the needed software is described there, including some troubleshooting https://github.com/Qvin0000/ExileApi

To use this version you need .net4.8 and git. 
Git https://git-scm.com/downloads
.NET 4.8 https://dotnet.microsoft.com/download/thank-you/net48

1. Create a PoeHUD (any name) folder
2. Open a git bash inside the folder
3. Run `git clone https://github.com/Queuete/ExileApi`
4. Open the solution file in Visual Studio
5. This should compile already, there could be some reference errors, those are mostly fixed by removing the reference (solution exlporer-plugin_project->references) the ones with warnings need to be readded

-> Adding plugins (example DevTree)

6. Open git bash in "ExileApi/Plugins"
7. Run `git clone https://github.com/Queuete/DevTree`
8. Open Visual Studio, right click the "ExileApi" solution in the "Solution Explorer". -> Add -> New Solution Folder (name it "Plugins")
9. Right click created folder -> Add -> Existing Project -> "ExileApi/Plugins/Devtree/DevTree.csproj"


Tools used for reverse engineering are mostly: ReClass.NET, CheatEngine, Ghidra
When you want to learn something about it, get comfortable with at least the first tool or second tool and try to comprehend already updated offsets. (For 3.10 I advise you to start with the LifeComponent)

