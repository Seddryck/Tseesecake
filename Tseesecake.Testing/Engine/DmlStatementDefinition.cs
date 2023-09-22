using System;
using System.Collections.Generic;
using System.Linq;
using Tseesecake.Querying;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;
using Tseesecake.Mounting;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Testing.Engine
{
    public class DmlStatementDefinition
    {
        public static Timeseries WindEnergy
            => new(
                    "WindEnergy"
                    , new Timestamp("Instant")
                    , new[] { new Measurement("Forecasted"), new Measurement("Produced") }
                    , new[] { new Facet("WindPark"), new Facet("Producer") }
                );

        protected static FileTimeseries WindEnergyFile
            => new(
                    "WindEnergy"
                    , new Timestamp("Instant")
                    , new[] { new Measurement("Forecasted"), new Measurement("Produced") }
                    , new[] { new Facet("WindPark"), new Facet("Producer") }
                    , new FileSource(@".\..\..\..\WindEnergy.csv", new Dictionary<string, object>() { { "DELIMITER", "," }, { "HEADER", 1 }, { "TIMESTAMPFORMAT", "%d/%m/%Y %H:%M:%S" } })
                );

        public static Timeseries CreateOrReplace
            => WindEnergy;

        public static Timeseries CopyFrom
            => WindEnergyFile;

        public static SelectStatement ProjectionCount
            => new(WindEnergy
                , new[] {
                    new AggregationProjection(new CountAggregation(new LiteralExpression("*")), "countOfRows")
                });
    }
}