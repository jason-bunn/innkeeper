using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public abstract class Room : ImageEntity
    {
        public GameVariables.RoomTypes Type { get; private set; }

        public int Cost {get; set;}

        public int ID { get; set; }

        public int ExpenseRate { get; set; }
        public int IncomeRate { get; set; }

        public Room() : base()
        {

        }

        public Room(Texture2D tex, Vector2 pos, Color tint) : base(tex, pos, tint)
        {
            //this.SetTexture(tex);
            //this.SetPosition(pos);
            //this.SetColor(tint);
        }

        public void SetRoomType(GameVariables.RoomTypes type)
        {
            this.Type = type;
        }

    }
}
