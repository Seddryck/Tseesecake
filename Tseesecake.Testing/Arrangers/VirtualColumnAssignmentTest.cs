using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.ColumnExpressions;
using Tseesecake.Querying;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;
using Tseesecake.Testing.Engine;
using LinqExpr = System.Linq.Expressions;

namespace Tseesecake.Testing.Arrangers
{
    public class VirtualColumnAssignmentTest
    {
        [Test]
        public void Execute_ColumnReference_ReplacedByDefinition()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var linqExpr = LinqExpr.Expression.Subtract(
                            LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                            LinqExpr.Expression.Parameter(typeof(double), "Produced"));
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new ColumnReferenceProjection(new ColumnReference("Accuracy"))
                },
                new VirtualMeasurement[] {
                    new VirtualMeasurement("Accuracy", linqExpr)
                }
            );

            var arranger = new VirtualColumnAssignment();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.Projections[0], Is.TypeOf<ColumnReferenceProjection>());

            var projection = statement.Projections[0] as ColumnReferenceProjection;
            Assert.That(projection!.Expression, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(projection.Alias, Is.EqualTo("Accuracy"));

            var expr = projection.Expression as VirtualColumnExpression;
            Assert.That(expr!.Expression, Is.EqualTo(linqExpr));
        }

        [Test]
        public void Execute_AggregationProjection_ReplacedByDefinition()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var linqExpr = LinqExpr.Expression.Subtract(
                            LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                            LinqExpr.Expression.Parameter(typeof(double), "Produced"));
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new AggregationProjection(new MaxAggregation(new ColumnExpression(new ColumnReference("Accuracy"))), "MinAccuracy")
                },
                new VirtualMeasurement[] {
                    new VirtualMeasurement("Accuracy", linqExpr)
                }
            );

            var arranger = new VirtualColumnAssignment();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.Projections[0], Is.TypeOf<AggregationProjection>());

            var projection = statement.Projections[0] as AggregationProjection;
            Assert.That(projection!.Aggregation.Expression, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(projection.Alias, Is.EqualTo("MinAccuracy"));

            var expr = projection.Aggregation.Expression as VirtualColumnExpression;
            Assert.That(expr!.Expression, Is.EqualTo(linqExpr));
        }
    }
}
