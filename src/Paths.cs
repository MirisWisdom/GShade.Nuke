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

using System.Collections.Generic;
using System.IO;
using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.Path;

namespace Miris.GShade.Nuke
{
  public static class Paths
  {
    public static readonly string GShade = "GShade";

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