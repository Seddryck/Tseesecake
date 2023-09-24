using System;
using System.Collections.Generic;
using System.Linq;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Arguments;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Frames;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Restrictions;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Modeling.Statements.WindowFunctions;
using Tseesecake.Modeling.Statements.Windows;
using LinqExpr = System.Linq.Expressions;

namespace Tseesecake.Testing.Engine
{
    public class SelectStatementDefinition
    {
        protected static Timeseries WindEnergy
            => new(
                    "WindEnergy"
                    , new Timestamp("Instant")
                    , new Measurement("Produced")
                    , new[] { new Facet("WindPark"), new Facet("Producer") }
                );

        public static SelectStatement ProjectionSingle
            => new (WindEnergy
                , new[] { 
                    new Projection(new ColumnReference("Produced"))
                });

        public static SelectStatement ProjectionMultiple
            => new (WindEnergy
                , new[] {
                    new Projection(new ColumnReference("Instant"))
                    , new Projection(new ColumnReference("Produced"))
                });

        public static SelectStatement ProjectionExpression
            => new (WindEnergy
                , new[] {
                    new Projection(new LiteralExpression("LOWER(\"WindPark\")"), "LowerWindPark")
                });

        public static SelectStatement ProjectionAggregation
            => new(WindEnergy
                , new[] {
                    new Projection(new AggregationExpression(new MaxAggregation(new ColumnReference("Produced"))), "Maximum")
                });

        public static SelectStatement ProjectionAggregationFilter
            => new(WindEnergy
                , new[] {
                    new Projection(
                        new FilteredAggregationExpression(
                            new AverageAggregation(new ColumnReference("Produced"))
                            , new[] { new EqualDicer(new Facet("Producer"), "Future Energy") }
                        )
                    , "Average")
                });

        public static SelectStatement ProjectionWindow
            => new(WindEnergy
                , new[] {
                    new Projection(
                        new WindowExpression(
                            new RowNumberWindowFunction(), 
                            new Window(
                                null
                                , new[] { new ColumnOrder(new ColumnReference("Produced"), Sorting.Descending, NullSorting.Last) }
                            )
                        )
                        , "RowId")
                });

        public static SelectStatement ProjectionWindowOffset
            => new(WindEnergy
                , new[] {
                    new Projection(
                        new WindowExpression(
                            new LagWindowFunction(new ColumnReference("Produced"), new ConstantExpression(4), new ConstantExpression(0)),
                            new Window(
                                new[] { new FacetSlicer(new Facet("WindPark")) }
                                , new[] { new ColumnOrder(new ColumnReference("Instant"), Sorting.Ascending, NullSorting.Last) }
                            )
                        )
                        , "FourHoursBefore")
                });

        public static SelectStatement ProjectionWindowOffsetExpression
            => new(WindEnergy
                , new[] {
                    new Projection(
                        new WindowExpression(
                            new LagWindowFunction(
                                new LiteralExpression("ABS(\"Produced\" - \"Forecasted\")")
                                , new ConstantExpression(4)
                                , new ConstantExpression(0)
                            ),
                            new Window(
                                new[] { new FacetSlicer(new Facet("WindPark")) }
                                , new[] { new ColumnOrder(new ColumnReference("Instant"), Sorting.Ascending, NullSorting.Last) }
                            )
                        )
                    , "FourHoursBefore")
                });

        public static SelectStatement ProjectionWindowFrame
            => new(WindEnergy
                , new[] {
                    new Projection(
                        new WindowExpression(
                            new LastWindowFunction(new ColumnReference("Produced")),
                            new Window(
                                new[] { new FacetSlicer(new Facet("WindPark")) }
                                , new[] { new ColumnOrder(new ColumnReference("Instant"), Sorting.Ascending, NullSorting.Last) }
                                , new RangeBetween(new UnboundedPreceding(), new Following(new ConstantExpression(new TimeSpan(6,0,0))))
                            )
                        )
                        , "Smooth"
                    )
                });

        public static SelectStatement FilterSingle
            =>  new (WindEnergy
                , new[] {
                    new Projection(new ColumnReference("Produced"))
                }
                , new IFilter[] {
                    new EqualDicer(new Facet("WindPark"), "Sea park")
                });

        public static SelectStatement FilterMultiple
            => new (WindEnergy
                , new[] {
                    new Projection(new ColumnReference("Produced"))
                }
                , new IFilter[] {
                    new InDicer(new Facet("WindPark"), new[] {"Sea park", "Children of tomorrow park" })
                    , new EqualDicer(new Facet("Producer"), "Future Energy")
                });

