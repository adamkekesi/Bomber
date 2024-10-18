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

        private System.Timers.Timer timer;

        public Bomb(Point position, int timeTillExplosion)
        {
            Position = position;
            timer = new System.Timers.Timer(timeTillExplosion) { AutoReset = false };
            timer.Elapsed += OnTimeElapsed;
        }

        private void OnTimeElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Exploded?.Invoke(this, e);
        }

        public void Pause()
        {
            timer.Stop();
        }

        public void Resume()
        {
            timer.Start();
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
            Exploded = null;
        }
    }
}
