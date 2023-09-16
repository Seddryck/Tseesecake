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
        internal DmlStatement(Timeseries ts)
            : this(
                   ts
                  , NullQueryLogger.Instance
            )
        { }

        public DmlStatement(Timeseries ts, IQueryLogger logger)
            : base(
                   $"{typeof(DmlStatement).Namespace}.{nameof(DmlStatement)}"
                  , $"{typeof(DmlStatement).Namespace}"
                  , $"{typeof(DmlStatement).Namespace}"
                  , new Dictionary<string, object?>() { { "ts", ts } }
                  , logger
            )
        { }
    }
}
