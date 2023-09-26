using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tseesecake.QA.MsSqlServer
{
    [Category("MsSqlServer")]
    public class GlobalEngineMsSqlServerTest : BaseGlobalEngineTest
    {
        private const string FILENAME = "Instance.txt";
        public override string ConnectionString => $"mssql://sa:Password12!@{(File.Exists(FILENAME) ? File.ReadAllText(FILENAME) : "localhost/2019")}/Energy?TrustServerCertificate=True";
    }
}