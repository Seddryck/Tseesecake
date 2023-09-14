﻿using DubUrl;
using DubUrl.Querying;
using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;
using Tseesecake.Mounting.Engine;
using Tseesecake.Parsing.Query;
using Tseesecake.Querying;

namespace Tseesecake.Engine
{
    public class QueryEngine
    {
        private DatabaseUrl DatabaseUrl { get; }
        public Timeseries[] Timeseries { get; }
        public IQueryLogger QueryLogger { get; } = NullQueryLogger.Instance;
        private ISelectArranger[] Arrangers { get; }

        protected internal QueryEngine(DatabaseUrl databaseUrl, Timeseries[] timeseries, IQueryLogger logger)
            => (DatabaseUrl, Timeseries, QueryLogger) = (databaseUrl, timeseries, logger);
        protected internal QueryEngine(DatabaseUrl databaseUrl, Timeseries[] timeseries, ISelectArranger[] arrangers)
            => (DatabaseUrl, Timeseries, Arrangers) = (databaseUrl, timeseries, arrangers);

        public QueryEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries)
            => (DatabaseUrl, Timeseries, QueryLogger) = (factory.Instantiate(url), timeseries, factory.QueryLogger);
        public QueryEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries, ISelectArranger[] arrangers)
            => (DatabaseUrl, Timeseries, Arrangers) = (factory.Instantiate(url), timeseries, arrangers);

        public IDataReader ExecuteReader(SelectStatement statement)
        {
            statement.Timeseries = Timeseries.Single(x => statement.Timeseries.Name == x.Name);

            foreach (var arranger in Arrangers)
                arranger.Execute(statement);

            var query = new ElementalQuery(statement, QueryLogger);

            return DatabaseUrl.ExecuteReader(query);
        }

        public IDataReader ExecuteReader(string query)
        {
            var parser = QueryParser.Query;
            var statement = parser.Parse(query);
            return ExecuteReader(statement);
        }
    }
}