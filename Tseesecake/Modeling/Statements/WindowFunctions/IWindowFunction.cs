using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.ColumnExpressions;

namespace Tseesecake.Modeling.Statements.WindowFunctions
{
    public interface IWindowFunction
    { }

    public interface IExpressionWindowFunction : IWindowFunction
    { 
        IExpression Expression { get; set; }
    }
}
