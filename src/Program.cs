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
using static System.DateTime;
using static Miris.GShade.Nuke.Archive;

namespace Miris.GShade.Nuke
{
  internal class Program
  {
    private static readonly OptionSet OptionSet = new()
    {
      {
        "game=|ffxiv=", "Path to the FFXIV game directory.", s => { Game = new DirectoryInfo(s); }
      },
      {
        "root=|install=", "Path to the GShade game directory.", s => { Root = new DirectoryInfo(s); }
      },
      {
        "backup=|archive=", "Path to the backup archive", s => { Backup = s; }
      }
    };

    private static DirectoryInfo Root   { get; set; } = Registry.Infer(Registry.Type.Install);
    private static DirectoryInfo Game   { get; set; } = Registry.Infer(Registry.Type.Game);
    private static string        Backup { get; set; } = $"{Paths.GShade}.{Now:yyyy-MM-dd-hh-mm-ss}.zip";

    private static void Main(string[] args)
    {
      OptionSet.WriteOptionDescriptions(Out);
      OptionSet.Parse(args);
      
      Create(new FileInfo(Backup), Paths.Shaders(Root, Game), "main");
      Create(new FileInfo(Backup), Paths.Installation(Root),  "core");
      Create(new FileInfo(Backup), Paths.Injections(Game),    "hook");
      Create(new FileInfo(Backup), Paths.Miscellaneous(Game), "misc");
    }
  }
}