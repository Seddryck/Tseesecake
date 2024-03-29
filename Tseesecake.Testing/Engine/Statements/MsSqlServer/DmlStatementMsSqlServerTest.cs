﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tseesecake.Testing.Engine.Statements.MsSqlServer
{
    public class DmlStatementMsSqlServerTest : BaseDmlStatementTest
    {
        protected override string DialectName => "mssql";

        protected override string CreateOrReplace
            => "DROP TABLE IF EXISTS [WindEnergy];\r\nCREATE TABLE [WindEnergy](\r\n\t[Instant] DATETIME NOT NULL\r\n\t, [WindPark] VARCHAR(50) NOT NULL\r\n\t, [Producer] VARCHAR(50) NOT NULL\r\n\t, [Forecasted] DECIMAL(15, 6) NOT NULL\r\n\t, [Produced] DECIMAL(15, 6) NOT NULL\r\n);\r\n";
        [Test]
        [Ignore("Not implemented")]
        public override void Read_CreateOrReplace_ValidStatement()
        { }


        protected override string CopyFrom
            => $"{CreateOrReplace}\r\nSET DateStyle TO euro;\r\n\r\nCOPY\r\n\t\"WindEnergy\"\r\nFROM\r\n\t'.\\..\\..\\..\\WindEnergy.csv'\r\nWITH (\r\n\tFORMAT CSV\r\n\t, DELIMITER ','\r\n\t, HEADER 1\r\n\t, TIMESTAMPFORMAT '%d/%m/%Y %H:%M:%S'\r\n);\r\n";
        [Test]
        [Ignore("Not implemented")]
        public override void Read_CopyFrom_ValidStatement()
        { }
    }
}