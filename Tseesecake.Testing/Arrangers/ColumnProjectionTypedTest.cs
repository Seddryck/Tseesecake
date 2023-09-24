using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.ColumnExpressions;
using Tseesecake.Modeling.Statements.Projections;
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
                    new Projection(new ColumnReference(ts.Timestamp.Name))
                    , new Projection(new ColumnReference(ts.Facets[0].Name))
                    , new Projection(new ColumnReference(ts.Facets[1].Name))
                    , new Projection(new AggregationExpression(new MaxAggregation(ts.Measurements[0])), "Maximum")
                }
            );

            var arranger = new ProjectionTyped();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(4));
            Assert.That(statement.Projections.Where(x => x.Expression is ColumnReference).ToArray(), Has.Length.EqualTo(0));
            Assert.That(statement.Projections.Where(x => x.Expression is ColumnExpression).ToArray(), Has.Length.EqualTo(3));

            foreach (var projection in statement.Projections.Where(x => x.Expression is ColumnExpression)
                            .Select(x => x.Expression)
                            .Cast<ColumnExpression>())
                Assert.That(projection.Column, Is.TypeOf<Facet>().Or.TypeOf<Timestamp>());
        }
    }
}