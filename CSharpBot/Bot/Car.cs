using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bot.Utilities.Processed.Packet;

namespace Bot
{
    
    class Car
    {
        public static float maxSpeed = 2300;
        public static float superSonic = 2200;
        public static float maxDrivingSpeed = 1410;
        public static float mass = 180;
        public static float boostConsumptionRate = 100/3; // uu/s
        public static float throttleAccelerationInAir = 100 * 2 / 3; // uu/s^2
        public static float boostAcceleration = 991 + 2 / 3; // uu/s^2
        public static float brakeAcceleration = -3500; //uu/s^2 -> forwards or backwards
        public static float zeroThrottleAcceleration = -525; // uu/s^2
        public static float instantJump = 291 + 2 / 3; // uu/s^2 on first physics tick
        public static float jumpAcceleration = 1458.333374f; // uu/s^2

        public static float yawAcceleration = 9.11f; // radians/s^2
        public static float pitchAcceleration = 12.46f; // radians/s^2
        public static float rollAcceleration = 38.34f; // radians/s^2
        public static float maxAngularAcceleration = 5.5f; // radians/s
        
        public Vector pos;
        public Vector velocity;
        public Vector angularVelocity;
        public Player player;

        public Car(Player player)
        {
            Physics p = player.Physics;
            //Using my own data type for vectors
            pos = new Vector(p.Location);
            velocity = new Vector(p.Velocity);
            angularVelocity = new Vector(p.AngularVelocity);

            this.player = player;
        }

        public float AccelerationOnGround(float speed)
        {
            if(speed > 1410)
            {
                return 0;
            }
            return -1440*speed / 1400 + 1600;
        }

    }
}
