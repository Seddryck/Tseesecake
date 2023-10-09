using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Filters;
using LinqExp = System.Linq.Expressions;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Engine.Statements.MsSqlServer.Arrangers;

namespace Tseesecake.Testing.Engine.Statements.MsSqlServer.Arrangers
{
    public class MsSqlServerExpressionForwardTest
    {
        [Test]
        public void Execute_ProjectionAsGroupFilter_Forwarded()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new Projection(new AggregationExpression(new MaxAggregation(new ColumnReference(ts.Measurements[0].Name))), "Maximum")
                }, null, null,
                new IFilter[] {
                    new GathererSifter(new ColumnReference("Maximum"), LinqExp.Expression.LessThan,  5)
                }
            );

            var arranger = new MsSqlServerExpressionForward();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.GroupFilters, Has.Count.EqualTo(1));
            Assert.That(statement.GroupFilters[0], Is.TypeOf<GathererSifter>());
            var sifter = (statement.GroupFilters[0] as GathererSifter)!;
            Assert.That(sifter.Expression, Is.TypeOf<AggregationExpression>());
            var expr = (sifter.Expression as AggregationExpression)!;
            Assert.Multiple(() =>
            {
                Assert.That(expr.Aggregation, Is.TypeOf<MaxAggregation>());
                Assert.That(expr.Aggregation.Expression, Is.TypeOf<ColumnReference>());
                var column = (expr.Aggregation.Expression as ColumnReference)!;
                Assert.That(column.Name, Is.EqualTo(ts.Measurements.ElementAt(0).Name));
            });
        }

        [Test]
        public void Execute_ProjectionAsOrderBy_Forwarded()
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

            var arranger = new MsSqlServerExpressionForward();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.Orders, Has.Count.EqualTo(1));
            Assert.That(statement.Orders[0], Is.TypeOf<ColumnOrder>());
            var orderBy = (statement.Orders[0] as ColumnOrder)!;
            Assert.That(orderBy.Expression, Is.TypeOf<AggregationExpression>());
            var expression = (orderBy.Expression as AggregationExpression)!;
            Assert.Multiple(() =>
            {
                Assert.That(expression.Aggregation, Is.TypeOf<MaxAggregation>());
                Assert.That(expression.Aggregation.Expression, Is.TypeOf(typeof(ColumnExpression)));
                Assert.That(((ColumnExpression)expression.Aggregation.Expression).Name, Is.EqualTo(ts.Measurements.ElementAt(0).Name));
            });
        }
    }
}
