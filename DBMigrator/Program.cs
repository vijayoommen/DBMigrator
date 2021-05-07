﻿using System;
using CommandLine;
using System.Collections.Generic;
using CommandLine.Text;

namespace DBMigrator
{
    class Program
    {
        private static Parser parser { get; set; }

        static void Main(string[] args)
        {
            parser = new Parser(settings => { settings.EnableDashDash = true; settings.CaseSensitive = false;  settings.AutoHelp = true; });
            var result = parser.ParseArguments<AppOptions>(args)
                .WithParsed(RunWithOptions);

            result.WithNotParsed(errors =>
                {
                    var helptext = HelpText.AutoBuild(result, h => { return HelpText.DefaultParsingErrorsHandler(result, h); });
                    Console.WriteLine(helptext);
                });
        }

        private static void RunWithOptions(AppOptions options)
        {
            if (options.List)
            {
                MigrationRunner.ListMigrations();
            }

            if (options.Rollback && options.Version > 0)
            {
                MigrationRunner.MigrateDown(options.Version);
                return;
            }

            MigrationRunner.MigrateUp();
        }
    }
}