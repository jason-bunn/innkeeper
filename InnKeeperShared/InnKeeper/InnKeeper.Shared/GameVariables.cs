using Microsoft.Xna.Framework;

namespace InnKeeper.Shared
{
    public static class GameVariables
    {

        static int RoomIDs = 0;

        public const int GROUNDHEIGHT = 500;

        public const int GRID_SIZE = 64;

        // Parallax amount
        public const float PARALLAX_AMOUNT = 0.25f;

        // Length of a day in seconds
        public const float DAY_CYCLE_LENGTH = 900.0f;

        // Camera speeds
        public const float CAMERA_MOVE_SPEED = 200.0f;
        public const float CAMERA_ZOOM_SPEED = 0.5f;

        // World dimensions
        public const int WORLD_WIDTH = 2536;
        public const int WORLD_HEIGHT = 1536;

        // Viewport dimensions
        public const int VIEWPORT_WIDTH = 960;
        public const int VIEWPORT_HEIGHT = 540;

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
