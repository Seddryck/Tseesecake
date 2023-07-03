using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Parsing.Query
{
    internal class QueryParser
    {
        protected internal readonly static Parser<Timeseries> TimeseriesReference =
            from identifier in Grammar.Identifier
            select new TimeseriesReference(identifier);

        public readonly static Parser<IFilter[]> Filters =
            from @where in Keyword.Where
            from filters in FilterParser.Filter.DelimitedBy(Parse.IgnoreCase("AND"))
            select filters.ToArray();

        public readonly static Parser<IOrderBy[]> OrderBys =
            from keyword in Keyword.OrderBy
            from orderBys in OrderByParser.OrderBy
            select orderBys.ToArray();

        public readonly static Parser<SelectStatement> Query =
            from @select in Keyword.Select
            from projections in ProjectionParser.Projection.DelimitedBy(Parse.Char(','))
            from @from in Keyword.From
            from ts in TimeseriesReference
            from filters in Filters.Optional()
            from orderBys in OrderBys.Optional()
            from restriction in RestrictionParser.Restriction.Optional()
            select new SelectStatement(ts, projections.ToArray(), filters.GetOrElse(null), null, null, null, null, orderBys.GetOrElse(null), restriction.GetOrElse(null));
    }
}
