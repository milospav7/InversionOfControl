using System;
using System.Collections.Generic;
using System.Linq;

namespace IoC
{
    public class Container
    {
        private readonly Dictionary<Type, Func<object>> _resolvedTypes = new Dictionary<Type, Func<object>>(); // kind of registered service for specific interface
        // func here is just a way to create instance when we get item from dict, its a lambda - we are not storing  object while adding new into dict!

        public void Register<TIn, TOut>()
        {
            _resolvedTypes.Add(typeof(TIn), () => GetInstance(typeof(TOut)));
        }

        public void RegisterSingleton<T>(T obj)
        {
            _resolvedTypes.Add(typeof(T), () => obj);
        }

        public T GetInstance<T>()
        {
            return (T)GetInstance(typeof(T));
        }

        public object GetInstance(Type type)
        {
            if (_resolvedTypes.ContainsKey(type))
                return _resolvedTypes[type](); // this () actully creates instance

            var constructor = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .First();

            var args = constructor.GetParameters()
                .Select(p => GetInstance(p.ParameterType))
                .ToArray();

            return Activator.CreateInstance(type, args);
        }
    }
}
