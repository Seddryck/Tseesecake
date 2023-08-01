using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.Projections
{
    internal class ColumnReferenceProjection : ExpressionProjection
    {
        public override string Template { get => nameof(ColumnReferenceProjection); }

        public ColumnReferenceProjection(ColumnReference reference)
            : base(new ColumnExpression(reference), reference.Name) { }
    }
}
