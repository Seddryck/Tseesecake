using DubUrl;
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

        protected internal QueryEngine(DatabaseUrl databaseUrl, Timeseries[] timeseries, ISelectArranger[] arrangers, IQueryLogger logger)
            => (DatabaseUrl, Timeseries, Arrangers, QueryLogger) = (databaseUrl, timeseries, arrangers, logger);

        public QueryEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries, ArrangerCollectionProvider provider)
        {
            var databaseUrl = factory.Instantiate(url);
            var arrangers = provider.Get(databaseUrl.Dialect.GetType()).Instantiate<IStatement>();
            (DatabaseUrl, Timeseries, Arrangers, QueryLogger) = (databaseUrl, timeseries, arrangers, factory.QueryLogger);
        }

        public IDataReader ExecuteReader(SelectStatement statement)
        {
            statement.Timeseries = Timeseries.Single(x => statement.Timeseries.Name == x.Name);
            var query = new ElementalQuery(Arrange(statement), QueryLogger);
            return DatabaseUrl.ExecuteReader(query);
        }

        protected internal SelectStatement Arrange(SelectStatement statement)
        {
            foreach (var arranger in Arrangers)
                arranger.Execute(statement);

            return statement;
        }


        public IDataReader ExecuteReader(string query)
        {
            var parser = QueryParser.Query;
            var statement = parser.Parse(query);
            return ExecuteReader(statement);
        }
    }
}