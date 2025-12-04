using System;

namespace Runtime.Colony.ModelCollections
{
    public interface IModelCollection<out T> where T : ISerializeModel, IDeserializeModel
    {
        event Action<T> OnCreateModel;
        event Action<T> OnDeleteModel;
    }
}