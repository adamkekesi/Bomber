using Bomber.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BomberView
{
    public partial class Cell : UserControl
    {
        private IField? field;

        private Bomb? bomb;

        public Cell(IField? field)
        {
            InitializeComponent();
            this.field = field;
            DrawField(field);
        }

        public void ReplaceField(IField? field)
        {
            this.field = field;
            DrawField(field);
        }

        public void PlantBomb(Bomb bomb)
        {
            this.bomb = bomb;
        }

        private void DrawField(Player p)
        {
            enemyUp.Visible = false;
            enemyDown.Visible = false;
            enemyLeft.Visible = false;
            enemyRight.Visible = false;
            wall.Visible = false;
            player.Visible = true;
        }

        private void DrawField(Wall w)
        {
            enemyUp.Visible = false;
            enemyDown.Visible = false;
            enemyLeft.Visible = false;
            enemyRight.Visible = false;
            wall.Visible = true;
            player.Visible = false;
        }

        private void DrawField(Enemy e)
        {
            enemyUp.Visible = false;
            enemyDown.Visible = false;
            enemyLeft.Visible = false;
            enemyRight.Visible = false;
            wall.Visible = false;
            player.Visible = false;

            switch (e.Orientation)
            {
                case Direction.Up:
                    enemyUp.Visible = true;

                    break;
                case Direction.Down:
                    enemyDown.Visible = true;

                    break;
                case Direction.Left:
                    enemyLeft.Visible = true;

                    break;
                case Direction.Right:
                    enemyRight.Visible = true;

                    break;
                default:
                    break;
            }
        }

        private void DrawField(IField? f)
        {
            SuspendLayout();
            if (f == null)
            {
                enemyUp.Visible = false;
                enemyDown.Visible = false;
                enemyLeft.Visible = false;
                enemyRight.Visible = false;
                wall.Visible = false;
                player.Visible = false;
                ResumeLayout();
                return;
            }

            if (f is Player p)
            {
                DrawField(p);
            }

            if (f is Enemy e)
            {
                DrawField(e);
            }

            if (f is Wall w)
            {
                DrawField(w);
            }
            ResumeLayout();

        }
    }
}
