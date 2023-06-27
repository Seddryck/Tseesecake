using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using DubUrl.Registering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Engine;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Restrictions;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Testing.Engine.DuckDB
{
    public class ElementalQueryDubckDBTest : BaseElementalQueryTest
    {
        protected override string DialectName => "duckdb";

        protected override string ProjectionSingle 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionMultiple 
            => "SELECT\r\n\tInstant\r\n\t, Produced\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionExpression 
            => "SELECT\r\n\tLOWER(WindPark) AS LowerWindPark\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionAggregation
            => "SELECT\r\n\tMAX(Produced) AS Maximum\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionAggregationFilter
            => "SELECT\r\n\tAVG(Produced) FILTER (WHERE Producer = 'Future Energy') AS Average\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string FilterSingle 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tWindPark = 'Sea park'\r\n";
        protected override string FilterMultiple 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tWindPark IN ('Sea park', 'Children of tomorrow park')\r\n\tAND Producer = 'Future Energy'\r\n";
        protected override string FilterCuller 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tNOT(Produced < 5)\r\n";
        protected override string FilterTemporizer 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tage(Instant) < INTERVAL '4 HOURS 30 MINUTES 0 SECONDS'\r\n";
        protected override string SlicerSingle 
            => "SELECT\r\n\tMAX(Produced) AS maximum\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\n";
        protected override string SlicerMultiple 
            => "SELECT\r\n\tMAX(Produced) AS maximum\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\n\t, date_part('weekday', Instant)\r\n";
        protected override string SlicerAndGroupFilter 
            => "SELECT\r\n\tAVG(Produced) AS average\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\nHAVING\r\n\taverage >= 15\r\n";
        protected override string LimitOffset 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nORDER BY\r\n\tProduced DESC NULLS LAST\r\nLIMIT 20\r\nOFFSET 40\r\n";
    }
}
