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

namespace Tseesecake.Arrangers
{
    [Polyglot]
    internal class ProjectionTyped : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.Timeseries is not Timeseries)
                throw new InvalidOperationException();

            var projections = statement.Projections
                .Where(x => x.Expression is ColumnReference);
            
            if (!projections.Any())
                return;

            foreach (var projection in statement.Projections)
            {
                if (projections.Contains(projection))
                    projection.Expression
                        = new ColumnExpression(
                                (Column)((Timeseries)statement.Timeseries).Columns.Single(x =>
                                    x.Name == ((ColumnReference)projection.Expression).Name
                            ));
            }
        }
    }
}
