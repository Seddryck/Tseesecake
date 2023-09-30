using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Modeling
{
    internal class EngineDictionary<T> : IEnumerable<T>
    {
        private readonly Dictionary<Type, T> InternalDictionary = new();

        public bool TryGetValue<S>([NotNullWhen(true)] out T? value) where S : IStatement
            => TryGetValue(typeof(S), out value);

        
        public bool TryGetValue(Type type, [NotNullWhen(true)] out T? value)
        {
            if (!typeof(IStatement).IsAssignableFrom(type))
                throw new UnexpectedTypeException<IStatement>(type);

            if (InternalDictionary.TryGetValue(type, out value))
                return value is not null ? true : throw new InvalidOperationException();

            foreach (var t in GetParentTypes(type))
                if (InternalDictionary.TryGetValue(t, out value))
                    return value is not null ? true : throw new InvalidOperationException();
            return false;
        }

        public void Add<S>(T value) where S : IStatement
            => Add(typeof(S), value);

        public void Add(Type type, T value)
        {
            if (!typeof(IStatement).IsAssignableFrom(type))
                throw new UnexpectedTypeException<IStatement>(type);
            InternalDictionary.Add(type, value);
        }

        protected virtual IEnumerable<Type> GetParentTypes(Type type)
        {
            if (type == null)
                yield break;

            foreach (var @interface in type.GetInterfaces())
                if (@interface != typeof(IStatement) && @interface.GetInterfaces().Contains(typeof(IStatement)))
                    yield return @interface;

            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType = currentBaseType.BaseType;
            }
        }

        public IEnumerator<T> GetEnumerator() => InternalDictionary.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => InternalDictionary.Values.GetEnumerator();
    }

    public class UnexpectedTypeException<E> : TseesecakeException
    {
        public UnexpectedTypeException(Type type)
            : base($"The type '{type.Name}' is not supported. You must use a type implementing '{typeof(E).Name}'") { }
    }
}