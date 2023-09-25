using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Projections;

namespace Tseesecake.Arrangers
{
    [Polyglot]
    internal class VirtualColumnAssignment : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            ExecuteProjectionForColumnReference(statement);
            ExecuteProjectionForAggregation(statement);
        }

        public void ExecuteProjectionForColumnReference(SelectStatement statement)
        {
            var projections = statement.Projections
                .Where(p => p.Expression is ColumnReference)
                .Where(x => statement.VirtualMeasurements.Select(y => y.Name)
                                .Contains(((ColumnReference)x.Expression).Name));

            if (!projections.Any())
                return;

            foreach (var projection in statement.Projections)
            {
                if (projections.Contains(projection))
                    ((Projection)projection).Expression
                        = new VirtualColumnExpression(statement.VirtualMeasurements.Single(x => 
                            x.Name == ((ColumnReference)((Projection)projection).Expression).Name
                    ).Expression!);
            }
        }

        public void ExecuteProjectionForAggregation(SelectStatement statement)
        {
            var projections = statement.Projections
                .Where(p => p.Expression is AggregationExpression)
                .Where(x => statement.VirtualMeasurements.Select(y => y.Name)
                                .Contains(((IColumn)((AggregationExpression)x.Expression).Aggregation.Expression).Name));

            if (!projections.Any())
                return;

            foreach (var projection in statement.Projections)
            {
                if (projections.Contains(projection))
                    ((AggregationExpression)projection.Expression).Aggregation.Expression
                        = new VirtualColumnExpression(statement.VirtualMeasurements.Single(x =>
                            x.Name == ((ColumnReference)((AggregationExpression)projection.Expression).Aggregation.Expression).Name
                    ).Expression!);
            }
        }
    }
}
