using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Parameters = System.Collections.Generic.Dictionary<string, double>;

namespace Tseesecake.Parsing.Select.Calculator
{
    /// <summary>
    /// Extensible calculator.
    /// Supports named parameters.
    /// </summary>
    internal class ParameterCalculator : ScientificCalculator
    {
        protected internal virtual Parser<Expression> Parameter =>
            // identifier not followed by a '(' is a measurement
            from id in Grammar.Identifier
            from n in Parse.Not(Parse.Char('('))
            select Expression.Parameter(typeof(double), id);

        protected internal override Parser<Expression> Factor =>
            Parameter.Or(base.Factor);
    }
}
