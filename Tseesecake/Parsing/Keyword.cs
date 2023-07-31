using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Ordering;

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
        public static readonly Parser<string> Where = Parse.IgnoreCase("Where").Text().Token();
        public static readonly Parser<string> Not = Parse.IgnoreCase("Not").Text().Token();
        public static readonly Parser<string> OrderBy = Parse.IgnoreCase("Order").Text().Token().Then(_ => Parse.IgnoreCase("By").Text().Token()).Return("Order By");
        public static readonly Parser<Sorting> Asc = Parse.IgnoreCase("Asc").Text().Token().Return(Sorting.Ascending);
        public static readonly Parser<Sorting> Desc = Parse.IgnoreCase("Desc").Text().Token().Return(Sorting.Descending);
        public static readonly Parser<string> Nulls = Parse.IgnoreCase("Nulls").Text().Token();
        public static readonly Parser<NullSorting> First = Parse.IgnoreCase("First").Text().Token().Return(NullSorting.First);
        public static readonly Parser<NullSorting> Last = Parse.IgnoreCase("Last").Text().Token().Return(NullSorting.Last);
        public static readonly Parser<string> Limit = Parse.IgnoreCase("Limit").Text().Token();
        public static readonly Parser<string> Offset = Parse.IgnoreCase("Offset").Text().Token();
        public static readonly Parser<string> GroupBy = Parse.IgnoreCase("Group").Text().Token().Then(_ => Parse.IgnoreCase("By").Text().Token()).Return("Group By");
        public static readonly Parser<string> Bucket = Parse.IgnoreCase("Bucket").Text().Token();
        public static readonly Parser<string> By = Parse.IgnoreCase("By").Text().Token();
        public static readonly Parser<string> With = Parse.IgnoreCase("With").Text().Token();
        public static readonly Parser<string> Measurement = Parse.IgnoreCase("Measurement").Text().Token();
    }
}
