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
using Tseese = Tseesecake.Querying.Expressions;

namespace Tseesecake.Parsing.Query
{
    internal class OrderByParser
    {
        private static readonly Parser<IOrderBy> OrderByColumn =
            from column in Grammar.Identifier
            from asc in Keyword.Asc.Or(Keyword.Desc).Optional()
            from first in Keyword.Nulls.Then(_ => Keyword.First.Or(Keyword.Last)).Optional()
            select new ColumnOrder(
                    new ColumnReference(column), asc.GetOrElse(Sorting.Ascending), first.GetOrElse(NullSorting.First)
                );

        public static Parser<IEnumerable<IOrderBy>> OrderBy = OrderByColumn.DelimitedBy(Parse.Char(','));
    }
}
