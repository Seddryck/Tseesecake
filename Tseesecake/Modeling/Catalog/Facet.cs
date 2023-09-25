using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Catalog
{
    public class Facet : Column
    {
        public Facet(string name)
            : base(name, DbType.String) { }
    }
}
