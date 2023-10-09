using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Windows;

namespace Tseesecake.Modeling.Statements.Projections
{
    internal class Projection : IProjection, IArrangeable
    {
        public IExpression Expression { get; set; }
        public string Alias { get; }

        public Projection(Column column)
            : this(new ColumnExpression(column), column.Name) { }
        public Projection(ColumnReference reference)
            : this(reference, reference.Name) { }
        public Projection(IExpression expression, string alias)
            => (Expression, Alias) = (expression, alias);

        public void Accept(IActionArranger arranger)
        {
            if (arranger is IActionArranger<IExpression> exprArranger)
                Expression = exprArranger.Execute(Expression);
            (Expression as IArrangeable)?.Accept(arranger);
        }
    }
}
