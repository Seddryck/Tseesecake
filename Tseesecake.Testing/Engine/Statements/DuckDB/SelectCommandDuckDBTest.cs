﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Testing.Engine.Statements;

namespace Tseesecake.Testing.Engine.Statements.DuckDB
{
    public class SelectCommandDuckDBTest : BaseSelectCommandTest
    {
        protected override string DialectName => "duckdb";

        protected override string ProjectionSingle
            => "SELECT\r\n\tProduced AS Produced\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionMultiple
            => "SELECT\r\n\tInstant AS Instant\r\n\t, Produced AS Produced\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionExpression
            => "SELECT\r\n\tLOWER(\"WindPark\") AS LowerWindPark\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionAggregation
            => "SELECT\r\n\tMAX(Produced) AS Maximum\r\nFROM\r\n\tWindEnergy\r\n"; 
        protected override string ProjectionAggregationProduct
            => "SELECT\r\n\tPRODUCT(Produced) AS Value\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tInstant < TIMESTAMP '2022-12-28 03:00:00'\r\n\tAND WindPark = 'Wind parks in the Sky'\r\n";
        protected override string ProjectionAggregationFilter
            => "SELECT\r\n\tAVG(Produced) FILTER (WHERE Producer = 'Future Energy') AS Average\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionWindow
            => "SELECT\r\n\tROW_NUMBER() OVER(\r\n\t\tORDER BY Produced DESC NULLS LAST\r\n\t) AS RowId\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionWindowOffset
            => "SELECT\r\n\tLAG(Produced, 4, 0) OVER(\r\n\t\tPARTITION BY WindPark\r\n\t\tORDER BY Instant ASC NULLS LAST\r\n\t) AS FourHoursBefore\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionWindowOffsetExpression
            => "SELECT\r\n\tLAG(ABS(\"Produced\" - \"Forecasted\"), 4, 0) OVER(\r\n\t\tPARTITION BY WindPark\r\n\t\tORDER BY Instant ASC NULLS LAST\r\n\t) AS FourHoursBefore\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string ProjectionWindowFrame
            => "SELECT\r\n\tLAST(Produced) OVER(\r\n\t\tPARTITION BY WindPark\r\n\t\tORDER BY Instant ASC NULLS FIRST\r\n\t\tRANGE BETWEEN UNBOUNDED PRECEDING\r\n\t\t\tAND CURRENT ROW\r\n\t) AS Smooth\r\nFROM\r\n\tWindEnergy\r\n";
        protected override string FilterSingle
            => "SELECT\r\n\tProduced AS Produced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tWindPark = 'Sea park'\r\n";
        protected override string FilterMultiple
            => "SELECT\r\n\tProduced AS Produced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tWindPark IN ('Sea park', 'Children of tomorrow park')\r\n\tAND Producer = 'Future Energy'\r\n";
        protected override string FilterCuller
            => "SELECT\r\n\tProduced AS Produced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tNOT(Produced < 5)\r\n";
        protected override string FilterTemporizer
            => "SELECT\r\n\tProduced AS Produced\r\nFROM\r\n\tWindEnergy\r\nWHERE\r\n\tage(Instant) < INTERVAL '4 HOURS 30 MINUTES 0 SECONDS'\r\n";
        protected override string SlicerSingle
            => "SELECT\r\n\tMAX(Produced) AS Maximum\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\n";
        protected override string SlicerMultiple
            => "SELECT\r\n\tMAX(Produced) AS Maximum\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\n\t, date_part('dayofweek', Instant)\r\n";
        protected override string SlicerAndGroupFilter
            => "SELECT\r\n\tAVG(Produced) AS AvgProduced\r\nFROM\r\n\tWindEnergy\r\nGROUP BY\r\n\tWindPark\r\nHAVING\r\n\tAvgProduced >= 15\r\n";
        protected override string NamedWindow
            => "SELECT\r\n\tMIN(Produced) OVER seven AS MinSevenDays\r\n\t, MAX(Produced) OVER seven AS MaxSevenDays\r\nFROM\r\n\tWindEnergy\r\nWINDOW\r\n\tseven AS (\r\n\t\tPARTITION BY WindPark\r\n\t\tORDER BY Instant ASC NULLS LAST\r\n\t\tROWS BETWEEN 7 PRECEDING\r\n\t\t\tAND CURRENT ROW\r\n\t)\r\n";
        protected override string Qualify
            => "SELECT\r\n\tROW_NUMBER() OVER(\r\n\t\tPARTITION BY Producer\r\n\t\tORDER BY Produced DESC NULLS LAST\r\n\t) AS RowNb\r\nFROM\r\n\tWindEnergy\r\nQUALIFY\r\n\tRowNb <= 5\r\n";
        protected override string LimitOffset
            => "SELECT\r\n\tProduced AS Produced\r\nFROM\r\n\tWindEnergy\r\nORDER BY\r\n\tProduced DESC NULLS FIRST\r\nLIMIT 20\r\nOFFSET 40\r\n";
        protected override string VirtualMeasurementProjection
            => "SELECT\r\n\t(Forecasted - Produced) AS Accuracy\r\nFROM\r\n\tWindEnergy\r\nORDER BY\r\n\tAccuracy DESC NULLS LAST\r\n";
        protected override string VirtualMeasurementAggregation
            => "SELECT\r\n\tMIN((Forecasted - Produced)) AS MinAccuracy\r\nFROM\r\n\tWindEnergy\r\n";
    }
}
