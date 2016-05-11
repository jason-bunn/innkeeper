using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public class LowQualityLodgingRoom : Room
    {
        public LowQualityLodgingRoom(Texture2D tex, Vector2 pos, Color tint) : base(tex, pos, tint)
        {
            this.SetTexture(tex);
            this.SetPosition(pos);
            this.SetColor(tint);

            this.SetSourceRect(8, 144, 256, 128);
            this.SetBoundingBox(new Rectangle((int)pos.X, (int)pos.Y, 256, 128));
            this.Cost = 50;
            this.ExpenseRate = 5;
            this.ID = GameVariables.GetID();

            this.Layer = 1;
            this.IsVisible = true;
        }
    }
}
