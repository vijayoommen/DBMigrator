using CommandLine;
using CommandLine.Text;

namespace DBMigrator
{
    public class AppOptions
    {
        [Option(shortName:'r', longName:"rollback", Default = false, ResourceType = typeof(bool))]
        public bool Rollback { get; set; }

        [Option(shortName:'v', longName:"version", Default = 0L, ResourceType = typeof(long))]
        public long Version { get; set; }

        [Option(shortName:'l', longName:"list", Default = false, ResourceType = typeof(bool))]
        public bool List { get; set; }

        [Option(shortName:'p', longName:"preview", Default = false, ResourceType = typeof(bool))]
        public bool Preview { get; set; }

        [Option(shortName:'r', longName:"runMigration", Default = false, ResourceType = typeof(bool))]
        public bool RunMigration { get; set; }
    }
}