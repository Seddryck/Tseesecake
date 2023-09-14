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
using Tseesecake.Parsing;
using Tseesecake.Parsing.Query;
using Tseesecake.Querying;

namespace Tseesecake.Engine
{
    public class GlobalEngine
    {
        private QueryEngine QueryEngine { get; }
        private MetaEngine MetaEngine { get; }
        public Timeseries[] Timeseries { get; }
        private ISelectArranger[] Arrangers { get; }

        public GlobalEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries, ArrangerCollectionProvider provider)
        {
            (DatabaseUrl, Timeseries) = (factory.Instantiate(url), timeseries);
            var dialect = DatabaseUrl.Dialect.GetType();
            Arrangers = provider.Get(dialect).Instantiate<IStatement>();
            QueryEngine =  new QueryEngine(DatabaseUrl, timeseries, Arrangers);
            MetaEngine = new MetaEngine(timeseries);
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
