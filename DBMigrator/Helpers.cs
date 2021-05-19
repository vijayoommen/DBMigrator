using System.Collections.Generic;
using System.Linq;

namespace DBMigrator
{
    public static class Helper
    {
        public static List<string> FindScripts(string primaryNamespace)
        {
            var assembly = typeof(Helper).Assembly;
            return assembly.GetManifestResourceNames().Where(x => x.StartsWith(primaryNamespace) && x.EndsWith(".sql")).ToList();
        }

    }
}
