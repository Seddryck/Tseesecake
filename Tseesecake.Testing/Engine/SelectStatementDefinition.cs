using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Restrictions;
using Tseesecake.Querying.Slicers;

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
                    new ExpressionProjection("MAX(Produced)", "maximum")
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
                    new Culler(new Measurement("Produced"), Expression.LessThan,  100)
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
                    new ExpressionProjection("MAX(Produced)", "maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                });

        public static SelectStatement SlicerMultiple
            => new (WindEnergy
                , new[] {
                    new ExpressionProjection("MAX(Produced)", "maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                    , new PartTemporalSlicer(new Timestamp("Instant"), "weekday")
                });

        public static SelectStatement SlicerAndGroupFilter
            => new (WindEnergy
                , new[] {
                    new ExpressionProjection("MAX(Produced)", "maximum")
                }
                , null
                , new ISlicer[] {
                    new FacetSlicer(new Facet("WindPark"))
                }
                , new IFilter[] {
                    new Gatherer(new Measurement("maximum"), Expression.GreaterThanOrEqual, 120)
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
                    new ColumnOrder(new Timestamp("Instant"), Sorting.Descending, NullSorting.Last) }
                , new LimitOffsetRestriction(20,40));
    }
}
