using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class Enemy : Unit
    {
        private readonly Random r;

        public Direction Orientation { get; private set; }

        public Enemy(Random r, IMap map, Point startingPos) : base(startingPos, map)
        {
            this.r = r;
            this.Orientation = Direction.Up;
        }

        public virtual void Move()
        {
            Move(Orientation);
        }

        public override void OnCollision(IField otherField, Point pos)
        {
            if (otherField is Player p)
            {
                p.Kill();
            }

            if (otherField is Wall)
            {
                Direction[] availableDirections = ((Direction[])Enum.GetValues(typeof(Direction))).Where(d => d != Orientation)
                                                                                                  .ToArray();

                Orientation = availableDirections[r.Next(availableDirections.Length)];
            }
        }

    }
}
