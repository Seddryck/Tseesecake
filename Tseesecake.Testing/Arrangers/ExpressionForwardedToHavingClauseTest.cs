using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Aggregations;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Projections;
using Tseesecake.Testing.Engine;
using LinqExp = System.Linq.Expressions;

namespace Tseesecake.Testing.Arrangers
{
    public class ExpressionForwardedToHavingClauseTest
    {
        [Test]
        public void Execute_ThreeColumnReferences_ThreeTypedColumns()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new AggregationProjection(new MaxAggregation(new ColumnExpression(ts.Measurements.ElementAt(0))), "Maximum")
                }
                , null, null,
                new IFilter[] {
                    new GathererSifter(new Measurement("Maximum"), LinqExp.Expression.LessThan,  5)
                }
            );

            var arranger = new ExpressionForwardedToHavingClause();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.GroupFilters, Has.Count.EqualTo(1));
            Assert.That(statement.GroupFilters[0], Is.TypeOf(typeof(GathererSifter)));
            var sifter = statement.GroupFilters[0] as GathererSifter ?? throw new InvalidCastException();
            Assert.That(sifter.Measurement, Is.TypeOf(typeof(AggregationMeasurement)));
            var measurement = sifter.Measurement as AggregationMeasurement ?? throw new InvalidCastException();
            Assert.Multiple(() =>
            {
                Assert.That(measurement.Aggregation.Name, Is.EqualTo("max"));
                Assert.That(measurement.Aggregation.Expression, Is.TypeOf(typeof(ColumnExpression)));
                var column = measurement.Aggregation.Expression as ColumnExpression ?? throw new InvalidCastException();
                Assert.That(column.Reference.Name, Is.EqualTo(ts.Measurements.ElementAt(0).Name));
            });
        }
    }
}
