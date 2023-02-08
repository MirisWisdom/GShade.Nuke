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

namespace Miris.GShade.Nuke
{
  public static class Erase
  {
    /**
     * Recursively deletes the inbound directories from the filesystem.
     */
    public static void Commit(IEnumerable<DirectoryInfo> directories)
    {
      foreach (var directory in directories.Where(directory => directory.Exists))
        try
        {
          directory.Delete(true);
        }
        catch (Exception e)
        {
          WriteLine($"Could not delete {directory.Name} -- {e.Message} -- Try running as administrator.");
        }
    }

    /**
     * Deletes the inbound files from the filesystem.
     */
    public static void Commit(IEnumerable<FileInfo> files)
    {
      foreach (var file in files.Where(file => file.Exists))
        try
        {
          file.Delete();
        }
        catch (Exception e)
        {
          WriteLine($"Could not delete {file.Name} -- {e.Message} -- Try running as administrator.");
        }
    }
  }
}