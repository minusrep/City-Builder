namespace Runtime.Colony.Buildings.Selection
{
    public interface ISelectableUnit
    {
        public string Id { get; }
        
        public SelectionOutline Outline { get; }
    }
}