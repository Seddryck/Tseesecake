﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Projections;
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
                    new Projection(new ColumnReference("Accuracy"))
                },
                new VirtualMeasurement[] {
                    new VirtualMeasurement("Accuracy", linqExpr)
                }
            );

            var arranger = new VirtualColumnAssignment();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));

            var projection = statement.Projections[0];
            Assert.That(projection.Expression, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(projection.Alias, Is.EqualTo("Accuracy"));

            var expr = projection.Expression as VirtualColumnExpression;
            Assert.That(expr!.Expression, Is.EqualTo(linqExpr));
        }

        [Test]
        public void Execute_Projection_ReplacedByDefinition()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var linqExpr = LinqExpr.Expression.Subtract(
                            LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                            LinqExpr.Expression.Parameter(typeof(double), "Produced"));
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new Projection(new AggregationExpression(new MaxAggregation(new ColumnReference("Accuracy"))), "MinAccuracy")
                },
                new VirtualMeasurement[] {
                    new VirtualMeasurement("Accuracy", linqExpr)
                }
            );

            var arranger = new VirtualColumnAssignment();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.Projections[0].Expression, Is.TypeOf<AggregationExpression>());

            var aggregation = statement.Projections[0].Expression as AggregationExpression;
            Assert.That(aggregation!.Aggregation.Expression, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(statement.Projections[0].Alias, Is.EqualTo("MinAccuracy"));

            var expr = aggregation.Aggregation.Expression as VirtualColumnExpression;
            Assert.That(expr!.Expression, Is.EqualTo(linqExpr));
        }
    }
}
