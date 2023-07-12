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
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;
using Tseesecake.Testing.Engine;

namespace Tseesecake.Testing.Arrangers
{
    public class ColumnProjectionTypedTest
    {
        [Test]
        public void Execute_ThreeColumnReferences_ThreeTypedColumns()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new ColumnProjection(new ColumnReference(ts.Timestamp.Name))
                    , new ColumnProjection(new ColumnReference(ts.Facets.ElementAt(0).Name))
                    , new ColumnProjection(new ColumnReference(ts.Facets.ElementAt(1).Name))
                    , new AggregationProjection(new MaxAggregation(new ColumnExpression(ts.Measurements.ElementAt(0))), "Maximum")
                }
            );

            var arranger = new ColumnProjectionTyped();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(4));
            Assert.That(statement.Projections.Where(x => x is ColumnProjection).ToArray(), Has.Length.EqualTo(3));
            foreach (var projection in statement.Projections.Where(x => x is ColumnProjection).Cast<ColumnProjection>())
            {
                Assert.That(projection.Expression, Is.TypeOf<ColumnExpression>());
                Assert.That(((ColumnExpression)projection.Expression).Column, Is.Not.TypeOf<ColumnReference>());
                Assert.That(((ColumnExpression)projection.Expression).Column, Is.TypeOf<Facet>().Or.TypeOf<Timestamp>());
            }
        }
    }
}
