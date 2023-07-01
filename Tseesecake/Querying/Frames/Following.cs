﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.Frames
{
    internal class Following : Boundary
    {
        public Following(IExpression expression)
            : base(expression, "following") { }
    }
}
