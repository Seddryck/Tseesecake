using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Modeling.Statements.Restrictions
{
    internal class LimitRestriction : IRestriction
    {
        public int Value { get; }
        public virtual string Template { get => nameof(LimitRestriction); }

        public LimitRestriction(int value)
            => Value = value;
    }
}
