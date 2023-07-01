using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Parsing
{
    internal class Keyword
    {
        public static readonly Parser<string> Create = Parse.IgnoreCase("Create").Text().Token();
        public static readonly Parser<string> Or = Parse.IgnoreCase("Or").Text().Token();
        public static readonly Parser<string> Replace = Parse.IgnoreCase("Replace").Text().Token();
        public static readonly Parser<string> Timeseries = Parse.IgnoreCase("Timeseries").Text().Token();
        public static readonly Parser<string> Import = Parse.IgnoreCase("Import").Text().Token();
        public static readonly Parser<string> From = Parse.IgnoreCase("From").Text().Token();
        public static readonly Parser<string> File = Parse.IgnoreCase("File").Text().Token();
        public static readonly Parser<string> As = Parse.IgnoreCase("As").Text().Token();
        public static readonly Parser<string> Select = Parse.IgnoreCase("Select").Text().Token();
    }
}
