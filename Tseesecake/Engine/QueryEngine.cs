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
        public Timeseries[] Timeseries { get; }

        protected internal QueryEngine(DatabaseUrl databaseUrl, Timeseries[] timeseries)
            => (DatabaseUrl, Timeseries) = (databaseUrl, timeseries);

        public QueryEngine(IDatabaseUrlFactory factory, string url, Timeseries[] timeseries)
            => (DatabaseUrl, Timeseries) = (factory.Instantiate(url), timeseries);

        public IDataReader ExecuteReader(SelectStatement statement)
        {
            statement.Timeseries = Timeseries.Single(x => statement.Timeseries.Name == x.Name);

            var arrangers = new ISelectArranger[] { new VirtualColumnAssignment(), new ColumnReferenceProjectionTyped(), new BucketAnonymousTimestamp(), new BucketAsProjection(), new FacetProjectionAsSlicer() };
            foreach (var arranger in arrangers)
                arranger.Execute(statement);

            return DatabaseUrl.ExecuteReader(new ElementalQuery(statement, DatabaseUrl.QueryLogger));
        }

        public IDataReader ExecuteReader(string query)
        {
            var parser = QueryParser.Query;
            var statement = parser.Parse(query);
            return ExecuteReader(statement);
        }
    }
}
