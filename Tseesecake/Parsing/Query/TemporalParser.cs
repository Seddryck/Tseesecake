using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Parsing.Query
{
    internal class TemporalParser
    {
        private static Parser<Timestamp> BucketBy =
            from bucket in Keyword.Bucket
            from id in Grammar.Identifier.Optional()
            from byKeyword in Keyword.By
            select id.IsDefined ? new Timestamp(id.Get()) : throw new NotImplementedException();

        private static Parser<T> CreateEnumParser<T>()
        {
            var values = new List<string[]>();
            foreach (var name in Enum.GetNames(typeof(T)).Union(Enum.GetNames(typeof(T)).Select(x => x.Concat("s"))))
            {
                var builder = new StringBuilder();
                foreach (char c in name)
                {
                    if (char.IsUpper(c))
                        builder.Append(' ');
                    builder.Append(c);
                }
                values.Add(builder.ToString().Split(" ", StringSplitOptions.RemoveEmptyEntries));
            }

            Parser<string>? full = null;
            foreach (var value in values)
            {
                Parser<string>? parser = null;
                foreach (var p in value)
                {
                    var word = Parse.IgnoreCase(p).Text().Token();
                    parser = parser?.Concat(word).Text() ?? word;
                }
                full = full?.Or(parser) ?? parser;
            }
            var enumParser = full.Select(x => (T)Enum.Parse(typeof(T), x, true));
            return enumParser;
        }

        protected internal static readonly Parser<TruncationTemporal> Truncation = CreateEnumParser<TruncationTemporal>();

        protected internal static readonly Parser<CyclicTemporal> Cyclic = CreateEnumParser<CyclicTemporal>();

        protected internal static Parser<TemporalSlicer> TruncationParser =
            from ts in BucketBy
            from trunc in Truncation
            select new TruncationTemporalSlicer(ts, trunc);

        protected internal static Parser<TemporalSlicer> CyclicParser =
            from ts in BucketBy
            from cyclic in Cyclic
            select new CyclicTemporalSlicer(ts, cyclic);

        public static Parser<TemporalSlicer> Slicer = CyclicParser.Or(TruncationParser);
    }
}
