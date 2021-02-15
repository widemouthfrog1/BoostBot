using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
    class BoostPad
    {
        public Vector position;
        public int value;
        public float respawnTime;
        public float nextRespawn;
        
        public BoostPad(Vector position, int value)
        {
            this.position = position;
            this.value = value;
            respawnTime = value == 12 ? 4 : 10;
            nextRespawn = -1;
        }
    }
}
