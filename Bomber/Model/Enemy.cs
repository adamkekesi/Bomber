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
        public event EventHandler? OrientationChanged;

        private readonly Random r;

        private Direction orientation;

        public Direction Orientation
        {
            get => orientation; private set
            {
                if (orientation == value)
                {
                    return;
                }
                orientation = value;
                OrientationChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Enemy(Random r, Point startingPos) : base(startingPos)
        {
            this.r = r;
        }

        public void Move()
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

        public override void Dispose()
        {
            base.Dispose();
            OrientationChanged = null;
        }
    }
}
