using DubUrl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying;

namespace Tseesecake.Engine
{
    public class QueryEngine
    {
        private DatabaseUrl DatabaseUrl { get; }

        public QueryEngine(IDatabaseUrlFactory factory, string url)
            => DatabaseUrl = factory.Instantiate(url);

        public IDataReader ExecuteReader(SelectStatement query)
            => DatabaseUrl.ExecuteReader(new ElementalQuery(query));
    }
}
