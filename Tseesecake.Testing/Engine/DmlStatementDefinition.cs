using System;
using System.Collections.Generic;
using System.Linq;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Mounting;

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
                    new Projection(new AggregationExpression(new CountAggregation(new LiteralExpression("*"))), "countOfRows")
                });
    }
}