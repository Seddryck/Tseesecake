using DubUrl.Querying;
using DubUrl.Querying.Parametrizing;
using DubUrl.Querying.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Engine.Mounting
{
    public class DmlCommand : EmbeddedSqlTemplateCommand
    {
        internal DmlCommand(Timeseries ts)
            : this(
                   ts
                  , NullQueryLogger.Instance
            )
        { }

        public DmlCommand(Timeseries ts, IQueryLogger logger)
            : base(
                   $"{typeof(DmlCommand).Namespace}.{nameof(DmlCommand)}"
                  , $"{typeof(DmlCommand).Namespace}"
                  , $"{typeof(DmlCommand).Namespace}"
                  , new Dictionary<string, object?>() { { "ts", ts } }
                  , logger
            )
        { }
    }
}
