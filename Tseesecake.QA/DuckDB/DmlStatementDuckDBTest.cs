using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tseesecake.QA.DuckDB
{
    [Category("DuckDB")]
    public class DmlStatementDuckDBTest : BaseDmlStatementTest
    {
        public override string ConnectionString => $"duckdb:///EnergyMount.duckdb";
    }
}