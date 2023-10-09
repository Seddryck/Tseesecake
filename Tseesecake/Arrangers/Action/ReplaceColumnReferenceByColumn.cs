using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Projections;
using System.Net.Sockets;

namespace Tseesecake.Arrangers.Action
{
    [Polyglot]
    internal class ReplaceColumnReferenceByColumn : IActionArranger<IExpression>
    {
        protected Column[] Columns { get; }

        public ReplaceColumnReferenceByColumn(SelectStatement statement)
            => Columns = ((Timeseries)statement.Timeseries).Columns.Cast<Column>().ToArray();

        public IExpression Execute(IExpression expr)
        {
            if(expr is ColumnReference reference)
            {
                if (Columns.Any(x => x.Name == reference.Name))
                    expr = new ColumnExpression(
                            Columns.Single(x => x.Name == reference.Name)
                        );
            }
            return expr;
        }

        public object Execute(object obj)
        {
            if (obj is IExpression expr)
                obj = Execute(expr);
            return obj;
        }
    }
}
