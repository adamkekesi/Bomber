using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class BombCollection : IDisposable
    {
        public enum ChangeType
        {
            Added,
            Removed
        }

        public class BombsChangedEventArgs
        {
            public ChangeType Change { get; private set; }

            public Bomb Bomb { get; private set; }

            public BombsChangedEventArgs(ChangeType change, Bomb bomb)
            {
                Change = change;
                Bomb = bomb;
            }
        }


        public event EventHandler<BombsChangedEventArgs>? BombsChanged;

        private readonly List<Bomb> bombs;

        public BombCollection()
        {
            bombs = new List<Bomb>();
        }

        public BombCollection(List<Bomb> list)
        {
            bombs = list;
        }

        public virtual void PlantBomb(Bomb bomb)
        {
            bombs.Add(bomb);
            bomb.Exploded += OnBombExploded;
            BombsChanged?.Invoke(this, new BombsChangedEventArgs(ChangeType.Added, bomb));
        }

        private void OnBombExploded(object? sender, EventArgs e)
        {
            if (sender is Bomb bomb)
            {
                Remove(bomb);
            }
        }

        public virtual void Remove(Bomb bomb)
        {
            bombs.Remove(bomb);
            bomb.Dispose();
            BombsChanged?.Invoke(this, new BombsChangedEventArgs(ChangeType.Removed, bomb));
        }

        public virtual void Pause()
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
            BombsChanged = null;
            foreach (var bomb in bombs)
            {
                bomb.Dispose();
            }
        }
    }
}
