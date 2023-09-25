using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Tseesecake.Modeling.Catalog
{
    public class VirtualMeasurement : Measurement
    {
        public Expression Expression { get; }
        public VirtualMeasurement(string name, Expression expression)
            : base(name) { Expression = expression; }
    }
}
