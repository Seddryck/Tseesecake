using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Projections;

namespace Tseesecake.Arrangers.Action
{
    internal class ReplaceExpressionByProjection : IActionArranger<IExpression>
    {
        protected Projection[] Projections { get; }

        public ReplaceExpressionByProjection(SelectStatement statement)
            => Projections = statement.Projections.Where(x => x is Projection).Cast<Projection>().ToArray();

        public IExpression Execute(IExpression obj)
        {
            if (obj is ColumnReference reference
                 && Projections.Any(x => x.Alias == reference.Name))
                return Projections.Single(x => x.Alias == reference.Name).Expression;
            return obj;
        }

        public object Execute(object obj)
        {
            if (obj is IExpression expression)
                Execute(expression);
            return obj;
        }
    }
}
