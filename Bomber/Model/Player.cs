using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class Player
    {
        public event EventHandler<EventArgs> PositionChanged;

        private Point position;

        public Point Position
        {
            get => position; set
            {
                if (position == value)
                {
                    return;
                }
                position = value;
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        
    }
}
