using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.WindowFunctions
{
    internal abstract class BaseColumnWindowFunction : BaseWindowFunction
    {
        protected BaseColumnWindowFunction(Column column)
            => Column = column;

        public Column Column { get; }
    }
}
