using Bomber.Model;
using Bomber.Persistence;
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
                    catch (System.IO.IOException ex)
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

            game.Map.MapChanged += OnMapChanged;
        }

        private void OnMapChanged(object? sender, Map.MapChangedEventArgs e)
        {
            BeginInvoke(() =>
            {
                container.SuspendLayout();
                foreach (var p in e.AffectedCells)
                {
                    cells[p.X, p.Y].ReplaceField(game.Map.Fields[p.X, p.Y]);
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

            int n = game.Map.Fields.GetLength(0);
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
                    var field = game.Map.Fields[i, j];
                    var cell = new Cell(field) { Size = new Size(cellSize, cellSize), Location = new Point(i * cellSize, j * cellSize) };
                    cells[i, j] = cell;
                    container.Controls.Add(cell);
                    if (field is Enemy enemy)
                    {
                        enemy.OrientationChanged += (sender, e) => OnEnemyOrientationChanged(enemy.Position);
                    }
                }
            }

            container.ResumeLayout();
        }

        private void OnEnemyOrientationChanged(Point p)
        {
            BeginInvoke(() =>
            {
                cells[p.X, p.Y].ReplaceField(game.Map.Fields[p.X, p.Y]);
            });
        }
    }
}
