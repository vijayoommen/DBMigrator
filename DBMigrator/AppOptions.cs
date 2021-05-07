using CommandLine;
using CommandLine.Text;

namespace DBMigrator
{
    internal class AppOptions
    {
        [Option(shortName:'r', longName:"rollback", Default = false, ResourceType = typeof(bool))]
        public bool Rollback { get; set; }

        [Option(shortName:'v', longName:"version", Default = 0L, ResourceType = typeof(long))]
        public long Version { get; set; }

        [Option(shortName:'l', longName:"list", Default = false, ResourceType = typeof(bool))]
        public bool List { get; set; }

    }
}