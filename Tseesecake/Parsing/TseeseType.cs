using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Parsing
{
    internal class TseeseType
    {
        public static readonly Parser<string> Timestamp = Parse.IgnoreCase("Timestamp").Text().Token();
        public static readonly Parser<string> Facet = Parse.IgnoreCase("Facet").Text().Token();
        public static readonly Parser<string> Measurement = Parse.IgnoreCase("Measurement").Text().Token();
    }
}
