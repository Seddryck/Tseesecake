﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Tseesecake.Modeling
{
    public record class MeasurementExpression(string Name, Expression Expression)
    { }
}
