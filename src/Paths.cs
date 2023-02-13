/**
 * Copyright (C) 2023 Emilian Roman / Miris Wisdom
 * 
 * This file is part of GShade.Nuke.
 * 
 * GShade.Nuke is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 2 of the License, or
 * (at your option) any later version.
 * 
 * GShade.Nuke is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GShade.Nuke.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;
using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.Path;

namespace Miris.GShade.Nuke
{
  public static class Paths
  {
    public static readonly string GShade = "GShade";

    /**
     * Attempts to figure out the XIV game directory path using the following methods:
     * 
     * 1.  Retrieving the location path from the GShade registry key.
     * 2.  Asserting the existence of the game in the default non-Steam installation path.
     * 3.  Asserting the existence of the game in the current directory.
     */
    public static DirectoryInfo Game()
    {
      try
      {
        return Registry.Infer(Registry.Type.Game);
      }
      catch (Exception e)
      {
        WriteLine(e.Message + "-- Attempting next XIV game path detection method.");
      }

      var defaultPath = Combine(GetFolderPath(ProgramFilesX86),
        "SquareEnix",
        "FINAL FANTASY XIV - A Realm Reborn",
        "game");

      var executables = new List<FileInfo>
      {
        new(Combine(defaultPath,      "ffxiv.exe")),
        new(Combine(defaultPath,      "ffxiv_dx11.exe")),
        new(Combine(CurrentDirectory, "ffxiv.exe")),
        new(Combine(CurrentDirectory, "ffxiv_dx11.exe"))
      };

      var path = new DirectoryInfo(string.Empty);

      foreach (var executable in executables.Where(executable => executable.Exists))
        path = executable.Directory;

      if (path is not {Exists: true})
        throw new DirectoryNotFoundException("Could not find the XIV game directory.");

      return path;
    }

    /**
     * Attempts to figure out the GShade installation directory path using the following methods:
     * 
     * 1.  Retrieving the location path from the GShade registry key.
     * 2.  Asserting the existence of assets in the default installation path.
     */
    public static DirectoryInfo Root()
    {
      try
      {
        return Registry.Infer(Registry.Type.Install);
      }
      catch (Exception e)
      {
        WriteLine(e.Message + "-- Attempting next GShade installation path detection method.");
      }

      var defaultPath = Combine(GetFolderPath(ProgramFiles),
        "GShade");

      var assets = new List<FileInfo>
      {
        new(Combine(defaultPath,      "GShade32.dll")),
        new(Combine(defaultPath,      "GShade64.dll")),
        new(Combine(CurrentDirectory, "inject32.exe")),
        new(Combine(CurrentDirectory, "inject75.exe"))
      };

      var path = new DirectoryInfo(string.Empty);

      foreach (var asset in assets.Where(asset => asset.Exists))
        path = asset.Directory;

      if (path is not {Exists: true})
        throw new DirectoryNotFoundException("Could not find the GShade installation directory.");

      return path;
    }

    /**
     * Principal Post-Processing directories, along with presets. These are the most important user files!
     */
    public static List<DirectoryInfo> Shaders(DirectoryInfo root, DirectoryInfo game)
    {
      return new List<DirectoryInfo>
      {
        new(Combine(root.FullName, $"{GShade.ToLower()}-shaders")),
        new(Combine(game.FullName, $"{GShade.ToLower()}-presets"))
      };
    }

    /**
     * Installation directories, including the core directory and other system directories.
     */
    public static List<DirectoryInfo> Installation(DirectoryInfo root)
    {
      return new List<DirectoryInfo>
      {
        root,
        new(Combine(GetFolderPath(CommonApplicationData), GShade)),
        new(Combine(GetFolderPath(CommonPrograms),        GShade))
      };
    }

    /**
     * DLL files used for injection/hooking of GShade (technically ReShade) onto FFXIV or any other supported game.
     */
    public static List<FileInfo> Injections(DirectoryInfo game)
    {
      return new List<FileInfo>
      {
        new(Combine(game.FullName, "dxgi.dll")),
        new(Combine(game.FullName, "d3d11.dll")),
        new(Combine(game.FullName, "dinput8.dll"))
      };
    }

    /**
     * Miscellaneous files such as shortcuts, installation logs and other stuff.
     */
    public static List<FileInfo> Miscellaneous(DirectoryInfo game)
    {
      return new List<FileInfo>
      {
        new(Combine(GetFolderPath(CommonDesktopDirectory), $"{GShade} Control Panel.lnk")),
        new(Combine(GetFolderPath(CommonDesktopDirectory), $"{GShade} Custom Shaders & Textures.lnk")),
        new(Combine(GetFolderPath(CommonDesktopDirectory), $"{GShade} FAQ.url")),

        new(Combine(game.FullName, "GShade.ini")),
        new(Combine(game.FullName, "GSInstLog.txt")),
        new(Combine(game.FullName, "notification.wav"))
      };
    }
  }
}