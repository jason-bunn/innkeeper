using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnKeeper.Shared
{
    public class UIcon : ImageEntity
    {
        public UIcon(Texture2D tex, Vector2 pos, Color tint, CallBack callBack) : base(tex,pos, tint)
        {
            this.SetCallBack(callBack);
            this.IsVisible = true;
        }

        

        
    }
}
