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
using Mono.Options;
using static System.Console;
using static System.DateTime;
using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.File;
using static System.IO.Path;
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
      },
      {
        "force", "Forcefully erase files without prompting. WARNING: DANGEROUS!", s => { Force = s != null; }
      },
      {
        "migrate", "Attempt GShade to ReShade migration. WARNING: EXPERIMENTAL!", s => { Migrate = s != null; }
      }
    };

    private static DirectoryInfo Root  { get; set; } = Registry.Infer(Registry.Type.Install);
    private static DirectoryInfo Game  { get; set; } = Registry.Infer(Registry.Type.Game);
    private static bool          Force { get; set; }
    private static bool          Migrate { get; set; }

    private static string Backup { get; set; } =
      Combine(GetFolderPath(Desktop), $"{Paths.GShade}.{Now:yyyy-MM-dd-hh-mm-ss}.zip");

    private static void Uninstall()
    {
      WriteLine("The following directories and files will be deleted:");

      foreach (var info in Paths.Installation(Root))
        WriteLine($"-   {info.FullName}");

      foreach (var info in Paths.Injections(Root))
        WriteLine($"-   {info.FullName}");

      foreach (var info in Paths.Miscellaneous(Root))
        WriteLine($"-   {info.FullName}");

      string confirmation = string.Empty;

      while (!confirmation.Equals("yes") && !Force)
      {
        WriteLine("Please type YES to confirm the deletion:");
        confirmation = ReadLine() ?? string.Empty;
        confirmation = confirmation.ToLower();
      }

      Erase.Commit(Paths.Installation(Root));
      Erase.Commit(Paths.Injections(Game));
      Erase.Commit(Paths.Miscellaneous(Game));
    }

    private static void Archive()
    {
      Create(new FileInfo(Backup), Paths.Shaders(Root, Game), "main");
      Create(new FileInfo(Backup), Paths.Installation(Root),  "core");
      Create(new FileInfo(Backup), Paths.Injections(Game),    "hook");
      Create(new FileInfo(Backup), Paths.Miscellaneous(Game), "misc");
    }

    private static void Rename()
    {
      if (!Migrate)
        return;

      Nuke.Migrate.Presets(Game);
      Nuke.Migrate.Shaders(Game);
    }

    private static void Main(string[] args)
    {
      WriteLine("GSHADE NUKE TOOL // MIRIS WISDOM");
      WriteLine("================================");
      WriteLine("github - miriswisdom/gshade.nuke");

      OptionSet.WriteOptionDescriptions(Out);
      OptionSet.Parse(args);

      try
      {
        Archive();
        Uninstall();
        Rename();
      }
      catch (Exception e)
      {
        var log = Combine(GetFolderPath(Desktop), "GShade.Nuke.log");
        AppendAllText(log, e.StackTrace);
        AppendAllText(log, e.InnerException.StackTrace);
        WriteLine($"An error has occurred: {e.Message}. Refer to the log file for more details: {log}");
      }

      WriteLine("Press any key to continue...");
      ReadLine();
    }
  }
}
