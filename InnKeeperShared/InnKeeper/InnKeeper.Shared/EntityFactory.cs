using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace InnKeeper.Shared
{
    public class EntityFactory
    {
        TextureManager texMan;
        public EntityFactory(TextureManager man)
        {
            this.texMan = man;
        }

        public Room CreateRoom(GameVariables.RoomTypes type)
        {
            Room temp = null;

            switch(type)
            {
                case GameVariables.RoomTypes.GREATHALL:
                    temp = new GreatHallRoom(texMan.GetTexture("Rooms"), new Vector2(200, 600), Color.White);
                    break;

                default:
                    temp = null;
                    break;
            }

            return temp;
        }

        public ImageEntity CreateIcon(GameVariables.IconTypes type)
        {
            ImageEntity temp = null;

            switch(type)
            {
                case GameVariables.IconTypes.BUILD:
                    temp = new ImageEntity(texMan.GetTexture("Icons"), new Vector2(1100, 600), Color.White);
                    temp.SetSourceRect(0, 0, 48, 48);
                    temp.SetBoundingBox(new Rectangle(1100, 600, 48, 48));
                    break;
                case GameVariables.IconTypes.BED:
                    temp = new ImageEntity(texMan.GetTexture("Icons"), new Vector2(1000, 600), Color.White);
                    temp.SetSourceRect(49, 0, 48, 48);
                    temp.SetBoundingBox(new Rectangle(1000, 600, 48, 48));
                    break;
                case GameVariables.IconTypes.MUG:
                    temp = new ImageEntity(texMan.GetTexture("Icons"), new Vector2(1100, 500), Color.White);
                    temp.SetSourceRect(97, 0, 48, 48);
                    temp.SetBoundingBox(new Rectangle(1000, 600, 48, 48));
                    break;
            }

            return temp;
        }
    }
}
