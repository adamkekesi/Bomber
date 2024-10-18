using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class HitAWallException: Exception
    {
        public Point Wall { get; private set; }

        public HitAWallException(Point wall)
        {
            Wall = wall;
        }
    }
}
