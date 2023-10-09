using DubUrl.Mapping;
using DubUrl.Querying.Dialects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Arrangers
{
    public abstract class BaseArrangerCollectionFactory : IArrangerCollectionFactory
    {
        public Assembly[] Assemblies { get; }

        public BaseArrangerCollectionFactory()
            : this(new[] { typeof(BaseArrangerCollectionFactory).Assembly }) { }

        public BaseArrangerCollectionFactory(Assembly[] assemblies)
            => (Assemblies) = (assemblies);

        public IArranger[] Instantiate<IStatement>()
            =>  InstantiatePolyglot(Assemblies)
                    .Union(InstantiateDialect()).ToArray();

        protected virtual IArranger[] InstantiatePolyglot(Assembly[] assemblies)
            => Assemblies.Aggregate(
                        Array.Empty<Type>(), (arrangers, asm)
                            => arrangers.Concat(asm.GetTypes()
                                            .Where(x => x.IsClass && !x.IsAbstract)
                                            .Where(x => x.GetInterfaces().Contains(typeof(IArranger)))
                                            .Where(x => x.GetCustomAttribute(typeof(PolyglotAttribute)) is not null)
                    ).ToArray())
                    .Select(x => (IArranger)Activator.CreateInstance(x)!)
                    .ToArray();

        protected abstract IArranger[] InstantiateDialect();
    }
}
