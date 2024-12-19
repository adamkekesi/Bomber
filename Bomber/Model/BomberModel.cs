using Bomber.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;

namespace Bomber.Model
{
    public class BomberModel : IDisposable
    {
        private const int enemyStepInterval = 1400;

        private const int bombExplodeTime = 2000;

        private const int bombRadius = 3;

        public event EventHandler? GameOver;

        public event EventHandler? StatUpdated;

        public event EventHandler? TimeElapsed;

        public bool Paused { get; private set; }

        public bool IsGameOver { get; private set; }

        public bool Won => enemies.Count == 0 && player.Alive;

        public int EnemiesKilled
        {
            get => enemiesKilled; private set
            {
                if (enemiesKilled == value)
                {
                    return;
                }
                enemiesKilled = value;
                StatUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public TimeSpan Time
        {
            get => time; private set
            {
                if (time == value)
                { return; }
                time = value;
                TimeElapsed?.Invoke(this, EventArgs.Empty);
            }
        }

        public IMap Map => map;

        public BombCollection Bombs => bombs;

        private readonly IMap map;

        private readonly Player player;

        private readonly List<Enemy> enemies;

        private readonly BombCollection bombs;

        private Random r;

        private System.Timers.Timer enemyStepScheduler;

        private System.Timers.Timer timer;

        private int enemiesKilled;

        private TimeSpan time = TimeSpan.Zero;

        public BomberModel(CellContent[,] cells)
        {
            r = new Random();
            enemyStepScheduler = new System.Timers.Timer(enemyStepInterval);
            enemyStepScheduler.Elapsed += OnEnemyStepSchedulerTick;
            enemyStepScheduler.Start();

            timer = new System.Timers.Timer(100);
            timer.Elapsed += OnTick;
            timer.Start();

            map = new Map(cells, r, out enemies);
            player = new Player(map, new Point(0, 0));
            player.Died += OnPlayerDied;

            map.PlacePlayer(player);

            bombs = new BombCollection(map);

            foreach (var enemy in enemies)
            {
                enemy.Died += OnEnemyDied;
            }
        }

        public BomberModel(Player player, IMap map, List<Enemy> enemies, BombCollection bombs)
        {
            r = new Random();
            enemyStepScheduler = new System.Timers.Timer(enemyStepInterval);
            enemyStepScheduler.Elapsed += OnEnemyStepSchedulerTick;
            enemyStepScheduler.Start();

            timer = new System.Timers.Timer(100);
            timer.Elapsed += OnTick;
            timer.Start();

            this.player = player;
            player.Died += OnPlayerDied;

            this.bombs = bombs;

            this.enemies = enemies;

            this.map = map;
            foreach (var enemy in enemies)
            {
                enemy.Died += OnEnemyDied;
            }
        }

        public void PlayerStep(Direction dir)
        {
            if (Paused)
            {
                return;
            }
            player.Move(dir);
        }

        public void PlantBomb()
        {
            if (Paused)
            {
                return;
            }
            Bomb bomb = player.PlantBomb(bombExplodeTime, bombRadius);
            bombs.PlantBomb(bomb);
        }

        public void PauseToggle()
        {
            if (IsGameOver)
            {
                return;
            }
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        private void Pause()
        {
            Paused = true;
            enemyStepScheduler.Stop();
            timer.Stop();
            bombs.Pause();
        }

        private void Resume()
        {
            Paused = false;
            enemyStepScheduler.Start();
            timer.Start();
            bombs.Resume();
        }

        private void OnEnemyStepSchedulerTick(object? sender, ElapsedEventArgs e)
        {
            foreach (Enemy? enemy in enemies)
            {
                enemy.Move();
            }
        }

        private void OnEnemyDied(object? sender, EventArgs e)
        {
            if (sender is Enemy enemy)
            {
                EnemiesKilled++;
                enemies.Remove(enemy);
                if (enemies.Count == 0)
                {
                    OnGameOver();
                }
            }

        }

        private void OnPlayerDied(object? sender, EventArgs e)
        {
            OnGameOver();
        }

        private void OnGameOver()
        {
            Pause();
            IsGameOver = true;
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        private void OnTick(object? sender, ElapsedEventArgs e)
        {
            Time += TimeSpan.FromMilliseconds(100);
        }

        public void Dispose()
        {
            map.Dispose();
            player.Dispose();
            foreach (var enemy in enemies)
            {
                enemy.Dispose();
            }
            bombs.Dispose();

            GameOver = null;
            StatUpdated = null;
            TimeElapsed = null;
        }
    }
}
