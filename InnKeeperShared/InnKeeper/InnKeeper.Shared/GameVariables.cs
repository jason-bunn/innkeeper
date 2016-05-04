using Microsoft.Xna.Framework;

namespace InnKeeper.Shared
{
    public static class GameVariables
    {

        static int RoomIDs = 0;

        public const int GROUNDHEIGHT = 564;

        public const int GRID_SIZE = 64;

        public enum RoomTypes
        {
            GREATHALL,
            KITCHEN,
            LOWBED,
            MIDBED,
            HIGHBED,
            DININGHALL
        }

        public enum IconTypes
        {
            BUILD,
            BED,
            MUG,
            CANCEL
        }

        public static int UpdateInnTimeInSeconds = 60;

        public static int GetID()
        {
            int id = RoomIDs;

            RoomIDs++;

            return id;
        }

        
    }
}
