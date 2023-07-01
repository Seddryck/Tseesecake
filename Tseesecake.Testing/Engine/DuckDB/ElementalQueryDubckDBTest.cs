using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
        protected override string ProjectionWindow
            => "SELECT\r\n\tROW_NUMBER() OVER(\r\n\t\tORDER BY Produced DESC NULLS LAST\r\n\t) AS RowId\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionWindowOffset
            => "SELECT\r\n\tLAG(Produced, 4, 0) OVER(\r\n\t\tPARTITION BY WindPark\r\n\t\tORDER BY Instant ASC NULLS LAST\r\n\t) AS FourHoursBefore\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionWindowOffsetExpression
            => "SELECT\r\n\tLAG(ABS(Produced - Forecasted), 4, 0) OVER(\r\n\t\tPARTITION BY WindPark\r\n\t\tORDER BY Instant ASC NULLS LAST\r\n\t) AS FourHoursBefore\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionWindowFrame
            => "SELECT\r\n\tLAST(Produced) OVER(\r\n\t\tPARTITION BY WindPark\r\n\t\tORDER BY Instant ASC NULLS LAST\r\n\t\tRANGE BETWEEN UNBOUNDED PRECEDING\r\n\t\t\tAND INTERVAL '6 HOURS 0 MINUTES 0 SECONDS' FOLLOWING\r\n\t) AS Smooth\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string FilterSingle 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tWindPark = 'Sea park'\r\n";
        protected override string FilterMultiple 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tWindPark IN ('Sea park', 'Children of tomorrow park')\r\n\tAND Producer = 'Future Energy'\r\n";
        protected override string FilterCuller 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tNOT(Produced < 5)\r\n";
        protected override string FilterTemporizer 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tage(Instant) < INTERVAL '4 HOURS 30 MINUTES 0 SECONDS'\r\n";
        protected override string SlicerSingle 
            => "SELECT\r\n\tMAX(Produced) AS Maximum\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\n";
        protected override string SlicerMultiple 
            => "SELECT\r\n\tMAX(Produced) AS Maximum\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\n\t, date_part('weekday', Instant)\r\n";
        protected override string SlicerAndGroupFilter 
            => "SELECT\r\n\tAVG(Produced) AS average\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\nHAVING\r\n\taverage >= 15\r\n";
        protected override string LimitOffset 
            => "SELECT\r\n\tProduced\r\nFROM\r\n\tWindEnergy\r\nORDER BY\r\n\tProduced DESC NULLS LAST\r\nLIMIT 20\r\nOFFSET 40\r\n";
    }
}
