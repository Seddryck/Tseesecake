using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.WindowFunctions
{
    internal abstract class BaseOffsetWindowFunction : BaseColumnWindowFunction
    {
        protected BaseOffsetWindowFunction(Column column, int offset, object @default)
            : base(column) { (Offset, Default) = (offset, @default); }

        public int Offset { get; }
        public object Default { get; }

    }
}
