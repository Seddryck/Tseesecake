using DubUrl.Querying;
using DubUrl.Querying.Parametrizing;
using DubUrl.Querying.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying;

namespace Tseesecake.Mounting.Engine
{
    public class DmlStatement : EmbeddedSqlTemplateCommand
    {
        public DmlStatement(Timeseries ts)
            : base(
                   $"{typeof(DmlStatement).Namespace}.{nameof(DmlStatement)}"
                  , $"{typeof(DmlStatement).Namespace}"
                  , $"{typeof(DmlStatement).Namespace}"
                  , new Dictionary<string, object?>() { { "ts", ts } }
                  , NullQueryLogger.Instance
            )
        { }
    }
}
