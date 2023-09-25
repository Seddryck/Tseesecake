using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Globalization;
using Tseesecake.Parsing.Select.Calculator;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Parsing.Select
{
    internal class VirtualMeasurementParser
    {
        public static Parser<VirtualMeasurement> VirtualMeasurement =
            from with in Keyword.With
            from measurement in Keyword.Measurement
            from name in Grammar.Identifier
            from @as in Keyword.As
            from lp in Parse.Char('(').Token()
            from expression in new ParameterCalculator().Expr
            from rp in Parse.Char(')').Token()
            select new VirtualMeasurement(name, expression);
    }
}
