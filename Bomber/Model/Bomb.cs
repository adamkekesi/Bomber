using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class Bomb : IDisposable
    {
        public event EventHandler? Exploded;

        public Point Position { get; private set; }

        public int Radius { get; private set; }

        private System.Timers.Timer timer;

        public Bomb(Point position, int timeTillExplosion, int radius)
        {
            Position = position;
            timer = new System.Timers.Timer(timeTillExplosion) { AutoReset = false };
            timer.Start();
            timer.Elapsed += OnTimeElapsed;
            Radius = radius;
        }

        private void OnTimeElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Exploded?.Invoke(this, e);
        }

        public virtual void Pause()
        {
            timer.Stop();
        }

        public virtual void Resume()
        {
            timer.Start();
        }

        public virtual void Dispose()
        {
            timer.Stop();
            timer.Dispose();
            Exploded = null;
        }
    }
}
