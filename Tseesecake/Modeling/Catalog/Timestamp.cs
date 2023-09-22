using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Catalog
{
    public class Timestamp : Column
    {
        public Timestamp(string name)
            : this(name, DbType.DateTime) { }

        protected Timestamp(string name, DbType dbType)
            : base(name, dbType) { }
    }
}
