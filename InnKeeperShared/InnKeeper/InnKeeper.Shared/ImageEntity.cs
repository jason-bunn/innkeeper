using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public class ImageEntity : Entity
    {

        public ImageEntity() : base()
        {

        }

        public ImageEntity(Texture2D tex, Vector2 pos, Color tint) : base(tex, pos, tint)
        {
            this.SetSourceRect(0, 0, tex.Width, tex.Height);
            
        }

        public void SetSourceRect(int x, int y, int width, int height)
        {
            this.SourceRect = new Rectangle(x, y, width, height);
            
        }

    }
}
