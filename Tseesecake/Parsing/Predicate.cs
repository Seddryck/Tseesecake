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

        public static readonly Parser<string> Equal = Parse.IgnoreCase("Equal").Text().Or(Parse.IgnoreCase("=").Text()).Token();
        public static readonly Parser<string> Different = Parse.IgnoreCase("Equal").Text().Or(Parse.IgnoreCase("<>").Text()).Or(Parse.IgnoreCase("!=").Text()).Token();
        public static readonly Parser<string> In = Parse.IgnoreCase("In").Text().Token();

        public static readonly Parser<string> Dicer = Equal.Or(Different).Or(In);
    }
}
