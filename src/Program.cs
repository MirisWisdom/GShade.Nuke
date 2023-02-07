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
using Mono.Options;
using static System.Console;

namespace Miris.GShade.Nuke
{
  internal class Program
  {
    private static readonly OptionSet OptionSet = new()
    {
      {
        "game=|root=", "Path to the FFXIV game directory.", s =>
        {
          if (new DirectoryInfo(s).Exists)
            Root = s;
        }
      }
    };

    private static string Root { get; set; }

    private static void Main(string[] args)
    {
      OptionSet.WriteOptionDescriptions(Out);
      OptionSet.Parse(args);

      // TODO: Backup and uninstallation logic!
    }
  }
}