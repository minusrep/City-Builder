using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Common.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _container;
        
        private readonly Stack<T> _pool = new();

        public ObjectPool(T prefab, int initialSize, Transform container)
        {
            _prefab = prefab;
            _container = container;

            for (var i = 0; i < initialSize; i++)
            {
                _pool.Push(CreateObject());
            }
        }

        public T Get()
        {
            if (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                obj.gameObject.SetActive(true);
                return obj;
            }

            return CreateObject();
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }

        private T CreateObject(bool isActive = false)
        {
            var obj = Object.Instantiate(_prefab, _container);
            obj.gameObject.SetActive(isActive);
            return obj;
        }
    }
}