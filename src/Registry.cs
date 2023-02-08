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
using System.IO;
using static Microsoft.Win32.Registry;

namespace Miris.GShade.Nuke
{
  public static class Registry
  {
    public enum Type
    {
      Game,
      Install
    }

    public static DirectoryInfo Infer(Type type)
    {
      var key = LocalMachine.OpenSubKey(@"SOFTWARE\\GShade");

      if (key == null)
        throw new InvalidOperationException("Could not infer path: GShade registry sub-key does not exist.");

      string? value;

      switch (type)
      {
        case Type.Game:
          value = key.GetValue("lastexepath") as string;

          if (value == null)
            throw new InvalidOperationException("Could not infer game path: key does not exist.");

          var path = new FileInfo(value).Directory;

          if (path == null)
            throw new InvalidOperationException("Could not infer game path: key value is an invalid path.");

          return path;
        case Type.Install:
          value = key.GetValue("instdir") as string;

          if (value == null)
            throw new InvalidOperationException("Could not infer install path: key does not exist.");

          return new DirectoryInfo(value);
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
    }
  }
}