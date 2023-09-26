using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Testing.Engine;
using LinqExp = System.Linq.Expressions;

namespace Tseesecake.Testing.Arrangers
{
    public class ExpressionForwardedToOrderByClauseTest
    {
        [Test]
        public void Execute_ThreeColumnReferences_ThreeTypedColumns()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new Projection(new AggregationExpression(new MaxAggregation(new ColumnExpression(ts.Measurements.ElementAt(0)))), "Maximum")
                }
                , null, null, null, null, null,
                new IOrderBy[] {
                    new ColumnOrder(new ColumnReference("Maximum"))
                }
            );

            var arranger = new ExpressionForwardedToOrderByClause();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.Orders, Has.Count.EqualTo(1));
            Assert.That(statement.Orders[0], Is.TypeOf(typeof(ColumnOrder)));
            var orderBy = statement.Orders[0] as ColumnOrder ?? throw new InvalidCastException();
            Assert.That(orderBy.Expression, Is.TypeOf(typeof(AggregationExpression)));
            var expression = orderBy.Expression as AggregationExpression ?? throw new InvalidCastException();
            Assert.Multiple(() =>
            {
                Assert.That(expression.Aggregation, Is.TypeOf<MaxAggregation>());
                Assert.That(expression.Aggregation.Expression, Is.TypeOf(typeof(ColumnExpression)));
                var column = expression.Aggregation.Expression as ColumnExpression ?? throw new InvalidCastException();
                Assert.That(column.Name, Is.EqualTo(ts.Measurements.ElementAt(0).Name));
            });
        }
    }
}
