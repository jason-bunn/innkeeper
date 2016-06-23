using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace InnKeeper.Shared
{
    public class EntityFactory
    {
        TextureManager texMan;
        GameController controller;

        public EntityFactory(TextureManager man, GameController controller)
        {
            this.texMan = man;
            this.controller = controller;
        }

        public Room CreateRoom(GameVariables.RoomTypes type)
        {
            Room temp = null;

            switch(type)
            {
                case GameVariables.RoomTypes.GREATHALL:
                    temp = new GreatHallRoom(texMan.GetTexture("Rooms"), new Vector2(200, 600), Color.White);
                    break;

                case GameVariables.RoomTypes.LOWBED:
                    temp = new LowQualityLodgingRoom(texMan.GetTexture("Rooms"), new Vector2(200, 600), Color.White);
                    break;

                case GameVariables.RoomTypes.KITCHEN:
                    temp = new KitchenRoom(texMan.GetTexture("Rooms"), new Vector2(200, 600), Color.White);
                    break;

                default:
                    temp = null;
                    break;
            }

            return temp;
        }

        public ImageEntity CreateIcon(GameVariables.IconTypes type, Entity.CallBack callBack)
        {
            UIcon temp = null;

            Vector2 displayDimensions = controller.GetDeviceDimensions();

            switch(type)
            {
                case GameVariables.IconTypes.BUILD:
                    temp = new UIcon(texMan.GetTexture("Icons"), new Vector2(displayDimensions.X - 96, displayDimensions.Y - 48), Color.White, callBack);
                    temp.SetSourceRect(0, 0, 48, 48);
                    temp.SetBoundingBox(new Rectangle((int)temp.Position.X, (int)temp.Position.Y, 48, 48));
                    
                    break;
                case GameVariables.IconTypes.BED:
                    temp = new UIcon(texMan.GetTexture("Icons"), new Vector2(displayDimensions.X - 192, displayDimensions.Y - 48), Color.White, callBack);
                    temp.SetSourceRect(49, 0, 48, 48);
                    temp.SetBoundingBox(new Rectangle((int)temp.Position.X, (int)temp.Position.Y, 48, 48));
                    
                    break;
                case GameVariables.IconTypes.MUG:
                    temp = new UIcon(texMan.GetTexture("Icons"), new Vector2(displayDimensions.X - 96, displayDimensions.Y - 144), Color.White, callBack);
                    temp.SetSourceRect(97, 0, 48, 48);
                    temp.SetBoundingBox(new Rectangle((int)temp.Position.X, (int)temp.Position.Y, 48, 48));
                    
                    break;
            }

            return temp;
        }
    }
}
