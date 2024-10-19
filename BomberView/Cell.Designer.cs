namespace BomberView
{
    partial class Cell
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bombContainer = new PictureBox();
            enemy = new PictureBox();
            wall = new PictureBox();
            player = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)bombContainer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wall).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            SuspendLayout();
            // 
            // bombContainer
            // 
            bombContainer.Dock = DockStyle.Fill;
            bombContainer.InitialImage = Properties.Resources.empty;
            bombContainer.Location = new Point(0, 0);
            bombContainer.Margin = new Padding(3, 2, 3, 2);
            bombContainer.Name = "bombContainer";
            bombContainer.Size = new Size(131, 112);
            bombContainer.SizeMode = PictureBoxSizeMode.CenterImage;
            bombContainer.TabIndex = 0;
            bombContainer.TabStop = false;
            bombContainer.Visible = false;
            // 
            // enemy
            // 
            enemy.Dock = DockStyle.Fill;
            enemy.Image = Properties.Resources.enemy;
            enemy.Location = new Point(0, 0);
            enemy.Margin = new Padding(3, 2, 3, 2);
            enemy.Name = "enemy";
            enemy.Size = new Size(131, 112);
            enemy.SizeMode = PictureBoxSizeMode.Zoom;
            enemy.TabIndex = 3;
            enemy.TabStop = false;
            enemy.Visible = false;
            // 
            // wall
            // 
            wall.Dock = DockStyle.Fill;
            wall.Image = Properties.Resources.wall;
            wall.InitialImage = Properties.Resources.wall;
            wall.Location = new Point(0, 0);
            wall.Margin = new Padding(3, 2, 3, 2);
            wall.Name = "wall";
            wall.Size = new Size(131, 112);
            wall.SizeMode = PictureBoxSizeMode.Zoom;
            wall.TabIndex = 5;
            wall.TabStop = false;
            wall.Visible = false;
            // 
            // player
            // 
            player.Dock = DockStyle.Fill;
            player.Image = Properties.Resources.player;
            player.InitialImage = Properties.Resources.player;
            player.Location = new Point(0, 0);
            player.Margin = new Padding(3, 2, 3, 2);
            player.Name = "player";
            player.Size = new Size(131, 112);
            player.SizeMode = PictureBoxSizeMode.Zoom;
            player.TabIndex = 6;
            player.TabStop = false;
            player.Visible = false;
            // 
            // Cell
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(bombContainer);
            Controls.Add(player);
            Controls.Add(wall);
            Controls.Add(enemy);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Cell";
            Size = new Size(131, 112);
            ((System.ComponentModel.ISupportInitialize)bombContainer).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy).EndInit();
            ((System.ComponentModel.ISupportInitialize)wall).EndInit();
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox bombContainer;
        private PictureBox enemy;
        private PictureBox wall;
        private PictureBox player;
    }
}
