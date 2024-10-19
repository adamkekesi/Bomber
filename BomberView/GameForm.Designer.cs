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
            startPauseButton = new Button();
            enemiesKilledLabel = new Label();
            timeElapsedLabel = new Label();
            label2 = new Label();
            label1 = new Label();
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
            menuStrip.Padding = new Padding(5, 2, 0, 2);
            menuStrip.Size = new Size(772, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { openFileDialogMenuItem });
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(37, 20);
            fileMenu.Text = "File";
            // 
            // openFileDialogMenuItem
            // 
            openFileDialogMenuItem.Name = "openFileDialogMenuItem";
            openFileDialogMenuItem.Size = new Size(174, 22);
            openFileDialogMenuItem.Text = "Choose game map";
            // 
            // scoreBoard
            // 
            scoreBoard.Controls.Add(startPauseButton);
            scoreBoard.Controls.Add(enemiesKilledLabel);
            scoreBoard.Controls.Add(timeElapsedLabel);
            scoreBoard.Controls.Add(label2);
            scoreBoard.Controls.Add(label1);
            scoreBoard.Location = new Point(542, 538);
            scoreBoard.Margin = new Padding(3, 2, 3, 2);
            scoreBoard.Name = "scoreBoard";
            scoreBoard.Size = new Size(219, 93);
            scoreBoard.TabIndex = 2;
            scoreBoard.Visible = false;
            // 
            // startPauseButton
            // 
            startPauseButton.Location = new Point(16, 59);
            startPauseButton.Name = "startPauseButton";
            startPauseButton.Size = new Size(75, 23);
            startPauseButton.TabIndex = 4;
            startPauseButton.Text = "Pause";
            startPauseButton.UseVisualStyleBackColor = true;
            startPauseButton.Click += startPauseButton_Click;
            // 
            // enemiesKilledLabel
            // 
            enemiesKilledLabel.AutoSize = true;
            enemiesKilledLabel.Location = new Point(117, 37);
            enemiesKilledLabel.Name = "enemiesKilledLabel";
            enemiesKilledLabel.Size = new Size(13, 15);
            enemiesKilledLabel.TabIndex = 3;
            enemiesKilledLabel.Text = "0";
            // 
            // timeElapsedLabel
            // 
            timeElapsedLabel.AutoSize = true;
            timeElapsedLabel.Location = new Point(117, 21);
            timeElapsedLabel.Name = "timeElapsedLabel";
            timeElapsedLabel.Size = new Size(0, 15);
            timeElapsedLabel.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 37);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 1;
            label2.Text = "Enemies killed: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 14);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Time: ";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 640);
            Controls.Add(scoreBoard);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip;
            Margin = new Padding(3, 2, 3, 2);
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
        private Button startPauseButton;
    }
}
