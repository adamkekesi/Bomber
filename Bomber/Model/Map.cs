using Bomber.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class Map :  IMap
    {
        public class OutOfMapException : Exception
        {
            public Point Point { get; private set; }

            public OutOfMapException(Point point)
            {
                Point = point;
            }
        }

        public class MapChangedEventArgs : EventArgs
        {
            public Point[] AffectedCells { get; private set; }

            public MapChangedEventArgs(params Point[] affectedCells)
            {
                AffectedCells = affectedCells;
            }
        }

        public int Size => fields.GetLength(0);

        public IField? this[int i, int j] => fields[i, j];

        public IField? this[Point p] => this[p.X, p.Y];

        public event EventHandler<MapChangedEventArgs>? MapChanged;

        private readonly IField?[,] fields;

        public Map(CellContent[,] cells, Random r, out List<Enemy> enemies)
        {
            int n = cells.GetLength(0);
            enemies = new List<Enemy>();
            fields = new IField[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    switch (cells[i, j])
                    {
                        case CellContent.Enemy:
                            Enemy enemy = new Enemy(r, this, new Point(i, j));
                            fields[i, j] = enemy;
                            enemies.Add(enemy);

                            break;
                        case CellContent.Wall:
                            fields[i, j] = new Wall();

                            break;
                        case CellContent.Empty:
                            fields[i, j] = null;

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void PlacePlayer(Player player)
        {
            fields[0, 0] = player;
        }

        public Map(IField?[,] fields)
        {
            this.fields = fields;
        }

        public void RemoveField(int i, int j)
        {
            RemoveField(new Point(i, j));
        }

        public void RemoveField(Point pos)
        {
            if (IsPointOutOfMap(pos))
            {
                throw new OutOfMapException(pos);
            }
            fields[pos.X, pos.Y] = null;
            MapChanged?.Invoke(this, new MapChangedEventArgs(pos));
        }

        public void Move(int i, int j, Direction dir)
        {
            Move(new Point(i, j), dir);
        }

        public virtual void Move(Point pos, Direction dir)
        {
            lock (this)
            {
                if (IsPointOutOfMap(pos))
                {
                    throw new OutOfMapException(pos);
                }

                var field = fields[pos.X, pos.Y];
                if (field == null)
                {
                    return;
                }

                Point newPos = GetNewPos(pos, dir);

                if (IsPointOutOfMap(newPos))
                {
                    HandleCollision(field, new Wall(), newPos);
                    return;
                }

                var targetField = fields[newPos.X, newPos.Y];

                if (targetField != null)
                {
                    HandleCollision(field, targetField, newPos);
                    return;
                }

                if (field is Unit unit)
                {
                    unit.Position = newPos;
                }
                fields[pos.X, pos.Y] = null;
                fields[newPos.X, newPos.Y] = field;

                MapChanged?.Invoke(this, new MapChangedEventArgs(pos, newPos));
            }
        }

        public void ForEachInArea(Point origin, int radius, Action<IField> action)
        {
            for (int i = origin.X - radius; i < origin.X + radius + 1; i++)
            {
                for (int j = origin.Y - radius; j < origin.Y + radius + 1; j++)
                {
                    if (!IsPointOutOfMap(new Point(i, j)) && this[i, j] != null)
                    {
                        action(this[i, j]!);
                    }
                }
            }


        }

        public void Dispose()
        {
            MapChanged = null;
            foreach (var field in fields)
            {
                if (field is IDisposable d)
                {
                    d.Dispose();
                }
            }
        }

        private void HandleCollision(IField field, IField targetField, Point newPos)
        {
                targetField.OnCollision(field, newPos);
                field.OnCollision(targetField, newPos);
            Exception? ex = null;
            try
            {

            }
            catch (Exception e)
            {
                ex = e;
            }

            try
            {
            }
            catch (Exception e)
            {
                ex = e;
            }

            if (ex != null)
            {
                throw ex;
            }
        }

        private bool IsPointOutOfMap(Point p)
        {
            return p.X < 0 || p.X >= fields.GetLength(0) || p.Y < 0 || p.Y >= fields.GetLength(1);
        }

        private Point GetNewPos(Point pos, Direction dir)
        {
            int i = pos.X;
            int j = pos.Y;
            Point newPos = Point.Empty;

            switch (dir)
            {
                case Direction.Up:
                    newPos = new Point(i, j - 1);
                    break;
                case Direction.Down:
                    newPos = new Point(i, j + 1);
                    break;
                case Direction.Left:
                    newPos = new Point(i - 1, j);
                    break;
                case Direction.Right:
                    newPos = new Point(i + 1, j);
                    break;
                default:
                    break;
            }

            return newPos;
        }


    }
}
