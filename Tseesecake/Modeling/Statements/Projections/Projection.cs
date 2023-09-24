using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Projections
{
    internal class Projection : IProjection
    {
        public IExpression Expression { get; set; }
        public string Alias { get; }

        public Projection(Column column)
            : this(new ColumnExpression(column), column.Name) { }
        public Projection(ColumnReference reference)
            : this(reference, reference.Name) { }
        public Projection(IExpression expression, string alias)
            => (Expression, Alias) = (expression, alias);
    }
}
