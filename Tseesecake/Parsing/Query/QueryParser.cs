using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Parsing.Query
{
    internal class QueryParser
    {
        protected internal readonly static Parser<Timeseries> TimeseriesReference =
            from identifier in Grammar.Identifier
            select new TimeseriesReference(identifier);

        public readonly static Parser<SelectStatement> Query =
            from @select in Keyword.Select
            from projections in ProjectionParser.Projection.DelimitedBy(Parse.Char(','))
            from @from in Keyword.From
            from ts in TimeseriesReference
            select new SelectStatement(ts, projections.ToArray());
    }
}
