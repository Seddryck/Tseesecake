using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.WindowFunctions
{
    internal class LagWindowFunction : BaseOffsetWindowFunction
    {
        public LagWindowFunction(Column column, int offset, object @default)
            : base(column, offset, @default) { }
    }
}
