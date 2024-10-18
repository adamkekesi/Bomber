using Bomber.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class Map: IDisposable
    {
        public class OutOfMapException : Exception
        {
            public Point Point { get; private set; }

            public OutOfMapException(Point point)
            {
                Point = point;
            }
        }

        public class MapChangedEventArgs: EventArgs
        {
            public Point[] AffectedCells { get; private set; }

            public MapChangedEventArgs(params Point[] affectedCells)
            {
                    AffectedCells=affectedCells;
            }
        }

        public event EventHandler<MapChangedEventArgs>? MapChanged;

        public IField?[,] Fields => fields;

        private readonly IField?[,] fields;

        public Map(CellContent[,] cells, Player player, Random r, out List<Enemy> enemies)
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
                            Enemy enemy = new Enemy(r, new Point(i, j));
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
            fields[0, 0] = player;
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

        public void Move(Point pos, Direction dir)
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
            }

            var targetField = fields[newPos.X, newPos.Y];

            if (targetField != null)
            {
                HandleCollision(field, targetField, newPos);
            }

            if (field is Unit unit)
            {
                unit.Position = newPos;
            }
            fields[pos.X, pos.Y] = null;
            fields[newPos.X, newPos.Y] = field;

            MapChanged?.Invoke(this, new MapChangedEventArgs(pos,newPos));
        }

        public void ApplyBlast(Point origin, int radius)
        {
            for (int i = origin.X - radius; i < origin.X + radius + 1; i++)
            {
                for (int j = origin.Y - radius; j < origin.Y + radius + 1; j++)
                {
                    if (!IsPointOutOfMap(new Point(i, j)) && fields[i,j] is Unit u)
                    {
                        u.Kill();
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
            Exception? ex = null;
            try
            {
                targetField.OnCollision(field, newPos);

            }
            catch (Exception e)
            {
                ex = e;
            }

            try
            {
                field.OnCollision(targetField, newPos);
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
