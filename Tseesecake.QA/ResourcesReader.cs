using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.QA
{
    internal static class ResourcesReader
    {
        private static string Read(string resource)
        {
            var resourceName = $"{Assembly.GetExecutingAssembly().GetName().Name}.Resources.{resource}.sql";
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
                ?? throw new FileNotFoundException(resourceName);
            using StreamReader sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }

        public static string BucketBy
            => Read(nameof(BucketBy));
        public static string ImplicitGroupBy
            => Read(nameof(ImplicitGroupBy));
    }
}
