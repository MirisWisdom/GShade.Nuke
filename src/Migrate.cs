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

using System.IO;
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;

namespace Miris.GShade.Nuke
{
  public static class Migrate
  {
    /**
     * Migrates gshade-shaders to reshade-shaders.
     */
    public static void Shaders(DirectoryInfo game)
    {
      var shadersReshade = new DirectoryInfo(Combine(game.FullName, "reshade-shaders"));
      var shadersGshade  = new DirectoryInfo(Combine(game.FullName, "gshade-shaders"));

      Rename(shadersGshade, shadersReshade);
    }

    /**
     * Migrates gshade-presets to reshade-presets.
     */
    public static void Presets(DirectoryInfo game)
    {
      var presetsReshade = new DirectoryInfo(Combine(game.FullName, "reshade-presets"));
      var presetsGshade  = new DirectoryInfo(Combine(game.FullName, "gshade-presets"));

      Rename(presetsGshade, presetsReshade);
    }

    /**
     * Renames the inbound subject directory to the inbound replacement directory.
     * If the inbound replacement directory exists, it will be renamed with a ".bak" suffix.
     */
    private static void Rename(FileSystemInfo subject, FileSystemInfo replacement)
    {
      if (!replacement.Exists)
      {
        Move(replacement.FullName, $"{replacement.FullName}.bak");
        WriteLine($"Backed up the original '{replacement.Name}' directory.");
      }

      if (!subject.Exists)
        throw new DirectoryNotFoundException($"Cannot rename '{subject.Name}'. Directory does not exist.");

      Move(subject.FullName, replacement.FullName);
      WriteLine($"Renamed directory '{subject.Name}' to '{replacement.Name}'.");
    }
  }
}