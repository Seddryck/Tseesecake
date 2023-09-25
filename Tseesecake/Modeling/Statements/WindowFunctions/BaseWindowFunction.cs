using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Modeling.Statements.WindowFunctions
{
    internal abstract class BaseWindowFunction : IWindowFunction
    {
        public string Name => GetType().Name.Replace("WindowFunction", "").ToLowerInvariant();
    }
}
