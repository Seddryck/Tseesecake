using Sprache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Modeling.Statements.WindowFunctions;
using Tseesecake.Modeling.Statements.Windows;

namespace Tseesecake.Parsing.Select
{
    internal class WindowParser
    {
        protected internal static Parser<ISlicer[]?> PartitionBys =
            from _ in Keyword.PartitionBy
            from slicers in SlicerParser.Facet.DelimitedBy(Parse.Char(','))
            select slicers.ToArray();

        protected internal static Parser<IOrderBy[]?> OrderBys =
            from _ in Keyword.OrderBy
            from orders in OrderByParser.OrderBy
            select orders.ToArray();

        protected internal static Parser<IWindow> InlineWindow =
            from over in Keyword.Over
            from lp in Parse.Char('(').Token()
            from partitions in PartitionBys.Optional()
            from orders in OrderBys.Optional()
            from rp in Parse.Char(')').Token()
            select new Window(
                partitions.IsDefined ? partitions.Get() : null
                , orders.IsDefined ? orders.Get() : null
            );

        protected internal static Parser<IWindow> ReferenceWindow =
            from over in Keyword.Over
            from reference in Grammar.Identifier
            select new ReferenceWindow(reference);

        protected internal static Parser<IWindow> Window =
            InlineWindow.Or(ReferenceWindow);

        public static Parser<WindowExpression> WindowExpression =
            from function in WindowFunctionParser.WindowFunction
            from window in Window
            select new WindowExpression(function, window);
    }
}
