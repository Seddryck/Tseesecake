using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using DubUrl.Querying.Dialects;
using Tseesecake.Engine.Statements.DuckDB;

namespace Tseesecake.Testing.Arrangers
{
    public class ArrangerCollectionProviderTest
    {
        [Test]
        public void Get_Existing_Factory()
        {
            var provider = new ArrangerCollectionProvider();
            var factory = provider.Get<DuckdbDialect>();
            Assert.That(factory, Is.Not.Null);
            Assert.That(factory, Is.TypeOf<DuckdbArrangerCollectionFactory>());
        }
    }
}
