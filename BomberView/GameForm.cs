using Bomber.Model;
using Bomber.Persistence;
using System.Diagnostics;
using System.Windows.Forms;

namespace BomberView
{
    public partial class GameForm : Form
    {
        private BomberModel? game;

        private const int cellSize = 30;

        private Cell[,]? cells;

        public GameForm()
        {
            InitializeComponent();
            openFileDialogMenuItem.Click += OnOpenFileDialogClicked;
        }

        private async void OnOpenFileDialogClicked(object? sender, EventArgs e)
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
                        game = new BomberModel(await new MapLoader(openFileDialog.FileName).LoadAsync());
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
            if (game == null)
            {
                return;
            }
            InitMap();
            scoreBoard.Visible = true;
            game.TimeElapsed += OnTimeElapsed;
            game.StatUpdated += OnStatUpdated;
            game.Bombs.BombsChanged += OnBombsChanged;
            game.Map.MapChanged += OnMapChanged;
            game.GameOver += OnGameOver;
            scoreBoard.BringToFront();
        }

        private void OnGameOver(object? sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }
            BeginInvoke(() =>
            {
                MessageBox.Show(game.Won ? "You won!" : "Game over!",
                       "End", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            game.Dispose();
        }

        private void OnBombsChanged(object? sender, BombCollection.BombsChangedEventArgs e)
        {
            if (game == null || cells == null)
            {
                return;
            }
            BeginInvoke(() =>
            {
                if (e.Change == BombCollection.ChangeType.Added)
                {
                    cells[e.Bomb.Position.X, e.Bomb.Position.Y].ShowBomb();
                }
                else
                {
                    cells[e.Bomb.Position.X, e.Bomb.Position.Y].HideBomb();
                }
            });
        }

        private void OnStatUpdated(object? sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }
            BeginInvoke(() =>
            {
                enemiesKilledLabel.Text = game.EnemiesKilled.ToString();
            });
        }

        private void OnTimeElapsed(object? sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }
            BeginInvoke(() =>
            {
                timeElapsedLabel.Text = game.Time.ToString(@"hh\:mm\:ss");
            });
        }

        private void OnMapChanged(object? sender, Map.MapChangedEventArgs e)
        {
            if (game == null || cells == null)
            {
                return;
            }
            BeginInvoke(() =>
            {
                gameContainer.SuspendLayout();
                foreach (var p in e.AffectedCells)
                {
                    cells[p.X, p.Y].ReplaceField(game.Map[p]);
                }
                gameContainer.ResumeLayout();
            });
        }

        private void InitMap()
        {
            if (game == null)
            {
                return;
            }

            gameContainer.Controls.Clear();

            int n = game.Map.Size;

            cells = new Cell[n, n];

            gameContainer.SuspendLayout();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var field = game.Map[i, j];
                    var cell = new Cell(field) { Size = new Size(cellSize, cellSize), Location = new Point(i * cellSize, j * cellSize) };
                    cells[i, j] = cell;
                    gameContainer.Controls.Add(cell);
                }
            }

            gameContainer.ResumeLayout();
        }

        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (game == null)
            {
                return;
            }
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

                case 'c':
                    game.PauseToggle();
                    break;

                default:
                    break;
            }
        }
    }
}
