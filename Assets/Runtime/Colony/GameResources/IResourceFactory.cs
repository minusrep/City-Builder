namespace Runtime.Colony.GameResources
{
    public interface IResourceFactory
    {
        ResourceModel Create(string id);
    }
}