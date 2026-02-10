using UnityEngine;

namespace Runtime.Common.ObjectPool
{
    public interface IObjectPool<T>  where T : Component
    {
        T Get();
        
        void Release(T obj);
    }
}