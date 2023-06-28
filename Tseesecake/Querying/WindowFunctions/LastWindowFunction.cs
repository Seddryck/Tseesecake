using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.WindowFunctions
{
    internal class LastWindowFunction : BaseColumnWindowFunction
    {
        public LastWindowFunction(Column column)
            : base(column) { }
    }
}
