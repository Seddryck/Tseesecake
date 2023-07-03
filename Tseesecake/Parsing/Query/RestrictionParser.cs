using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Restrictions;
using Tseese = Tseesecake.Querying.Expressions;

namespace Tseesecake.Parsing.Query
{
    internal class RestrictionParser
    {
        private static Parser<int> BaseLimit =
            from limit in Keyword.Limit
            from count in Parse.Number.Select(int.Parse)
            select count;

        private static Parser<IRestriction> Limit = BaseLimit.Select(x => new LimitRestriction(x));

        private static Parser<IRestriction> Offset =
            from limit in BaseLimit
            from offset in Keyword.Offset
            from count in Parse.Number.Select(int.Parse)
            select new LimitOffsetRestriction(limit, count);

        public static Parser<IRestriction> Restriction = Offset.Or(Limit);
    }
}
