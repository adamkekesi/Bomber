using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class BombCollection:IDisposable
    { 
        public class BombsChangedEventArgs
        {
            public Act MyProperty { get; private set; }
        }

        private const int bombExplodeTime = 2000;

        private const int bombRadius = 3;

        public event EventHandler<Map.MapChangedEventArgs>? BombsChanged;

        private readonly List<Bomb> bombs;

        public BombCollection()
        {
            bombs = new List<Bomb>();
        }

        public void PlantBomb(Bomb bomb)
        {
            bombs.Add(bomb);
        }

        public void RemoveBomb(Bomb bomb) 
        {
            bombs.Remove(bomb);
        }

        public void Pause() 
        {
            foreach (var bomb in bombs)
            {
                bomb.Pause();
            }
        }

        public void Resume()
        {
            foreach (var bomb in bombs)
            {
                bomb.Resume();
            }
        }

        public void Dispose()
        {
            BombAdded = null;
            BombRemoved = null;
            foreach (var bomb in bombs)
            {
                bomb.Dispose();
            }
        }
    }
}
