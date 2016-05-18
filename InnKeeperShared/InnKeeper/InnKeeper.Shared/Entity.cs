using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public abstract class Entity
    {
        public Vector2 Position { get; private set; }
        public Texture2D SpriteTexture { get; private set; }
        public Color Tint { get; private set; }
        public Rectangle BoundingBox { get; private set; }
        public Rectangle SourceRect { get; set; }
        public uint Layer { get; set; }


        public bool IsVisible { get; set; }
        public delegate void CallBack();

        public CallBack Action { get; private set; }

        public Entity()
        {
            this.Position = Vector2.Zero;
            this.SpriteTexture = null;
            this.Tint = Color.White;
            this.Layer = 0;

            Action = null;
        }

        public Entity(Texture2D tex, Vector2 pos, Color tint)
        {
            this.Position = pos;
            this.SpriteTexture = tex;
            this.Tint = tint;
            this.Layer = 0;

            Action = null;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }

        public void SetColor(Color color)
        {
            this.Tint = color;
        }

        public void SetPosition(Vector2 newPos)
        {
            this.Position = newPos;
            this.SetBoundingBox(new Rectangle((int)newPos.X, (int)newPos.Y, this.BoundingBox.Width, this.BoundingBox.Height));
        }

        public void SetTexture(Texture2D tex)
        {
            this.SpriteTexture = tex;
        }

        public void SetBoundingBox(Rectangle newRect)
        {
            this.BoundingBox = newRect;
            
        }

        public void SetCallBack(CallBack callBack)
        {
            this.Action = callBack;
        }
        
    }
}
