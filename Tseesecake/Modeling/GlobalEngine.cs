using DubUrl;
using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Parsing;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Modeling
{
    public class GlobalEngine
    {
        private EngineDictionary<IDataReaderEngine> Engines { get; }
        public Timeseries[] Timeseries { get; }
        public GlobalParser Parser { get; }

        public GlobalEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries, IArrangerCollectionProvider provider)
        {
            var databaseUrl = factory.Instantiate(url);
            var arrangers = provider.Get(databaseUrl.Dialect.GetType()).Instantiate<IStatement>();
            Parser = new GlobalParser();
            Engines = new()
            {
                { typeof(ISelectStatement), new SelectEngine(databaseUrl, timeseries, arrangers, factory.QueryLogger) },
                { typeof(IShowStatement), new CatalogEngine(timeseries) }
            };
            Timeseries = timeseries;
        }

        public void Add<S>(IDataReaderEngine engine, Parser<IStatement> parser) where S : IStatement
        {
            Engines.Add<S>(engine);
            Parser.Add(parser);
        }

        public IDataReader ExecuteReader(string query)
        {
            var statement = Parser.Global.Parse(query);
            if (Engines.TryGetValue(statement.GetType(), out var engine))
                return engine.ExecuteReader(statement);
            else
                throw new InvalidOperationException();
        }
    }
}
