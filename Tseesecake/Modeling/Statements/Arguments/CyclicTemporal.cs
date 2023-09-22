using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling.Statements.Arguments
{
    public enum CyclicTemporal
    {
        Undefined = 0,
        DayOfWeek,
        DayOfMonth,
        DayOfYear,
        WeekOfYear,
        MonthOfYear,
        QuarterOfYear,
    }
}
