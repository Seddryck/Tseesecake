using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Modeling.Statements.Restrictions
{
    internal class LimitOffsetRestriction : LimitRestriction
    {
        public int Offset { get; }
        public override string Template { get => nameof(LimitOffsetRestriction); }

        public LimitOffsetRestriction(int value, int offset)
            : base(value) { Offset = offset; }
    }
}
