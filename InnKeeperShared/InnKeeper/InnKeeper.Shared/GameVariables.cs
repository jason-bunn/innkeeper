﻿using Microsoft.Xna.Framework;

namespace InnKeeper.Shared
{
    public static class GameVariables
    {

        static int RoomIDs = 0;

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
