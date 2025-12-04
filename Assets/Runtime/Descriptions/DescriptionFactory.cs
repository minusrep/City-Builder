using System.Collections.Generic;
using System;

namespace Runtime.Descriptions
{
    public class DescriptionFactory
    {
        private readonly Dictionary<string, Type> _entityTypes = new ();
    
        public void Register<T>(string type) where T : class
        {
            _entityTypes.Add(type, typeof(T));
        }
    
        public T Create<T>(string id, Dictionary<string, object> dictionary) where T : class
        {
            return (T)Activator.CreateInstance(_entityTypes[dictionary["type"].ToString()], id, dictionary);
        }
    }
}