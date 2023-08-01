using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.WindowFunctions
{
    public interface IExpressionWindowFunction : IWindowFunction
    {
        IExpression Expression { get; set; }
    }
}
