using System.Linq.Expressions;
using Sprache;
using System.Diagnostics.Tracing;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Mounting;

namespace Tseesecake.Parsing.Dml
{
    internal class DmlParser
    {
        public static Parser<Column> Timestamp =
            from identifier in Grammar.Identifier
            from _ in TseeseType.Timestamp
            select new Timestamp(identifier);

        public static Parser<Column> Measurement =
            from identifier in Grammar.Identifier
            from _ in TseeseType.Measurement
            select new Measurement(identifier);

        public static Parser<Column> Facet =
            from identifier in Grammar.Identifier
            from _ in TseeseType.Facet
            select new Facet(identifier);

        protected static Parser<string> TimeseriesIdentifier =
            from create in Keyword.Create
            from or in Keyword.Or
            from replace in Keyword.Replace
            from ts in Keyword.Timeseries
            from name in Grammar.Identifier
            select name;

        protected static Parser<Column> Column = Timestamp.Or(Facet).Or(Measurement);
        protected static Parser<IEnumerable<Column>> Columns = Column.DelimitedBy(Parse.Char(','));

        protected static Parser<Timeseries> BasicTimeseries =
            from name in TimeseriesIdentifier
            from lpar in Parse.Char('(').Token()
            from columns in Columns
            from rpar in Parse.Char(')').Token()
            select new Timeseries(name
                , (Timestamp)columns.Single(x => x is Timestamp) 
                , columns.Where(x => x is Measurement).Cast<Measurement>().ToArray()
                , columns.Where(x => x is Facet).Cast<Facet>().ToArray()
            );

        protected static Parser<IFileSource> FileSource =
            from import in Keyword.Import
            from @from in Keyword.From
            from file in Keyword.File
            from path in Grammar.SingleQuotedTextual
            select new FileSource(path, new Dictionary<string, object>());

        protected static Parser<Timeseries> FileTimeseries =
            from name in TimeseriesIdentifier
            from lpar in Parse.Char('(').Token()
            from columns in Columns
            from rpar in Parse.Char(')').Token()
            from file in FileSource
            select new FileTimeseries(name
                , (Timestamp)columns.Single(x => x is Timestamp)
                , columns.Where(x => x is Measurement).Cast<Measurement>().ToArray()
                , columns.Where(x => x is Facet).Cast<Facet>().ToArray()
                , file
            );

        public static Parser<Timeseries> Timeseries = 
            from ts in FileTimeseries.Or(BasicTimeseries)
            from terminator in Grammar.Terminator.Many().Optional().End()
            select ts;
    }
}
