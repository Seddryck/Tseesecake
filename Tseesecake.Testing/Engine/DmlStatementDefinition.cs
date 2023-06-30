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
using Tseesecake.Mounting;

namespace Tseesecake.Testing.Engine
{
    public class DmlStatementDefinition
    {
        protected static Timeseries WindEnergy
            => new(
                    "WindEnergy"
                    , new Timestamp("Instant")
                    , new Measurement("Produced")
                    , new[] { new Facet("WindPark"), new Facet("Producer") }
                );

        protected static FileTimeseries WindEnergyFile
            => new(
                    "WindEnergy"
                    , new Timestamp("Instant")
                    , new Measurement("Produced")
                    , new[] { new Facet("WindPark"), new Facet("Producer") }
                    , new FileSource(@".\data\WindEnergy.csv", new Dictionary<string, object>() { { "DELIMITER", "," }, { "HEADER", 1 } })
                );

        public static Timeseries CreateOrReplace
            => WindEnergy;

        public static Timeseries CopyFrom
            => WindEnergyFile;
    }
}
