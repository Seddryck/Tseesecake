using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    public abstract class Sifter : IFilter
    {
        public Measurement Measurement { get; set; }

        public abstract string Template { get; }

        public Sifter(Measurement measurement)
            => Measurement = measurement;
    }
}
