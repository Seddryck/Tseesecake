using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Statements.Expressions
{
    internal class BaseExpression : IExpression
    {
        public virtual string Template => GetType().Name;
    }
}
