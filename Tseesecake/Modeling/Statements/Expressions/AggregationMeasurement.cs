using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Aggregations;

namespace Tseesecake.Modeling.Statements.Expressions
{
    internal class AggregationMeasurement : Measurement, IExpression
    {
        public override string Template { get => "AggregationMeasurement"; }
        public IAggregation Aggregation { get; }
        public AggregationMeasurement(string name, IAggregation aggregation)
            : base(name) { Aggregation = aggregation; }
    }
}
