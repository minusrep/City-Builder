using System;
using System.Collections.Generic;

namespace Runtime.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public void Register<T>(T service) where T : IService
        {
            _services.Add(typeof(T), service);
        }

        public T Get<T>() where T : IService
        {
            _services.TryGetValue(typeof(T), out var service);
            
            return (T) service;
        }
    }
}