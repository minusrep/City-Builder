namespace Runtime.Colony
{
    public class WorldGridModel
    {
        public int Width { get; }
        public int Height { get; }
        
        private GridCellModel[,] _cells;

        public WorldGridModel(int width, int height, float cellSize)
        {
            Width = width;
            Height = height;
            
            _cells = new GridCellModel[width, height];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    _cells[x, y] = new GridCellModel(x, y);
                }
            }
        }
    }
}