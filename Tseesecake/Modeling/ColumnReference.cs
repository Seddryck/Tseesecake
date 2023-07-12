using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public record class ColumnReference : Column
    {
        public ColumnReference(string name)
            : base(name, DbType.Object) { }
    }
}
