using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.Projections
{
    internal class ExpressionProjection : IProjection
    {
        public IExpression Expression { get; }
        public string Alias { get; }

        public virtual string Template { get => nameof(ExpressionProjection); }

        public ExpressionProjection(IExpression expression, string alias)
            => (Expression, Alias) = (expression, alias);
    }
}
