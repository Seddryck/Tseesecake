using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Querying;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Arrangers
{
    [Polyglot]
    internal class ColumnReferenceProjectionTyped : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.Timeseries is not Timeseries)
                throw new InvalidOperationException();

            var projections = statement.Projections
                .Where(x => x is ColumnReferenceProjection).Cast<ColumnReferenceProjection>()
                .Where(x => x.Expression is ColumnExpression);
            if (!projections.Any())
                return;

            foreach (var projection in statement.Projections)
            {
                if (projections.Contains(projection))
                    ((ColumnExpression)((ColumnReferenceProjection)projection).Expression).Reference
                        = (statement.Timeseries as Timeseries).Columns.Single(x =>
                            x.Name == ((ColumnExpression)((ColumnReferenceProjection)projection).Expression).Reference.Name
                    );
            }
        }
    }
}
