# GShade Nuke Utility

In light of the [recent](https://gamerant.com/final-fantasy-14-gshade-mod-twitter-trend-developer-malware/) [GShade](https://www.fanbyte.com/games/news/gshade-malware-controversy-ffxiv-third-party-tool/) ["malware" incident](https://piunikaweb.com/2023/02/07/psa-gshade-contains-malware-install-reshade-before-uninstalling/), this project aims to completely uninstall GShade from your system in a thorough yet reliable manner.

1. It will completely back up all of the GShade data to an archive, for preservation and restoration; and
2. It will completely erase the installation files from your system, to ensure that no traces of it remain.

## Usage

1. Download the [latest release](https://github.com/MirisWisdom/GShade.Nuke/releases/latest) of the program. Updates won't be enforced on you!
2. Double click the program and it will automatically start backing up your stuff and proceed with the uninstallation.
3. During the uninstallation phase, you will be asked to confirm that the data which will be deleted is safe to delete. Type `yes` to do so.

## Backup

Prior to erasure, all of your GShade data will be backed up to a ZIP archive. You will see four folders in it:

| Name   | Description                                           |
| ------ | ----------------------------------------------------- |
| `main` | The shaders and presets -- this is what matters most! |
| `hook` | DLL files used by GShade (ReShade) to inject onto XIV |
| `core` | Installation files scattered across the filesystem    |
| `misc` | Shortcuts and other goodies which depend on GShade    |

The `main` folder is the one you'll care about the most. It contains your shaders and presets, and if you want to migrate to ReShade, you can use the files stored in it.

## Plans

1. A rudimentary GUI to avoid spooky black screens.
2. ReShade installer? Or at least a migration process?

## Rationale

The official GShade uninstaller has two caveats:

1. It seemingly requires you to be updated to the latest version of GShade, thereby opening up a potential attack vector to further malware; and
2. Whether it's a partial uninstallation or unwanted side-effects, there is no guarantee that the uninstaller will do one thing to the fullest extent: remove the GShade files.
