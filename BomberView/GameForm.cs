using Bomber.Model;
using Bomber.Persistence;
using System.Diagnostics;
using System.Windows.Forms;

namespace BomberView
{
    public partial class GameForm : Form
    {
        private Game? game;

        private const int cellSize = 30;

        private Panel? container;

        private Cell[,]? cells;

        public GameForm()
        {
            InitializeComponent();
            openFileDialogMenuItem.Click += OnOpenFileDialogClicked;
        }

        private void OnOpenFileDialogClicked(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        game = new Game(new MapLoader(openFileDialog.FileName));
                        StartGame();

                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("File reading is unsuccessful!\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void StartGame()
        {
            InitMap();
            scoreBoard.Visible = true;
            game.TimeElapsed += OnTimeElapsed;
            game.StatUpdated += OnStatUpdated;
            game.BombsChanged
            game.Map.MapChanged += OnMapChanged;
        }

        private void OnStatUpdated(object? sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                enemiesKilledLabel.Text = game.EnemiesKilled.ToString();
            });
        }

        private void OnTimeElapsed(object? sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                timeElapsedLabel.Text = game.Time.ToString(@"hh\:mm\:ss");
            });
        }

        private void OnMapChanged(object? sender, Map.MapChangedEventArgs e)
        {
            BeginInvoke(() =>
            {
                container.SuspendLayout();
                foreach (var p in e.AffectedCells)
                {
                    cells[p.X, p.Y].ReplaceField(game.Map[p]);
                }
                container.ResumeLayout();
            });
        }

        private void InitMap()
        {
            if (game == null)
            {
                return;
            }

            int n = game.Map.Size;
            int size = n * cellSize;
            container = new Panel()
            {
                Size = new Size(size, size),
                Location = new Point(20, 60)
            };
            Controls.Add(container);

            cells = new Cell[n, n];

            container.SuspendLayout();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var field = game.Map[i, j];
                    var cell = new Cell(field) { Size = new Size(cellSize, cellSize), Location = new Point(i * cellSize, j * cellSize) };
                    cells[i, j] = cell;
                    container.Controls.Add(cell);
                }
            }

            container.ResumeLayout();
        }

        private void startPauseButton_Click(object sender, EventArgs e)
        {
            if (game.Paused)
            {
                game.Resume();
                startPauseButton.Text = "Pause";
            }
            else
            {
                game.Pause();
                startPauseButton.Text = "Resume";
            }
        }

        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    game.PlayerStep(Direction.Up);
                    break;

                case 'a':
                    game.PlayerStep(Direction.Left);

                    break;

                case 's':
                    game.PlayerStep(Direction.Down);

                    break;

                case 'd':
                    game.PlayerStep(Direction.Right);

                    break;

                case ' ':
                    game.PlantBomb();
                    break;

                default:
                    break;
            }
        }
    }
}
