namespace BomberView
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            openFileDialogMenuItem = new ToolStripMenuItem();
            scoreBoard = new Panel();
            enemiesKilledLabel = new Label();
            timeElapsedLabel = new Label();
            label2 = new Label();
            label1 = new Label();
            gameContainer = new Panel();
            menuStrip.SuspendLayout();
            scoreBoard.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(6, 3, 0, 3);
            menuStrip.Size = new Size(882, 30);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { openFileDialogMenuItem });
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(46, 24);
            fileMenu.Text = "File";
            // 
            // openFileDialogMenuItem
            // 
            openFileDialogMenuItem.Name = "openFileDialogMenuItem";
            openFileDialogMenuItem.Size = new Size(217, 26);
            openFileDialogMenuItem.Text = "Choose game map";
            // 
            // scoreBoard
            // 
            scoreBoard.Controls.Add(enemiesKilledLabel);
            scoreBoard.Controls.Add(timeElapsedLabel);
            scoreBoard.Controls.Add(label2);
            scoreBoard.Controls.Add(label1);
            scoreBoard.Location = new Point(619, 717);
            scoreBoard.Name = "scoreBoard";
            scoreBoard.Size = new Size(250, 124);
            scoreBoard.TabIndex = 2;
            scoreBoard.Visible = false;
           
            // 
            // enemiesKilledLabel
            // 
            enemiesKilledLabel.AutoSize = true;
            enemiesKilledLabel.Location = new Point(134, 49);
            enemiesKilledLabel.Name = "enemiesKilledLabel";
            enemiesKilledLabel.Size = new Size(17, 20);
            enemiesKilledLabel.TabIndex = 3;
            enemiesKilledLabel.Text = "0";
            // 
            // timeElapsedLabel
            // 
            timeElapsedLabel.AutoSize = true;
            timeElapsedLabel.Location = new Point(134, 28);
            timeElapsedLabel.Name = "timeElapsedLabel";
            timeElapsedLabel.Size = new Size(0, 20);
            timeElapsedLabel.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 49);
            label2.Name = "label2";
            label2.Size = new Size(111, 20);
            label2.TabIndex = 1;
            label2.Text = "Enemies killed: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 19);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Time: ";
            // 
            // gameContainer
            // 
            gameContainer.Dock = DockStyle.Fill;
            gameContainer.Location = new Point(0, 30);
            gameContainer.Name = "gameContainer";
            gameContainer.Size = new Size(882, 823);
            gameContainer.TabIndex = 3;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 853);
            Controls.Add(gameContainer);
            Controls.Add(scoreBoard);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "GameForm";
            Text = "Bomber";
            KeyPress += GameForm_KeyPress;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            scoreBoard.ResumeLayout(false);
            scoreBoard.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem openFileDialogMenuItem;
        private Panel scoreBoard;
        private Label label2;
        private Label label1;
        private Label enemiesKilledLabel;
        private Label timeElapsedLabel;
        private Panel gameContainer;
    }
}