        public static SelectStatement FilterCuller
            => new (WindEnergy
                , new[] {
                    new Projection(new ColumnReference("Produced"), "Produced")
                }
                , new IFilter[] {
                    new CullerSifter(new Measurement("Produced"), LinqExpr.Expression.LessThan,  5)
                });

        public static SelectStatement FilterTemporizer
            => new (WindEnergy
                , new[] {
                    new Projection(new ColumnReference("Produced"))
                }
                , new IFilter[] {
                    new SinceTemporizer(new Timestamp("Instant"), new TimeSpan(4, 30, 0))
                });

        public static SelectStatement SlicerSingle
            => new (WindEnergy
                , new[] {
                    new Projection(new AggregationExpression(new MaxAggregation(new ColumnReference("Produced"))), "Maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                });

        public static SelectStatement SlicerMultiple
            => new (WindEnergy
                , new[] {
                    new Projection(new AggregationExpression(new MaxAggregation(new ColumnReference("Produced"))), "Maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                    , new CyclicTemporalSlicer(new Timestamp("Instant"), CyclicTemporal.DayOfWeek)
                });

        public static SelectStatement SlicerAndGroupFilter
            => new (WindEnergy
                , new[] {
                    new Projection(new AggregationExpression(new AverageAggregation(new ColumnReference("Produced"))), "AvgProduced")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                }
                , new IFilter[] {
                    new GathererSifter(new Measurement("AvgProduced"), LinqExpr.Expression.GreaterThanOrEqual, 15)
                });

        public static SelectStatement NamedWindow
            => new(WindEnergy
                , new[] {
                    new Projection(
                        new WindowExpression(
                            new MinAggregation(new ColumnReference("Produced"))
                            , new ReferenceWindow("seven")
                        )
                        , "MinSevenDays"
                    )
                    , new Projection(
                        new WindowExpression(
                            new MaxAggregation(new ColumnReference("Produced"))
                            , new ReferenceWindow("seven")
                        )
                        , "MaxSevenDays"
                    )}
                , null
                , null
                , new[] {
                    new NamedWindow("seven"
                        , new[] { new FacetSlicer(new Facet("WindPark")) }
                        , new[] { new ColumnOrder(new ColumnReference("Instant"), Sorting.Ascending, NullSorting.Last) }
                        , new RangeBetween(new Preceding(new ConstantExpression(new TimeSpan(3,0,0,0))), new Following(new ConstantExpression(new TimeSpan(3,0,0,0))))
                    )} 
                );
        public static SelectStatement Qualify
            => new(WindEnergy
                , new[] {
                    new Projection(
                        new WindowExpression(
                            new RowNumberWindowFunction(),
                            new Window(
                                new[] {new FacetSlicer(new Facet("Producer")) }
                                , new[] { new ColumnOrder(new ColumnReference("Produced"), Sorting.Descending, NullSorting.Last) }
                            )
                        )
                        , "RowNb"
                    )
                }
                , null
                , null
                , null
                , null
                , new[] {
                    new GathererSifter(new Measurement("RowNb"), LinqExpr.Expression.LessThanOrEqual, 5)
                }
                , null
                , null);

        public static SelectStatement LimitOffset
            => new (WindEnergy
                , new[] {
                    new Projection(new ColumnReference("Produced"))
                }
                , null
                , null
                , null
                , null
                , null
                , new IOrderBy[] {
                    new ColumnOrder(new ColumnReference("Produced"), Sorting.Descending, NullSorting.Last) }
                , new LimitOffsetRestriction(20,40));

        public static SelectStatement VirtualMeasurementProjection
            => new(WindEnergy
                , new[] {
                    new Projection(new ColumnReference("Accuracy"), "Accuracy")
                    { 
                        Expression = new VirtualColumnExpression(
                            LinqExpr.Expression.Subtract(
                                LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                                LinqExpr.Expression.Parameter(typeof(double), "Produced")
                             )
                        ) 
                    }
                }
                , null
                , null
                , null
                , null
                , null
                , new IOrderBy[] {
                    new ColumnOrder(new ColumnReference("Accuracy"), Sorting.Descending, NullSorting.Last) }
                , null
                );

        public static SelectStatement VirtualMeasurementAggregation
            => new(WindEnergy
                , new[] {
                    new Projection(new AggregationExpression(
                        new MinAggregation(
                            new VirtualColumnExpression(
                                LinqExpr.Expression.Subtract(
                                    LinqExpr.Expression.Parameter(typeof(double), "Forecasted"),
                                    LinqExpr.Expression.Parameter(typeof(double), "Produced")
                                 )
                            )
                        )
                    ), "MinAccuracy")
                });
    }
}
