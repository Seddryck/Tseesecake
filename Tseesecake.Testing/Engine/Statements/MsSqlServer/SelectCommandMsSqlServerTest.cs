using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Tseesecake.Engine.Statements;
using Tseesecake.Engine.Statements.MsSql;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Frames;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.WindowFunctions;
using Tseesecake.Modeling.Statements.Windows;

namespace Tseesecake.Testing.Engine.Statements.MsSqlServer
{
    public class SelectCommandMsSqlServerTest : BaseSelectCommandTest
    {
        protected override string DialectName => "mssql";

        protected override string ProjectionSingle
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionMultiple
            => "SELECT\r\n\t[Instant] AS [Instant]\r\n\t, [Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\n";

        protected override string ProjectionExpression
            => "SELECT\r\n\tLOWER([WindPark]) AS [LowerWindPark]\r\nFROM\r\n\t[WindEnergy]\r\n";
        [Test]
        public override void Read_ProjectionExpression_ValidStatement()
        {
            var statement = new SelectStatement(SelectStatementDefinition.ProjectionExpression.Timeseries
                , new[] {
                    new Projection(new LiteralExpression("LOWER([WindPark])"), "LowerWindPark")
                });

            Assert.That(new SelectCommand(statement).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionExpression));
        }


        protected override string ProjectionAggregation
            => "SELECT\r\n\tMAX([Produced]) AS [Maximum]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionAggregationFilter
            => "SELECT\r\n\tAVG(\r\n\t\tCASE \r\n\t\t\tWHEN [Producer] = 'Future Energy'\r\n\t\t\tTHEN [Produced]\r\n\t\t\tELSE NULL\r\n\t\tEND\r\n\t) AS [Average]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionWindow
            => "SELECT\r\n\tROW_NUMBER() OVER(\r\n\t\tORDER BY [Produced] DESC\r\n\t) AS [RowId]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionWindowOffset
            => "SELECT\r\n\tLAG([Produced], 4, 0) OVER(\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY CASE WHEN [Instant] IS NULL THEN 1 ELSE 0 END ASC, [Instant] ASC\r\n\t) AS [FourHoursBefore]\r\nFROM\r\n\t[WindEnergy]\r\n";
        
        [Test]
        public override void Read_ProjectionWindowOffsetExpression_ValidStatement()
        {
            var statement = SelectStatementDefinition.ProjectionWindowOffsetExpression;
            ((statement.Projections[0].Expression as WindowExpression)!.WindowFunction as LagWindowFunction)!.Expression = new LiteralExpression("ABS([Produced] - [Forecasted])");

            Assert.That(new SelectCommand(statement).Read(Dialect, Connectivity)
                , Is.EqualTo(ProjectionWindowOffsetExpression));
        }
        protected override string ProjectionWindowOffsetExpression
            => "SELECT\r\n\tLAG(ABS([Produced] - [Forecasted]), 4, 0) OVER(\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY CASE WHEN [Instant] IS NULL THEN 1 ELSE 0 END ASC, [Instant] ASC\r\n\t) AS [FourHoursBefore]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string ProjectionWindowFrame
            => "SELECT\r\n\tLAST_VALUE([Produced]) OVER(\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY [Instant] ASC\r\n\t\tRANGE BETWEEN UNBOUNDED PRECEDING\r\n\t\t\tAND CURRENT ROW\r\n\t) AS [Smooth]\r\nFROM\r\n\t[WindEnergy]\r\n";
        protected override string FilterSingle
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nWHERE\r\n\t[WindPark] = 'Sea park'\r\n";
        protected override string FilterMultiple
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nWHERE\r\n\t[WindPark] IN ('Sea park', 'Children of tomorrow park')\r\n\tAND [Producer] = 'Future Energy'\r\n";
        protected override string FilterCuller
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nWHERE\r\n\tNOT([Produced] < 5)\r\n";
        protected override string FilterTemporizer
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nWHERE\r\n\tDATEDIFF(second, [Instant], GETDATE()) < DATEDIFF(second, 0, CAST ('04:30:00' AS TIME))\r\n";
        protected override string SlicerSingle
            => "SELECT\r\n\tMAX([Produced]) AS [Maximum]\r\nFROM\r\n\t[WindEnergy]\r\nGROUP BY\r\n\t[WindPark]\r\n";
        protected override string SlicerMultiple
            => "SELECT\r\n\tMAX([Produced]) AS [Maximum]\r\nFROM\r\n\t[WindEnergy]\r\nGROUP BY\r\n\t[WindPark]\r\n\t, DATEPART(weekday, [Instant])\r\n";
        protected override string SlicerAndGroupFilter
            => "SELECT\r\n\tAVG([Produced]) AS [AvgProduced]\r\nFROM\r\n\t[WindEnergy]\r\nGROUP BY\r\n\t[WindPark]\r\nHAVING\r\n\tAVG([Produced]) >= 15\r\n";

