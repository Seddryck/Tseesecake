using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Querying.Expressions
{
    internal class BaseExpression : IExpression
    {
        public string Template => GetType().Name;
    }
}
