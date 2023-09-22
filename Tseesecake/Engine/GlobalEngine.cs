using DubUrl;
using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Parsing;
using Tseesecake.Parsing.Query;
using Tseesecake.Querying;

namespace Tseesecake.Engine
{
    public class GlobalEngine
    {
        private QueryEngine QueryEngine { get; }
        private CatalogEngine MetaEngine { get; }
        public Timeseries[] Timeseries { get; }

        public GlobalEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries, ArrangerCollectionProvider provider)
        {
            var databaseUrl = factory.Instantiate(url);
            var arrangers = provider.Get(databaseUrl.Dialect.GetType()).Instantiate<IStatement>();
            QueryEngine =  new QueryEngine(databaseUrl, timeseries, arrangers, factory.QueryLogger);
            MetaEngine = new CatalogEngine(timeseries);
            Timeseries = timeseries;
        }

        public IDataReader ExecuteReader(string query)
        {
            var parser = GlobalParser.Global;
            var statement = parser.Parse(query);
            return statement switch
            {
                SelectStatement select => QueryEngine.ExecuteReader(select),
                IShowStatement show => MetaEngine.ExecuteReader(show),
                _ => throw new NotImplementedException()
            };
        }
    }
}
