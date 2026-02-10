namespace Runtime.Selection
{
    public interface ISelectableUnit : IRenderableIcon
    {
        public string Id { get; }
        
        public SelectionOutline Outline { get; }
    }
}