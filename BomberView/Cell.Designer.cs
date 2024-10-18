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
            enemyUp = new PictureBox();
            enemyDown = new PictureBox();
            enemyLeft = new PictureBox();
            enemyRight = new PictureBox();
            wall = new PictureBox();
            player = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)bombContainer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemyUp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemyDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemyLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemyRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wall).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            SuspendLayout();
            // 
            // bombContainer
            // 
            bombContainer.Dock = DockStyle.Fill;
            bombContainer.InitialImage = Properties.Resources.empty;
            bombContainer.Location = new Point(0, 0);
            bombContainer.Name = "bombContainer";
            bombContainer.Size = new Size(150, 150);
            bombContainer.SizeMode = PictureBoxSizeMode.CenterImage;
            bombContainer.TabIndex = 0;
            bombContainer.TabStop = false;
            bombContainer.Visible = false;
            // 
            // enemyUp
            // 
            enemyUp.Dock = DockStyle.Fill;
            enemyUp.Image = Properties.Resources.enemy_up;
            enemyUp.InitialImage = Properties.Resources.enemy_up;
            enemyUp.Location = new Point(0, 0);
            enemyUp.Name = "enemyUp";
            enemyUp.Size = new Size(150, 150);
            enemyUp.SizeMode = PictureBoxSizeMode.Zoom;
            enemyUp.TabIndex = 1;
            enemyUp.TabStop = false;
            enemyUp.Visible = false;
            // 
            // enemyDown
            // 
            enemyDown.Dock = DockStyle.Fill;
            enemyDown.Image = Properties.Resources.enemy_down;
            enemyDown.InitialImage = Properties.Resources.enemy_down;
            enemyDown.Location = new Point(0, 0);
            enemyDown.Name = "enemyDown";
            enemyDown.Size = new Size(150, 150);
            enemyDown.SizeMode = PictureBoxSizeMode.Zoom;
            enemyDown.TabIndex = 2;
            enemyDown.TabStop = false;
            enemyDown.Visible = false;
            // 
            // enemyLeft
            // 
            enemyLeft.Dock = DockStyle.Fill;
            enemyLeft.Image = Properties.Resources.enemy_left;
            enemyLeft.InitialImage = Properties.Resources.enemy_left;
            enemyLeft.Location = new Point(0, 0);
            enemyLeft.Name = "enemyLeft";
            enemyLeft.Size = new Size(150, 150);
            enemyLeft.SizeMode = PictureBoxSizeMode.Zoom;
            enemyLeft.TabIndex = 3;
            enemyLeft.TabStop = false;
            enemyLeft.Visible = false;
            // 
            // enemyRight
            // 
            enemyRight.Dock = DockStyle.Fill;
            enemyRight.Image = Properties.Resources.enemy_right;
            enemyRight.InitialImage = Properties.Resources.enemy_right;
            enemyRight.Location = new Point(0, 0);
            enemyRight.Name = "enemyRight";
            enemyRight.Size = new Size(150, 150);
            enemyRight.SizeMode = PictureBoxSizeMode.Zoom;
            enemyRight.TabIndex = 4;
            enemyRight.TabStop = false;
            enemyRight.Visible = false;
            // 
            // wall
            // 
            wall.Dock = DockStyle.Fill;
            wall.Image = Properties.Resources.wall;
            wall.InitialImage = Properties.Resources.wall;
            wall.Location = new Point(0, 0);
            wall.Name = "wall";
            wall.Size = new Size(150, 150);
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
            player.Name = "player";
            player.Size = new Size(150, 150);
            player.SizeMode = PictureBoxSizeMode.Zoom;
            player.TabIndex = 6;
            player.TabStop = false;
            player.Visible = false;
            // 
            // Cell
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(player);
            Controls.Add(wall);
            Controls.Add(enemyRight);
            Controls.Add(enemyLeft);
            Controls.Add(enemyDown);
            Controls.Add(enemyUp);
            Controls.Add(bombContainer);
            Name = "Cell";
            ((System.ComponentModel.ISupportInitialize)bombContainer).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemyUp).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemyDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemyLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemyRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)wall).EndInit();
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox bombContainer;
        private PictureBox enemyUp;
        private PictureBox enemyDown;
        private PictureBox enemyLeft;
        private PictureBox enemyRight;
        private PictureBox wall;
        private PictureBox player;
    }
}
