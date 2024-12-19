using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bomber.Model.Map;

namespace Bomber.Model
{
    public interface IMap : IDisposable
    {
        public void Move(int i, int j, Direction dir);
        public void Move(Point pos, Direction dir);
        public void ForEachInArea(Point origin, int radius, Action<IField> action);
        public void RemoveField(int i, int j);
        public void RemoveField(Point pos);
        public void PlacePlayer(Player player);
        public IField? this[int i, int j] { get; }
        public IField? this[Point p] { get; }
        public int Size { get; }
        public event EventHandler<MapChangedEventArgs>? MapChanged;
    }
}
