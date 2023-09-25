using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tseesecake.QA.DuckDB
{
    [Category("DuckDB")]
    public class SelectCommandDuckDBTest : BaseSelectCommandTest
    {
        public override string ConnectionString => $"duckdb:///Energy.duckdb";
    }
}
