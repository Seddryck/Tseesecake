using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Engine.Statements.Common.Arrangers;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Modeling.Statements.WindowFunctions;
using Tseesecake.Modeling.Statements.Windows;
using Tseesecake.Testing.Engine;
using LinqExpr = System.Linq.Expressions;

namespace Tseesecake.Testing.Engine.Statements.Common.Arrangers
{
    public class VirtualMeasurementForwardTest
    {
        [Test]
        public void Execute_ProjectionColumnReference_ReplacedByDefinition()
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

            var arranger = new VirtualMeasurementForward();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));

            var projection = statement.Projections[0];
            Assert.That(projection.Expression, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(projection.Alias, Is.EqualTo("Accuracy"));

            var expr = projection.Expression as VirtualColumnExpression;
            Assert.That(expr!.Expression, Is.EqualTo(linqExpr));
        }

        [Test]
        public void Execute_ProjectionAggregation_ReplacedByDefinition()
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

            var arranger = new VirtualMeasurementForward();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.Projections[0].Expression, Is.TypeOf<AggregationExpression>());

            var aggregation = statement.Projections[0].Expression as AggregationExpression;
            Assert.That(aggregation!.Aggregation.Expression, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(statement.Projections[0].Alias, Is.EqualTo("MinAccuracy"));

            var expr = aggregation.Aggregation.Expression as VirtualColumnExpression;
            Assert.That(expr!.Expression, Is.EqualTo(linqExpr));
        }

        [Test]
        public void Execute_ProjectionWindowFunction_ReplacedByDefinition()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var linqExpr = LinqExpr.Expression.Subtract(
                            LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                            LinqExpr.Expression.Parameter(typeof(double), "Produced"));
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new Projection(new WindowExpression(
                        new MaxAggregation(new ColumnReference("Accuracy"))
                        , new Window(new[] { new FacetSlicer(new Facet("WindPark")) }, new[] { new ColumnOrder(new ColumnReference("Instant")) }, null)
                    )
                    , "MinAccuracy")
                },
                new VirtualMeasurement[] {
                    new VirtualMeasurement("Accuracy", linqExpr)
                }
            );

            var arranger = new VirtualMeasurementForward();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.Projections[0].Expression, Is.TypeOf<WindowExpression>());

            var windowExpr = statement.Projections[0].Expression as WindowExpression;
            Assert.That(windowExpr!.WindowFunction, Is.TypeOf<MaxAggregation>());
            var expr = ((IAggregation)windowExpr.WindowFunction).Expression;
            Assert.That(expr, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(((VirtualColumnExpression)expr).Expression, Is.EqualTo(linqExpr));

            Assert.That(statement.Projections[0].Alias, Is.EqualTo("MinAccuracy"));
        }

        [Test]
        public void Execute_ProjectionWindowOrderBy_ReplacedByDefinition()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var linqExpr = LinqExpr.Expression.Subtract(
                            LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                            LinqExpr.Expression.Parameter(typeof(double), "Produced"));
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new Projection(new WindowExpression(
                        new MaxAggregation(new ColumnReference("Produced"))
                        , new Window(null, new[] {new ColumnOrder(new ColumnReference("Accuracy")) }, null)
                    )
                    , "MinAccuracy")
                },
                new VirtualMeasurement[] {
                    new VirtualMeasurement("Accuracy", linqExpr)
                }
            );

            var arranger = new VirtualMeasurementForward();
            arranger.Execute(statement);

            Assert.That(statement.Projections, Has.Count.EqualTo(1));
            Assert.That(statement.Projections[0].Expression, Is.TypeOf<WindowExpression>());

            var window = statement.Projections[0].Expression as WindowExpression;
            Assert.That(window!.Window, Is.TypeOf<Window>());
            var orderBys = ((Window)window!.Window).OrderBys;
            Assert.That(orderBys, Is.Not.Null);
            Assert.That(orderBys, Has.Length.EqualTo(1));
            var expr = ((ColumnOrder)orderBys[0]).Expression;
            Assert.That(expr, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(((VirtualColumnExpression)expr).Expression, Is.EqualTo(linqExpr));

            Assert.That(statement.Projections[0].Alias, Is.EqualTo("MinAccuracy"));
        }

        [Test]
        public void Execute_OrderBy_ReplacedByDefinition()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var linqExpr = LinqExpr.Expression.Subtract(
                            LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                            LinqExpr.Expression.Parameter(typeof(double), "Produced"));
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new Projection(new ColumnReference("Accuracy")
                    , "MinAccuracy")
                },
                null, null, null, null, null,
                new IOrderBy[] {
                    new ColumnOrder(new ColumnReference("Accuracy"))
                }, null,
                new VirtualMeasurement[] {
                    new VirtualMeasurement("Accuracy", linqExpr)
                }
            );

            var arranger = new VirtualMeasurementForward();
            arranger.Execute(statement);

            Assert.That(statement.Orders, Is.Not.Null);
            Assert.That(statement.Orders, Has.Count.EqualTo(1));
            Assert.That(((ColumnOrder)statement.Orders[0]).Expression, Is.TypeOf<VirtualColumnExpression>());
            var expr = ((ColumnOrder)statement.Orders[0]).Expression;
            Assert.That(expr, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(((VirtualColumnExpression)expr).Expression, Is.EqualTo(linqExpr));
        }

        [Test]
        public void Execute_Filter_ReplacedByDefinition()
        {
            var ts = DmlStatementDefinition.WindEnergy;
            var linqExpr = LinqExpr.Expression.Subtract(
                            LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                            LinqExpr.Expression.Parameter(typeof(double), "Produced"));
            var statement = new SelectStatement(ts,
                new IProjection[] {
                    new Projection(new ColumnReference("Accuracy")
                    , "MinAccuracy")
                },
                new IFilter[] {
                    new GathererSifter(new ColumnReference("Accuracy"), LinqExpr.Expression.LessThan, 5)
                }
                , null, null, null, null, null, null,
                new VirtualMeasurement[] {
                    new VirtualMeasurement("Accuracy", linqExpr)
                }
            );

            var arranger = new VirtualMeasurementForward();
            arranger.Execute(statement);

            Assert.That(statement.Filters, Is.Not.Null);
            Assert.That(statement.Filters, Has.Count.EqualTo(1));
            Assert.That(((GathererSifter)statement.Filters[0]).Expression, Is.TypeOf<VirtualColumnExpression>());
            var expr = ((GathererSifter)statement.Filters[0]).Expression;
            Assert.That(expr, Is.TypeOf<VirtualColumnExpression>());
            Assert.That(((VirtualColumnExpression)expr).Expression, Is.EqualTo(linqExpr));
        }
    }
}
