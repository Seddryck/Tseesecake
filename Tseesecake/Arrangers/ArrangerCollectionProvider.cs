using DubUrl.Querying.Dialects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Arrangers
{
    public sealed class ArrangerCollectionProvider : IArrangerCollectionProvider
    {
        public Assembly[] Assemblies { get; }
        private Dictionary<Type, IArrangerCollectionFactory>? factories;
        private Dictionary<Type, IArrangerCollectionFactory>? Factories { get => factories ??= Initialize(); }

        public ArrangerCollectionProvider()
            : this(new[] { typeof(ArrangerCollectionProvider).Assembly }) { }

        public ArrangerCollectionProvider(Assembly[] assemblies)
            => (Assemblies) = (assemblies);

        private Dictionary<Type, IArrangerCollectionFactory> Initialize()
        {
            var factories = Assemblies.Aggregate(
                                Array.Empty<Type>(), (factories, asm)
                                    => factories.Concat(asm.GetTypes()
                                                    .Where(x => x.IsClass && !x.IsAbstract)
                                                    .Where(x => x.GetInterfaces().Contains(typeof(IArrangerCollectionFactory)))
                            ).ToArray());

            var dico = new Dictionary<Type, IArrangerCollectionFactory>();
            foreach (var factory in factories)
            {
                var dialect = ((DialectAttribute)(factory.GetCustomAttribute(typeof(DialectAttribute)) ?? throw new InvalidOperationException())).Dialect;
                dico.Add(dialect, (IArrangerCollectionFactory)Activator.CreateInstance(factory)!);
            }
            return dico; 
        }

        public IArrangerCollectionFactory Get<T>() where T : IDialect
            => Get(typeof(T));

        public IArrangerCollectionFactory Get(Type dialect)
            => dialect.IsAssignableTo(typeof(IDialect)) 
                    ? Factories![dialect] 
                    : throw new ArgumentException(nameof(dialect));
    }
}
