using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public abstract class Unit : IField, IDisposable
    {
        protected Unit(Point startingPos, IMap map)
        {
            Position = startingPos;
            this.map = map;
        }

        public event EventHandler? Died;

        public bool Alive { get; private set; } = true;

        public Point Position { get; set; }

        protected IMap map;

        public abstract void OnCollision(IField otherField, Point point);

        public virtual void Move(Direction dir)
        {
            if (!Alive)
            {
                return;
            }
            map.Move(Position, dir);
        }

        public virtual void Kill()
        {
            Alive = false;
            OnDied();
        }

        public virtual void Dispose()
        {
            Died = null;
        }

        protected virtual void OnDied()
        {
            Died?.Invoke(this, EventArgs.Empty);
            map.RemoveField(Position);
            Dispose();
        }
    }
}
