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
using Tseesecake.Engine;
using Tseesecake.Engine.Statements;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Modeling
{
    public class SelectEngine : IDataReaderEngine
    {
        private IDatabaseUrl DatabaseUrl { get; }
        public Timeseries[] Timeseries { get; }
        public IQueryLogger QueryLogger { get; } = NullQueryLogger.Instance;
        private ISelectArranger[] Arrangers { get; }

        protected internal SelectEngine(IDatabaseUrl databaseUrl, Timeseries[] timeseries, ISelectArranger[] arrangers, IQueryLogger logger)
            => (DatabaseUrl, Timeseries, Arrangers, QueryLogger) = (databaseUrl, timeseries, arrangers, logger);

        public SelectEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries, IArrangerCollectionProvider provider)
        {
            var databaseUrl = factory.Instantiate(url);
            var arrangers = provider.Get(databaseUrl.Dialect.GetType()).Instantiate<IStatement>();
            (DatabaseUrl, Timeseries, Arrangers, QueryLogger) = (databaseUrl, timeseries, arrangers, factory.QueryLogger);
        }

        public IDataReader ExecuteReader(SelectStatement statement)
        {
            statement.Timeseries = statement.Timeseries is IReference<Timeseries> reference ? Timeseries.Single(x => reference.Name == x.Name) : statement.Timeseries;
            var query = new SelectCommand(Arrange(statement), QueryLogger);
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
            var parser = SelectStatementParser.Query;
            var statement = parser.Parse(query);
            return ExecuteReader(statement);
        }

        public IDataReader ExecuteReader(IStatement statement)
            => statement is SelectStatement stmt ? ExecuteReader(stmt) : throw new ArgumentException();
    }
}