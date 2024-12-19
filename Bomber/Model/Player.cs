using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{

    public class Player : Unit
    {
        public class PlayerDeadException : Exception
        {
            public PlayerDeadException()
            {
            }

            public PlayerDeadException(string? message) : base(message)
            {
            }

            public PlayerDeadException(string? message, Exception? innerException) : base(message, innerException)
            {
            }

        }
        public Player(IMap map, Point startingPos ) : base(startingPos, map)
        {
        }

        public override void OnCollision(IField otherField, Point point)
        {

        }

        public virtual Bomb PlantBomb(int timeTillExplosion, int radius)
        {
            if (!Alive)
            {
                throw new PlayerDeadException();
            }
            return new Bomb(Position, timeTillExplosion, radius);
        }
    }
}
