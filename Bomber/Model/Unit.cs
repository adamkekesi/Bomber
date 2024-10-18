using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public abstract class Unit : IField, IDisposable
    {
        public class MovedEventArgs
        {
            public Point CurrentPos { get; private set; }

            public Direction Direction { get; private set; }

            public MovedEventArgs(Point currentPos, Direction direction)
            {
                CurrentPos = currentPos;
                Direction = direction;
            }
        }

        protected Unit(Point startingPos)
        {
            Position = startingPos;
        }

        public event EventHandler? Died;

        public event EventHandler<MovedEventArgs>? Moved;

        public bool Alive { get; private set; } = true;

        public Point Position { get; set; }

        public abstract void OnCollision(IField otherField, Point point);

        public virtual void Move(Direction dir)
        {
            if (!Alive)
            {
                return;
            }
            Moved?.Invoke(this, new MovedEventArgs(Position, dir));
        }

        public void Kill()
        {
            Alive = false;
            Died?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Dispose()
        {
            Moved = null;
            Died = null;
        }
    }
}
