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
using Tseesecake.Mounting.Engine;
using Tseesecake.Parsing.Query;
using Tseesecake.Querying;

namespace Tseesecake.Engine
{
    public class QueryEngine
    {
        
        private DatabaseUrl DatabaseUrl { get; }
        public Timeseries[] Timeseries { get; set; }

        public QueryEngine(IDatabaseUrlFactory factory, string url)
            : this(factory, url, Array.Empty<Timeseries>()) { }

        public QueryEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries)
            => (DatabaseUrl, Timeseries) = (factory.Instantiate(url), timeseries);

        public IDataReader ExecuteReader(SelectStatement query)
            => DatabaseUrl.ExecuteReader(new ElementalQuery(query));

        public IDataReader ExecuteReader(string query)
        {
            var parser = QueryParser.Query;
            var statement = parser.Parse(query);

            statement.Timeseries = Timeseries.Single(x => statement.Timeseries.Name == x.Name);

            var arrangers = new ISelectArranger[] { new ColumnProjectionTyped(), new BucketAnonymousTimestamp(), new BucketAsProjection(), new FacetProjectionAsSlicer() };
            foreach (var arranger in arrangers)
                arranger.Execute(statement);

            return ExecuteReader(statement);
        }
    }
}
