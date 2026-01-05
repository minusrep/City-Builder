using Runtime.Descriptions;

namespace Runtime.Colony
{
    public class WorldGridModel
    {
        public WorldGridDescription Description { get; }
        
        private GridCellModel[,] Cells { get; }

        public WorldGridModel(WorldGridDescription description)
        {
            Description = description;
            
            Cells = new GridCellModel[Description.Width, Description.Height];

            for (var x = 0; x < Description.Width; x++)
            {
                for (var y = 0; y < Description.Height; y++)
                {
                    Cells[x, y] = new GridCellModel(x, y);
                }
            }
        }
    }
}