using Microsoft.Xna.Framework;

namespace InnKeeper.Shared
{
    public static class GameVariables
    {

        static int RoomIDs = 0;

        public const int GROUNDHEIGHT = 564;

        public const int GRID_SIZE = 64;

        // Length of a day in seconds
        public const float DAY_CYCLE_LENGTH = 900.0f;
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
