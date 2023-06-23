using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying
{
    internal class ExpressionProjection : IProjection
    {
        public string Expression { get; }
        public string Alias { get; }

        public string Template { get => nameof(ExpressionProjection); }

        public ExpressionProjection(string expression, string alias)
            => (Expression, Alias) = (expression, alias);
    }
}
