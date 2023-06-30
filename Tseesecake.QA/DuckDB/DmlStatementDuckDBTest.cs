using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Testing.Engine.DuckDB;

namespace Tseesecake.QA.DuckDB
{
    public class DmlStatementDuckDBTest : BaseDmlStatementTest
    {
        public override string ConnectionString => $"duckdb:///WindEnergyMount.duckdb";
    }
}