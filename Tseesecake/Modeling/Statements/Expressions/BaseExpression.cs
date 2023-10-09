using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;

namespace Tseesecake.Modeling.Statements.Expressions
{
    internal abstract class BaseExpression : IExpression
    {
        public virtual string Template => GetType().Name;
    }
}
