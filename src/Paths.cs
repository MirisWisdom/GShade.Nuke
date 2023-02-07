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

    public static readonly List<FileInfo> Shortcuts = new()
    {
      new FileInfo(Combine(GetFolderPath(CommonDesktopDirectory), $"{GShade} Control Panel.lnk")),
      new FileInfo(Combine(GetFolderPath(CommonDesktopDirectory), $"{GShade} Custom Shaders & Textures.lnk")),
      new FileInfo(Combine(GetFolderPath(CommonDesktopDirectory), $"{GShade} FAQ.url"))
    };

    public static readonly List<DirectoryInfo> Directories = new()
    {
      new DirectoryInfo(Combine(GetFolderPath(CommonApplicationData), GShade)),
      new DirectoryInfo(Combine(GetFolderPath(CommonPrograms),        GShade)),
      new DirectoryInfo(Combine(GetFolderPath(ProgramFiles),          GShade)),
      new DirectoryInfo(Combine(GetFolderPath(ProgramFilesX86),       GShade))
    };

    public static List<FileInfo> Injections()
    {
      return Injections(Infer.Path(Infer.Type.Game));
    }

    public static List<FileInfo> Injections(DirectoryInfo root)
    {
      return new List<FileInfo>
      {
        new(Combine(root.FullName, "dxgi.dll")),
        new(Combine(root.FullName, "d3d11.dll")),
        new(Combine(root.FullName, "dinput8.dll"))
      };
    }

    public static DirectoryInfo Presets()
    {
      return Presets(Infer.Path(Infer.Type.Game));
    }

    public static DirectoryInfo Presets(DirectoryInfo root)
    {
      return new DirectoryInfo(Combine(root.FullName, "gshade-presets"));
    }
  }
}