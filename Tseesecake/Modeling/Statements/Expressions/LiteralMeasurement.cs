using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Expressions
{
    internal class LiteralMeasurement : Measurement
    {
        public override string Template { get => "LiteralExpression"; }
        public string Literal { get; }
        public LiteralMeasurement(string name, string literal)
            : base(name) { Literal = literal; }
    }
}
