using System;
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
            var command = string.Join(' ', args);
            //command = "--list --runMigration";
            parser = new Parser(settings => { 
                settings.EnableDashDash = true; 
                settings.CaseSensitive = false;  
                settings.AutoHelp = true; 
            });

            var result = parser.ParseArguments<AppOptions>(command.Split(' ')).WithParsed(RunWithOptions);

            result.WithNotParsed(errors =>
                {
                    var helptext = HelpText.AutoBuild(result, h => {
                        h.AdditionalNewLineAfterOption = false;
                        h.AddNewLineBetweenHelpSections = false;
                        h.Heading = "DB Migration Utility";
                        h.Copyright = "Western Alliance Bank - Slalom";
                        return HelpText.DefaultParsingErrorsHandler(result, h); });
                    Console.WriteLine(helptext);
                });
        }

        private static void RunWithOptions(AppOptions options)
        {
            if (options.List)
            {
                MigrationRunner.ListMigrations();
            }

            if (options.Preview)
            {
                MigrationRunner.Preview();
            }

            if (options.Rollback && options.Version > 0)
            {
                MigrationRunner.MigrateDown(options.Version);
            }

            if (options.RunMigration)
            {
                MigrationRunner.MigrateUp(options.Version);
            }
            
        }
    }
}