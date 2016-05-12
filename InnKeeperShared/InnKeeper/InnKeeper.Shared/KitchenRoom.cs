using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public class KitchenRoom : Room
    {
        public KitchenRoom(Texture2D tex, Vector2 pos, Color tint) : base(tex, pos, tint)
        {

            this.SetTexture(tex);
            this.SetPosition(pos);
            this.SetColor(tint);

            this.SetSourceRect(518, 3, 256, 128);
            this.SetBoundingBox(new Rectangle((int)pos.X, (int)pos.Y, 256, 128));
            this.Cost = 100;
            this.ExpenseRate = 10;
            this.ID = GameVariables.GetID();

            this.Layer = 1;
            this.IsVisible = true;
        }

    }
}
