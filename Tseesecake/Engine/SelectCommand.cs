using DubUrl.Querying;
using DubUrl.Querying.Parametrizing;
using DubUrl.Querying.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Engine
{
    public class SelectCommand : EmbeddedSqlTemplateCommand
    {
        internal SelectCommand(SelectStatement statement)
            : this(
                  statement
                  , NullQueryLogger.Instance
            )
        { }

        public SelectCommand(SelectStatement statement, IQueryLogger logger)
            : base(
                   $"{typeof(SelectCommand).Namespace}.{nameof(SelectCommand)}"
                  , $"{typeof(SelectCommand).Namespace}"
                  , $"{typeof(SelectCommand).Namespace}"
                  , new Dictionary<string, object?>() { { "statement", statement } }
                  , logger
            )
        { }
    }
}