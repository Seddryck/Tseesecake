using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Engine.Statements.Common.Arrangers;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Engine.Statements.Postgresql.Arrangers
{
    internal class PostgresqlExpressionForward : ExpressionForward
    {
        protected override object[] ClausesSelect(SelectStatement statement)
        {
            return statement.GroupFilters.Cast<object>()
                .ToArray();
        }
    }
}
