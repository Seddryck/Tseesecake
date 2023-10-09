using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Filters
{
    public abstract class Sifter : IFilter, IArrangeable
    {
        public IExpression Expression { get; set; }

        public abstract string Template { get; }

        public Sifter(IExpression reference)
            => Expression = reference;

        public void Accept(IActionArranger arranger)
        {
            if (arranger is IActionArranger<IExpression> exp)
                Expression = exp.Execute(Expression);
            (Expression as IArrangeable)?.Accept(arranger);
        }
    }
}
