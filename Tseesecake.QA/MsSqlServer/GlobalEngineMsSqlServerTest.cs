using DubUrl.Extensions.DependencyInjection;
using DubUrl;
using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using DubUrl.Registering;
using DuckDB.NET.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tseesecake.Engine;
using Tseesecake.Testing.Engine;

namespace Tseesecake.QA.MsSqlServer
{
    [Category("MsSqlServer")]
    public class GlobalEngineMsSqlServerTest : BaseGlobalEngineTest
    {
        private const string FILENAME = "Instance.txt";
        public override string ConnectionString => $"mssql://sa:Password12!@{(File.Exists(FILENAME) ? File.ReadAllText(FILENAME) : "localhost/2019")}/Energy?TrustServerCertificate=True";

        [Test]
        public void Check()
        {
            Console.WriteLine(ConnectionString);

            var builder = new SchemeMapperBuilder();
            var factory = new ConnectionUrlFactory(builder);
            var conn = factory.Instantiate(ConnectionString).Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "select count(*) from WindEnergy";
            Console.WriteLine($"Rows count: {cmd.ExecuteScalar()}");
            conn.Close();
        }
    }
}