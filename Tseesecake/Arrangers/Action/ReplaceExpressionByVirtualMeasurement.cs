using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Projections;

namespace Tseesecake.Arrangers.Action
{
    [Polyglot]
    internal class ReplaceExpressionByVirtualMeasurement : IActionArranger<IExpression>
    {
        protected VirtualMeasurement[] Virtuals { get; }

        public ReplaceExpressionByVirtualMeasurement(SelectStatement statement)
            => Virtuals = statement.VirtualMeasurements.ToArray();

        public IExpression Execute(IExpression obj)
        {
            if (obj is ColumnReference reference
                 && Virtuals.Any(x => x.Name == reference.Name))
                return new VirtualColumnExpression(Virtuals.Single(x => x.Name == reference.Name).Expression);
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
