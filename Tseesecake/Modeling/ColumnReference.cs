using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public class ColumnReference
    {
        public string Name { get; }

        public ColumnReference(string name)
            => Name = name;
    }
}
