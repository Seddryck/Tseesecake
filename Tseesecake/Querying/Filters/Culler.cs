using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class Culler : IFilter
    {
        protected Measurement Measurement { get; }

        protected virtual string Reference { get => Measurement.Name; }
        protected virtual string Criterion { get; private set; }

        public string Label { get => $"NOT({Reference} {Criterion})"; }

        public Culler(Measurement measurement, string criterion)
            => (Measurement, Criterion) = (measurement, criterion);
    }
}
