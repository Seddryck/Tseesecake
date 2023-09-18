using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Engine;
using Tseesecake.Engine.Postgresql;
using Tseesecake.Modeling;

namespace Tseesecake.Testing.Engine.MsSqlServer
{
    public class ElementalQueryMsSqlServerTest : BaseElementalQueryTest
    {
        protected override string DialectName => "mssql";

        protected override string ProjectionSingle 
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionMultiple 
            => "SELECT\r\n\t[Instant] AS [Instant]\r\n\t, [Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionExpression 
            => "SELECT\r\n\tLOWER([WindPark]) AS [LowerWindPark]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionAggregation
            => "SELECT\r\n\tMAX([Produced]) AS [Maximum]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionAggregationFilter
            => "SELECT\r\n\tAVG([Produced]) FILTER (WHERE [Producer] = 'Future Energy') AS [Average]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionWindow
            => "SELECT\r\n\tROW_NUMBER() OVER(\r\n\t\tORDER BY [Produced] DESC NULLS LAST\r\n\t) AS [RowId]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionWindowOffset
            => "SELECT\r\n\tLAG([Produced\", 4, 0) OVER(\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY [Instant] ASC NULLS LAST\r\n\t) AS [FourHoursBefore]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionWindowOffsetExpression
            => "SELECT\r\n\tLAG(ABS([Produced] - [Forecasted]), 4, 0) OVER(\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY [Instant] ASC NULLS LAST\r\n\t) AS [FourHoursBefore]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionWindowFrame
            => "SELECT\r\n\tLAST_VALUE([Produced]) OVER(\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY [Instant] ASC NULLS LAST\r\n\t\tRANGE BETWEEN UNBOUNDED PRECEDING\r\n\t\t\tAND INTERVAL '6 HOURS 0 MINUTES 0 SECONDS' FOLLOWING\r\n\t) AS [Smooth]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string FilterSingle 
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nWHERE\r\n\t[WindPark] = 'Sea park'\r\n";
        protected override string FilterMultiple 
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nWHERE\r\n\t[WindPark] IN ('Sea park', 'Children of tomorrow park')\r\n\tAND [Producer] = 'Future Energy'\r\n";
        protected override string FilterCuller 
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nWHERE\r\n\tNOT([Produced] < 5)\r\n";
        protected override string FilterTemporizer 
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nWHERE\r\n\tage([Instant]) < INTERVAL '4 HOURS 30 MINUTES 0 SECONDS'\r\n";
        protected override string SlicerSingle 
            => "SELECT\r\n\tMAX([Produced]) AS [Maximum]\r\nFROM\r\n\t[WindEnergy]\r\nGROUP BY\r\n\t[WindPark]\r\n";
        protected override string SlicerMultiple 
            => "SELECT\r\n\tMAX([Produced]) AS [Maximum]\r\nFROM\r\n\t[WindEnergy]\r\nGROUP BY\r\n\t[WindPark]\r\n\t, date_part('dow', [Instant])\r\n";
        protected override string SlicerAndGroupFilter 
            => "SELECT\r\n\tAVG([Produced]) AS [AvgProduced]\r\nFROM\r\n\t[WindEnergy]\r\nGROUP BY\r\n\t[WindPark]\r\nHAVING\r\n\tAVG([Produced]) >= 15\r\n";

        [Test]
        public override void Read_SlicerAndGroupFilter_ValidStatement()
        {
            var arrangers = new PostgresqlArrangerCollectionFactory().Instantiate<IStatement>();
            var statement = SelectStatementDefinition.SlicerAndGroupFilter;
            foreach (var arranger in arrangers)
                arranger.Execute(statement);

            Assert.That(new ElementalQuery(statement).Read(Dialect, Connectivity)
                , Is.EqualTo(SlicerAndGroupFilter));
        }

        protected override string NamedWindow
            => "SELECT\r\n\tMIN([Produced]) OVER seven AS [MinSevenDays]\r\n\t, MAX([Produced]) OVER seven AS [MaxSevenDays]\r\nFROM\r\n\t[WindEnergy]\r\nWINDOW\r\n\tseven AS (\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY [Instant] ASC NULLS LAST\r\n\t\tRANGE BETWEEN INTERVAL '3 DAYS 0 HOURS 0 MINUTES 0 SECONDS' PRECEDING\r\n\t\t\tAND INTERVAL '3 DAYS 0 HOURS 0 MINUTES 0 SECONDS' FOLLOWING\r\n\t)\r\n";
        protected override string Qualify
            => "SELECT * FROM (\r\n\tSELECT\r\n\t\tROW_NUMBER() OVER(\r\n\t\t\tPARTITION BY [Producer]\r\n\t\t\tORDER BY [Produced] DESC NULLS LAST\r\n\t\t) AS [RowNb]\r\n\tFROM\r\n\t\t[WindEnergy]\r\n) AS T1\r\nWHERE\r\n\t[RowNb] <= 5\r\n";
        protected override string LimitOffset 
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nORDER BY\r\n\t[Produced] DESC NULLS LAST\r\nLIMIT 20\r\nOFFSET 40\r\n";
        protected override string VirtualMeasurementProjection
            => "SELECT\r\n\t([Forecasted] - [Produced]) AS [Accuracy]\r\nFROM\r\n\t[WindEnergy]\r\nORDER BY\r\n\t[Accuracy] DESC NULLS LAST\r\n";
        protected override string VirtualMeasurementAggregation
            => "SELECT\r\n\tMIN(([Forecasted] - [Produced])) AS [MinAccuracy]\r\nFROM\r\n\t[WindEnergy]\r\n";
    }
}