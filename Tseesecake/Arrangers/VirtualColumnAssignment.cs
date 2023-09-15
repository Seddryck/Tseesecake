using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Arrangers
{
    [Polyglot]
    internal class VirtualColumnAssignment : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            ExecuteColumnReferenceProjection(statement);
            ExecuteAggregationProjection(statement);
        }

        public void ExecuteColumnReferenceProjection(SelectStatement statement)
        {
            var projections = statement.Projections
                .Where(x => x is ColumnReferenceProjection).Cast<ColumnReferenceProjection>()
                .Where(x => statement.VirtualMeasurements.Select(y => y.Name).Contains(((ColumnExpression)x.Expression).Reference.Name));

            if (!projections.Any())
                return;

            foreach (var projection in statement.Projections)
            {
                if (projections.Contains(projection))
                    ((ColumnReferenceProjection)projection).Expression
                        = new VirtualColumnExpression(statement.VirtualMeasurements.Single(x => 
                            x.Name == ((ColumnExpression)((ColumnReferenceProjection)projection).Expression).Reference.Name
                    ).Expression!);
            }
        }

        public void ExecuteAggregationProjection(SelectStatement statement)
        {
            var projections = statement.Projections
                .Where(x => x is AggregationProjection).Cast<AggregationProjection>()
                .Where(x => statement.VirtualMeasurements.Select(y => y.Name).Contains(((ColumnExpression)x.Aggregation.Expression).Reference.Name));

            if (!projections.Any())
                return;

            foreach (var projection in statement.Projections)
            {
                if (projections.Contains(projection))
                    ((AggregationProjection)projection).Aggregation.Expression
                        = new VirtualColumnExpression(statement.VirtualMeasurements.Single(x =>
                            x.Name == ((ColumnExpression)((AggregationProjection)projection).Aggregation.Expression).Reference.Name
                    ).Expression!);
            }
        }
    }
}
