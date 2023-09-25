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
using Tseesecake.Testing.Engine;
using Tseesecake.Modeling.Statements;

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
                    new Projection(new AggregationExpression(new MaxAggregation(new ColumnReference(ts.Measurements[0].Name))), "Maximum")
                }, null, null,
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
                Assert.That(measurement.Aggregation, Is.TypeOf<MaxAggregation>());
                Assert.That(measurement.Aggregation.Expression, Is.TypeOf<ColumnReference>());
                var column = measurement.Aggregation.Expression as ColumnReference ?? throw new InvalidCastException();
                Assert.That(column.Name, Is.EqualTo(ts.Measurements.ElementAt(0).Name));
            });
        }
    }
}
