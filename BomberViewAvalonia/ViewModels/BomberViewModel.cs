using Avalonia.Threading;
using Bomber.Model;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberViewAvalonia.ViewModels
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
        public RelayCommand<string> MoveCommand { get; private set; }

        public RelayCommand PlantBombCommand { get; private set; }

        public RelayCommand PauseToggleCommand { get; private set; }

        public RelayCommand LoadMapCommand { get; private set; }
        #endregion

        #region Constructor
        public BomberViewModel()
        {
            MoveCommand = new RelayCommand<string>(OnMove);
            PlantBombCommand = new RelayCommand(OnPlantBomb);
            PauseToggleCommand = new RelayCommand(OnPauseToggle);
            LoadMapCommand = new RelayCommand(
                () => OnLoadMap(),
                () => !GameStarted || model?.IsGameOver == true);
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
            LoadMapCommand.NotifyCanExecuteChanged();
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
            //TODO:Handle thread dispatch
            /*Application.Current.Dispatcher.BeginInvoke(() =>
            {
                LoadMapCommand.RaiseCanExecuteChanged();
            });*/
            if (!Dispatcher.UIThread.CheckAccess()) // hamisat ad vissza, ha nem a dispatcher thread-en vagyunk
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    LoadMapCommand.NotifyCanExecuteChanged();
                });
            }
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

        private void OnPlantBomb(
            )
        {
            model?.PlantBomb();
        }

        private void OnPauseToggle()
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