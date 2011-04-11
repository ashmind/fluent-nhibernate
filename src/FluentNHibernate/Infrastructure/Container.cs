using System;
using System.Collections.Generic;

namespace FluentNHibernate.Infrastructure
{
    public class Container
    {
        private readonly IDictionary<Type, Func<Container, object>> registeredTypes = new Dictionary<Type, Func<Container, object>>();

        public void Register<T>(Func<Container, T> instantiateFunc)
        {
            registeredTypes[typeof(T)] = c => (object)instantiateFunc(c);
        }

        public object Resolve(Type type)
        {
            if (!registeredTypes.ContainsKey(type))
                throw new ResolveException(type);
            
            var instantiationFunc = registeredTypes[type];

            return instantiationFunc(this);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
    }
}