using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Testing.Engine;

namespace Tseesecake.QA.MsSqlServer
{
    [Category("MsSqlServer")]
    public class DmlStatemenMsSqlServerTest : BaseDmlStatementTest
    {
        private const string FILENAME = "Instance.txt";
        public override string ConnectionString => $"mssql://sa:Password12!@{(File.Exists(FILENAME) ? File.ReadAllText(FILENAME) : "localhost/2019")}/Energy?TrustServerCertificate=True";

        [Test]
        [Ignore("File options not correctly handled")]
        public override void Mount_CopyFrom_ValidStatement()
        { }

        [Test]
        [Ignore("File options not correctly handled")]
        public override void Mount_CreateOrReplace_ValidStatement()
        { }
    }
}