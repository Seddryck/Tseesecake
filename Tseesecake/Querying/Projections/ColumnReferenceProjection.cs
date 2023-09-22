using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.Projections
{
    internal class ColumnReferenceProjection : ExpressionProjection
    {
        public override string Template { get => nameof(ColumnReferenceProjection); }

        public ColumnReferenceProjection(IColumn reference)
            : base(new ColumnExpression(reference), reference.Name) { }
    }
}
