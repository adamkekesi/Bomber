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
            enemy.Visible = false;
            wall.Visible = false;
            player.Visible = true;
        }

        private void DrawField(Wall w)
        {
            enemy.Visible = false;
            wall.Visible = true;
            player.Visible = false;
        }

        private void DrawField(Enemy e)
        {
            enemy.Visible = true;
            wall.Visible = false;
            player.Visible = false;

            
        }

        private void DrawField(IField? f)
        {
            SuspendLayout();
            if (f == null)
            {
                enemy.Visible = false;
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
