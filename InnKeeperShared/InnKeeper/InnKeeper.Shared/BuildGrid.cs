using Microsoft.Xna.Framework;

namespace InnKeeper.Shared
{
    public class BuildGrid
    {
        public int GridSize { get; private set; }

        int screenWidth;
        int screenHeight;
        int numRows;
        int numColumns;

        public BuildGrid(int size, int screenWidth, int screenHeight)
        {
            this.SetGridSize(size);
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            numRows = screenHeight / size;
            numColumns = screenWidth / size;
        }

        public void SetGridSize(int size)
        {
            this.GridSize = size;
        }

        public Vector2 SnapToNearestNode(Vector2 position)
        {
            Vector2 temp = position;

            int remainderX = (int)position.X % GridSize;
            int remainderY = (int)position.Y % GridSize;
            int halfGrid = GridSize / 2;

            if(remainderX == 0 && remainderY == 0)
            {
                
            }
            else
            {
                if(remainderX <= halfGrid)
                {
                    temp.X = position.X - (remainderX);
                }
                else if(remainderX > halfGrid)
                {
                    temp.X = position.X + remainderX - ((remainderX - halfGrid)*2);
                }

                if(remainderY <= halfGrid)
                {
                    temp.Y = position.Y - (remainderY);
                }
                else
                {
                    temp.Y = position.Y + remainderY - ((remainderY - halfGrid) * 2);
                }
            }

            return temp;
        }
    }
}
