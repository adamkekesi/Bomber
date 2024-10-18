using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{

    public class Player : Unit
    {
        public event EventHandler? BombPlanted;

        public Player(Point startingPos) : base(startingPos)
        {
        }

        public override void OnCollision(IField otherField, Point point)
        {

        }

        public void PlantBomb()
        {
            if (!Alive)
            {
                return;
            }
            BombPlanted?.Invoke(this, EventArgs.Empty);
        }

        public override void Dispose()
        {
            base.Dispose();
            BombPlanted = null;
        }
    }
}
