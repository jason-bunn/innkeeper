using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public class GreatHallRoom : Room
    {
        public GreatHallRoom(Texture2D tex, Vector2 pos, Color tint) : base(tex, pos, tint)
        {
            this.SetTexture(tex);
            this.SetPosition(pos);
            this.SetColor(tint);

            this.SetSourceRect(9, 9, 512, 128);
            this.SetBoundingBox(new Rectangle((int)pos.X, (int)pos.Y, 512, 128));
            this.Cost = 500;
            this.ExpenseRate = 10;
            this.ID = GameVariables.GetID();

            this.IsVisible = true;
        }
    }
}
