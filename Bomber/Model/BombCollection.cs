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

        private readonly IMap map;

        public BombCollection(IMap map)
        {
            bombs = new List<Bomb>();
            this.map = map;
        }

        public BombCollection(List<Bomb> list, IMap map)
        {
            bombs = list;
            this.map = map;
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
                map.ForEachInArea(bomb.Position, bomb.Radius, (field) =>
                {
                    if (field is Unit unit)
                    {
                        unit.Kill();
                    }
                });
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