        [Test]
        public override void Read_SlicerAndGroupFilter_ValidStatement()
        {
            var arrangers = new MsSqlServerArrangerCollectionFactory().Instantiate<IStatement>();
            var statement = SelectStatementDefinition.SlicerAndGroupFilter;
            foreach (var arranger in arrangers)
                arranger.Execute(statement);

            Assert.That(new SelectCommand(statement).Read(Dialect, Connectivity)
                , Is.EqualTo(SlicerAndGroupFilter));
        }

        protected override string NamedWindow
            => "SELECT\r\n\tMIN([Produced]) OVER(\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY CASE WHEN [Instant] IS NULL THEN 1 ELSE 0 END ASC, [Instant] ASC\r\n\t\tROWS BETWEEN 7 PRECEDING\r\n\t\t\tAND CURRENT ROW\r\n\t) AS [MinSevenDays]\r\n\t, MAX([Produced]) OVER(\r\n\t\tPARTITION BY [WindPark]\r\n\t\tORDER BY CASE WHEN [Instant] IS NULL THEN 1 ELSE 0 END ASC, [Instant] ASC\r\n\t\tROWS BETWEEN 7 PRECEDING\r\n\t\t\tAND CURRENT ROW\r\n\t) AS [MaxSevenDays]\r\nFROM\r\n\t[WindEnergy]\r\n";
        [Test]
        public override void Read_NamedWindow_ValidStatement()
        {
            var arrangers = new MsSqlServerArrangerCollectionFactory().Instantiate<IStatement>();
            var statement = SelectStatementDefinition.NamedWindow;
            foreach (var arranger in arrangers)
                arranger.Execute(statement);

            Assert.That(new SelectCommand(statement).Read(Dialect, Connectivity)
                , Is.EqualTo(NamedWindow));
        }

        protected override string Qualify
            => "SELECT * FROM (\r\n\tSELECT\r\n\t\tROW_NUMBER() OVER(\r\n\t\t\tPARTITION BY [Producer]\r\n\t\t\tORDER BY [Produced] DESC\r\n\t\t) AS [RowNb]\r\n\tFROM\r\n\t\t[WindEnergy]\r\n) AS T1\r\nWHERE\r\n\t[RowNb] <= 5\r\n";
        protected override string LimitOffset
            => "SELECT\r\n\t[Produced] AS [Produced]\r\nFROM\r\n\t[WindEnergy]\r\nORDER BY\r\n\tCASE WHEN [Produced] IS NULL THEN 1 ELSE 0 END DESC, [Produced] DESC\r\nOFFSET 40 ROWS\r\nFETCH NEXT 20 ROWS ONLY\r\n";
        protected override string VirtualMeasurementProjection
            => "SELECT\r\n\t([Forecasted] - [Produced]) AS [Accuracy]\r\nFROM\r\n\t[WindEnergy]\r\nORDER BY\r\n\t[Accuracy] DESC\r\n";
        protected override string VirtualMeasurementAggregation
            => "SELECT\r\n\tMIN(([Forecasted] - [Produced])) AS [MinAccuracy]\r\nFROM\r\n\t[WindEnergy]\r\n";
    }
}