using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
    class MapDimensions
    {
        public static Vector center = new Vector(0, 0, 0);
        private static float backWall = 5120;
        private static float backWallLength = 5888;
        private static float sideWall = 4096;
        private static float sideWallLength = 7936;
        public static float ground = 0;
        public static float ceiling = 2044;
        public static Vector blueBackWallLeft = new Vector(backWallLength/2, -backWall, 0);
        public static Vector orangeBackWallLeft = new Vector(backWallLength / 2, backWall, 0);
        public static Vector blueBackWallRight = new Vector(-backWallLength / 2, -backWall, 0);
        public static Vector orangeBackWallRight = new Vector(-backWallLength / 2, backWall, 0);

        public static Vector leftSideWallBlue = new Vector(sideWall, -sideWallLength / 2, 0);
        public static Vector leftSideWallOrange = new Vector(sideWall, sideWallLength / 2, 0);
        public static Vector rightSideWallBlue = new Vector(-sideWall, -sideWallLength / 2, 0);
        public static Vector rightSideWallOrange = new Vector(-sideWall, sideWallLength / 2, 0);
    }
}
