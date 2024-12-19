using Bomber.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BomberViewWpf.ViewModel
{
    public class BomberViewModel : ViewModelBase
    {
        #region Fields
        private int enemiesKilled;

        private TimeSpan time;

        private BomberModel? model;

        #endregion

        #region Properties
        public int EnemiesKilled
        {
            get { return enemiesKilled; }
            set
            {
                enemiesKilled = value;
                OnPropertyChanged();
            }
        }


        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<CellViewModel> Cells { get; set; }

        public bool GameStarted => model != null;

        public int MapSize => model?.Map.Size ?? 0;
        #endregion

        #region Events
        public event EventHandler? LoadMap;
        #endregion

        #region Commands
        public DelegateCommand MoveCommand { get; private set; }

        public DelegateCommand PlantBombCommand { get; private set; }

        public DelegateCommand PauseToggleCommand { get; private set; }

        public DelegateCommand LoadMapCommand { get; private set; }
        #endregion

        #region Constructor
        public BomberViewModel()
        {
            MoveCommand = new DelegateCommand(OnMove);
            PlantBombCommand = new DelegateCommand(OnPlantBomb);
            PauseToggleCommand = new DelegateCommand(OnPauseToggle);
            LoadMapCommand = new DelegateCommand(
                (_) => !GameStarted || model?.IsGameOver == true,
                (_) => OnLoadMap());
            Cells = new ObservableCollection<CellViewModel>();

        }
        #endregion

        #region Public methods
        public void StartGame(BomberModel model)
        {
            this.model = model;

            Cells.Clear();
            for (int y = 0; y < model.Map.Size; y++)
            {
                for (int x = 0; x < model.Map.Size; x++)
                {
                    Cells.Add(new CellViewModel
                    {
                        X = x,
                        Y = y,
                        Field = model.Map[x, y],
                    });
                }
            }

            model.TimeElapsed += OnTimeElapsed;
            model.StatUpdated += OnStatUpdated;
            model.GameOver += OnGameOver;
            model.Map.MapChanged += OnMapChanged;
            model.Bombs.BombsChanged += OnBombsChanged;
            OnPropertyChanged(nameof(GameStarted));
            OnPropertyChanged(nameof(MapSize));
            LoadMapCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Event handlers
        private void OnTimeElapsed(object? sender, EventArgs e)
        {
            if (model == null)
            {
                return;
            }
            Time = model.Time;
        }

        private void OnStatUpdated(object? sender, EventArgs e)
        {
            if (model == null)
            {
                return;
            }
            EnemiesKilled = model.EnemiesKilled;
        }

        private void OnMapChanged(object? sender, Map.MapChangedEventArgs e)
        {
            if (model == null)
            {
                return;
            }
            foreach (var cell in e.AffectedCells)
            {
                CellViewModel cellVm = Cells.Single(c => c.X == cell.X && c.Y == cell.Y);
                cellVm.Field = model.Map[cell.X, cell.Y];
            }

        }

        private void OnGameOver(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadMapCommand.RaiseCanExecuteChanged();
            });
        }

        private void OnBombsChanged(object? sender, BombCollection.BombsChangedEventArgs e)
        {
            if (model == null)
            {
                return;
            }
            CellViewModel cellVm = Cells.Single(c => c.X == e.Bomb.Position.X && c.Y == e.Bomb.Position.Y);
            cellVm.BombPlaced = e.Change == BombCollection.ChangeType.Added;

        }

        private void OnMove(object? param)
        {
            if (param is not String dir)
            {
                return;
            }
            model?.PlayerStep((Direction)int.Parse(dir));
        }

        private void OnPlantBomb(object? param)
        {
            model?.PlantBomb();
        }

        private void OnPauseToggle(object? param)
        {
            model?.PauseToggle();
        }
        #endregion

        #region Event methods
        private void OnLoadMap()
        {
            LoadMap?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
