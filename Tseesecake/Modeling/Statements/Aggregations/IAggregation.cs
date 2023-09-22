using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.WindowFunctions;

namespace Tseesecake.Modeling.Statements.Aggregations
{
    public interface IAggregation : IExpressionWindowFunction
    { }
}
