using System;
using System.Collections.Generic;

namespace Runtime.Descriptions
{
    public class DescriptionFactory
    {
        private readonly Dictionary<string, Type> _entityTypes = new ();
    
        public void Register(string type, Type obj)
        {
            _entityTypes.Add(type, obj);
        }
    
        public T Create<T>(Dictionary<string, object> dictionary)
        {
            return (T)Activator.CreateInstance(_entityTypes[dictionary["type"].ToString()], dictionary);
        }
    }
}