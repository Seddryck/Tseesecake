using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements;
using Tseesecake.Parsing.Query;

namespace Tseesecake.Parsing.Meta
{
    internal class TimeseriesMetaParser
    {
        public readonly static Parser<IShowStatement> ShowFieldsTimeseries =
            from @show in Keyword.Show
            from @from in Keyword.Timeseries
            from ts in Grammar.Identifier
            select new ShowFieldsTimeseries(ts);

        public readonly static Parser<IShowStatement> ShowAllTimeseries =
            from @show in Keyword.Show
            from @from in Keyword.Timeseries
            select new ShowAllTimeseries();

        public readonly static Parser<IShowStatement> Show = ShowFieldsTimeseries.Or(ShowAllTimeseries);
    }
}
