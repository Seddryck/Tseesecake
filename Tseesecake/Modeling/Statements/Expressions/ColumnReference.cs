using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers.Action;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Expressions
{
    public class ColumnReference : IReference<Column>, IColumn, IExpression, IArrangeable
    {
        public string Name { get; }

        public string Template => "ColumnExpression";

        public ColumnReference(string name)
            => Name = name;

        public void Accept(IActionArranger arranger) { }
    }
}
