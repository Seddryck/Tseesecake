using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Parsing;

namespace Tseesecake.Querying.Filters
{
    internal class FilterFactory
    {
        private IDictionary<string, Type>? _mapping;
        protected IDictionary<string, Type> Mapping { get => _mapping ??= Initialize(); }

        protected IDictionary<string, Type> Initialize()
        {
            var mapping = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);
            LocateFilters().ToList().ForEach(x => mapping.Add(x.Name, x));
            return mapping;
        }

        protected IEnumerable<Type> LocateFilters()
            => GetType().Assembly.GetTypes().Where(x => 
                    x.GetInterfaces().Contains(typeof(IFilter))
                    && !x.IsAbstract
               );

        public IFilter Instantiate(string @class, string name, string identifier, object[] arguments)
        {
            if (!Mapping.TryGetValue(name + @class, out var type))
                throw new ArgumentOutOfRangeException();
            
            Column column = @class switch
            {
                string x when x.Equals("Temporizer") => new Timestamp(identifier),
                string y when y.Equals("Dicer") => new Facet(identifier),
                _ => new Measurement(identifier),
            };

            var parameters = new List<object> { column };
            parameters.AddRange(arguments);
            var filter = Instantiate<IFilter>(type, parameters.ToArray());
            return filter;
        }

        protected T Instantiate<T>(Type type, object[] parameters)
        {
            var ctor = GetMatchingConstructor(type, parameters.Length);
            return (T)ctor.Invoke(parameters);
        }

        protected internal ConstructorInfo GetMatchingConstructor(Type type, int paramCount)
            => type.GetConstructors().SingleOrDefault(x => x.GetParameters().Length == paramCount)
                ?? throw new ArgumentException();
    }
}
