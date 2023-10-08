using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Filters;
using LinqExp = System.Linq.Expressions;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Engine.Statements.Postgresql.Arrangers;

namespace Tseesecake.Testing.Engine.Statements.Postgresql.Arrangers
{
    public class PostgresqlExpressionForwardTest
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

            var arranger = new PostgresqlExpressionForward();
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
    }
}
