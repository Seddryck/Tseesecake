﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Arrangers
{
    public interface IArranger
    {
        void Execute(SelectStatement statement);
    }
}
