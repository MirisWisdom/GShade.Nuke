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
using System.IO.Compression;
using System.Linq;
using static System.Console;
using static System.IO.Compression.ZipArchiveMode;
using static System.IO.Compression.ZipFile;
using static System.IO.Path;
using static System.IO.SearchOption;

namespace Miris.GShade.Nuke
{
  public static class Archive
  {
    /**
     * Creates/updates the inbound archive with all the files contained in the inbound directories.
     *
     * Considerations:
     *
     * 1.  Each directory's name will be used as a prefix for its file paths in the archive:
     *
     *     Filesystem: C:\Path\To\{DIRECTORY}\Parent\Of\File.ext
     *     In Archive:            {DIRECTORY}\Parent\Of\File.ext
     *
     * 2.  Directories with the name of GShade will use its *parent* directory as a prefix:
     *
     *     Filesystem: C:\Path\To\{PARENT}\[GShade]\Parent\Of\File.ext
     *     In Archive:            {PARENT}\[GShade]\Parent\Of\File.ext
     *
     *     This is to handle the scenario where multiple GShade directories are provided in the list, for example, the
     *     Program Files GShade directory and Program Data GShade directory. If we don't do this, the files would be
     *     merged together and thereby corrupt the tree structure.
     *
     * The inbound prefix will be unconditionally prepended to the path. An empty string should be provided to not use a
     * prefix.
     */
    public static void Create(FileInfo archive, IEnumerable<DirectoryInfo> directories, string prefix)
    {
      using var a = Open(archive.FullName, Update);

      foreach (var directory in directories.Where(directory => directory.Exists))
      {
        var parent = directory.Name;

        if (directory.Name == Paths.GShade)
          parent = Combine(directory.Parent?.Name ?? string.Empty, directory.Name);

        foreach (var file in directory.GetFiles("*", AllDirectories).Where(file => file.Exists))
        {
          var path = GetRelativePath(directory.FullName, file.FullName);
          a.CreateEntryFromFile(file.FullName, Combine(prefix, parent, path));
          WriteLine($"Archived GShade file '{file.Name}' to '{prefix}' section in the archive.");
        }
      }
    }

    /**
     * Creates/updates the inbound archive with the the inbound files as the entries.
     */
    public static void Create(FileInfo archive, IEnumerable<FileInfo> files, string prefix)
    {
      using var a = Open(archive.FullName, Update);
      foreach (var file in files.Where(file => file.Exists))
      {
        a.CreateEntryFromFile(file.FullName, Combine(prefix, file.Name));
        WriteLine($"Archived GShade file '{file.Name}' to '{prefix}' section in the archive.");
      }
    }
  }
}