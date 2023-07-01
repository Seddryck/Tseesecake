using System;
using System.Collections.Generic;
using System.Linq;
using LinqExp = System.Linq.Expressions;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Aggregations;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Restrictions;
using Tseesecake.Querying.Slicers;
using Tseesecake.Querying.WindowFunctions;
using Tseesecake.Querying.Frames;

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
                    new ColumnProjection(new Measurement("Produced"))
                });

        public static SelectStatement ProjectionMultiple
            => new (WindEnergy
                , new[] {
                    new ColumnProjection(new Timestamp("Instant"))
                    , new ColumnProjection(new Measurement("Produced"))
                });

        public static SelectStatement ProjectionExpression
            => new (WindEnergy
                , new[] {
                    new ExpressionProjection(new LiteralExpression("LOWER(WindPark)"), "LowerWindPark")
                });

        public static SelectStatement ProjectionAggregation
            => new(WindEnergy
                , new[] {
                    new AggregationProjection(new MaxAggregation(new ColumnExpression(new Measurement("Produced"))), "Maximum")
                });

        public static SelectStatement ProjectionAggregationFilter
            => new(WindEnergy
                , new[] {
                    new AggregationProjection(new AverageAggregation(new ColumnExpression(new Measurement("Produced"))), new[] { new EqualDicer(new Facet("Producer"), "Future Energy") } , "Average")
                });


        public static SelectStatement ProjectionWindow
            => new(WindEnergy
                , new[] {
                    new WindowProjection(new RowNumberWindowFunction(), new[] { new ColumnOrder(new Measurement("Produced"), Sorting.Descending, NullSorting.Last) }, null , "RowId")
                });

        public static SelectStatement ProjectionWindowOffset
            => new(WindEnergy
                , new[] {
                    new WindowProjection(new LagWindowFunction(new ColumnExpression(new Measurement("Produced")), new ConstantExpression(4), new ConstantExpression(0)), new[] { new ColumnOrder(new Timestamp("Instant"), Sorting.Ascending, NullSorting.Last) }, new[] { new FacetSlicer(new Facet("WindPark")) } , "FourHoursBefore")
                });

        public static SelectStatement ProjectionWindowOffsetExpression
            => new(WindEnergy
                , new[] {
                    new WindowProjection(
                        new LagWindowFunction(
                            new LiteralExpression("ABS(Produced - Forecasted)")
                            , new ConstantExpression(4)
                            , new ConstantExpression(0)
                        )
                        , new[] { new ColumnOrder(new Timestamp("Instant"), Sorting.Ascending, NullSorting.Last) }
                        , new[] { new FacetSlicer(new Facet("WindPark")) } 
                        , "FourHoursBefore")
                });

        public static SelectStatement ProjectionWindowFrame
            => new(WindEnergy
                , new[] {
                    new WindowProjection(
                        new LastWindowFunction(new ColumnExpression(new Measurement("Produced")))
                        , new[] { new ColumnOrder(new Timestamp("Instant"), Sorting.Ascending, NullSorting.Last) }
                        , new[] { new FacetSlicer(new Facet("WindPark")) } 
                        , new RangeBetween(new UnboundedPreceding(), new Following(new ConstantExpression(new TimeSpan(6,0,0))))
                        , "Smooth"
                    )
                });

        public static SelectStatement FilterSingle
            =>  new (WindEnergy
                , new[] {
                    new ColumnProjection(new Timestamp("Produced"))
                }
                , new[] {
                    new EqualDicer(new Facet("WindPark"), "Sea park")
                });

        public static SelectStatement FilterMultiple
            => new (WindEnergy
                , new[] {
                    new ColumnProjection(new Timestamp("Produced"))
                }
                , new IFilter[] {
                    new InDicer(new Facet("WindPark"), new[] {"Sea park", "Children of tomorrow park" })
                    , new EqualDicer(new Facet("Producer"), "Future Energy")
                });

        public static SelectStatement FilterCuller
            => new (WindEnergy
                , new[] {
                    new ColumnProjection(new Timestamp("Produced"))
                }
                , new IFilter[] {
                    new Culler(new Measurement("Produced"), LinqExp.Expression.LessThan,  5)
                });

        public static SelectStatement FilterTemporizer
            => new (WindEnergy
                , new[] {
                    new ColumnProjection(new Timestamp("Produced"))
                }
                , new IFilter[] {
                    new SinceTemporizer(new Timestamp("Instant"), new TimeSpan(4, 30, 0))
                });

        public static SelectStatement SlicerSingle
            => new (WindEnergy
                , new[] {
                    new AggregationProjection(new MaxAggregation(new ColumnExpression(new Measurement("Produced"))), "Maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                });

        public static SelectStatement SlicerMultiple
            => new (WindEnergy
                , new[] {
                    new AggregationProjection(new MaxAggregation(new ColumnExpression(new Measurement("Produced"))), "Maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                    , new PartTemporalSlicer(new Timestamp("Instant"), "weekday")
                });

        public static SelectStatement SlicerAndGroupFilter
            => new (WindEnergy
                , new[] {
                    new AggregationProjection(new AverageAggregation(new ColumnExpression(new Measurement("Produced"))), "average")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                }
                , new IFilter[] {
                    new Gatherer(new Measurement("average"), LinqExp.Expression.GreaterThanOrEqual, 15)
                });

        public static SelectStatement LimitOffset
            => new (WindEnergy
                , new[] {
                    new ColumnProjection(new Measurement("Produced"))
                }
                , null
                , null
                , null
                , new IOrderBy[] {
                    new ColumnOrder(new Measurement("Produced"), Sorting.Descending, NullSorting.Last) }
                , new LimitOffsetRestriction(20,40));
    }
}
