using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.WindowFunctions
{
    internal class FirstWindowFunction : BaseColumnWindowFunction
    {
        public FirstWindowFunction(Column column)
            : base(column) { }
    }
}
