﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public abstract record class Column
    (
        string Name,
        DbType DbType
    )
    { }
}
