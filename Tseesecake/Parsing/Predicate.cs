using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Parsing
{
    internal class Predicate
    {
        public static readonly Parser<string> After = Parse.IgnoreCase("After").Text().Token();
        public static readonly Parser<string> Before = Parse.IgnoreCase("Before").Text().Token();
        public static readonly Parser<string> Range = Parse.IgnoreCase("Range").Text().Token();
        public static readonly Parser<string> Since = Parse.IgnoreCase("Since").Text().Token();

        public static readonly Parser<string> Temporizer = After.Or(Before).Or(Range).Or(Since);
    }
}
