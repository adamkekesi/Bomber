using Bomber.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;

namespace Bomber.Model
{
    public class Game : IDisposable
    {
        private const int enemyStepInterval = 800;

        private const int bombExplodeTime = 2000;

        private const int bombRadius = 3;

        public event EventHandler? GameOver;

        public event EventHandler? StatUpdated;

        public event EventHandler? TimeElapsed;

        public event EventHandler<Map.MapChangedEventArgs>? BombAdded;

        public bool Paused { get; private set; }

        public bool IsGameOver { get; private set; }

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

        public Map Map => map;

        private readonly Map map;

        private readonly Player player;

        private readonly List<Enemy> enemies;

        private readonly List<Bomb> activeBombs;

        private Random r;

        private System.Timers.Timer enemyStepScheduler;

        private System.Timers.Timer timer;

        private int enemiesKilled;

        private TimeSpan time = TimeSpan.Zero;

        public Game(IMapLoader mapLoader)
        {
            r = new Random();
            enemyStepScheduler = new System.Timers.Timer(enemyStepInterval);
            enemyStepScheduler.Elapsed += OnEnemyStepSchedulerTick;
            enemyStepScheduler.Start();

            timer = new System.Timers.Timer(100);
            timer.Elapsed += OnTick;
            timer.Start();

            player = new Player(new Point(0, 0));
            player.Died += OnPlayerDied;
            player.Moved += OnUnitMoved;
            player.BombPlanted += OnBombPlanted;

            activeBombs = new List<Bomb>();

            map = new Map(mapLoader.Load(), player, r, out enemies);
            foreach (var enemy in enemies)
            {
                enemy.Moved += OnUnitMoved;
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
            player.PlantBomb();
        }

        public void Pause()
        {
            Paused = true;
            enemyStepScheduler.Stop();
            timer.Stop();
            foreach (var bomb in activeBombs)
            {
                bomb.Pause();
            }
        }

        public void Resume()
        {
            if (IsGameOver) 
            {
                return;
            }
            Paused = false;
            enemyStepScheduler.Start();
            timer.Start();
            foreach (var bomb in activeBombs)
            {
                bomb.Resume();
            }
        }

        private void OnBombPlanted(object? sender, EventArgs e)
        {
            if (sender is Unit unit)
            {
                Bomb bomb = new Bomb(unit.Position, bombExplodeTime);
                activeBombs.Add(bomb);
                BombsChanged?.Invoke(this, new Map.MapChangedEventArgs(bomb.Position));
                bomb.Exploded += OnBombExploded;
            }
        }

        private void OnBombExploded(object? sender, EventArgs e)
        {
            if (sender is Bomb bomb)
            {
                map.ApplyBlast(bomb.Position, bombRadius);
                activeBombs.Remove(bomb);
                bomb.Dispose();
            }
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
                map.RemoveField(enemy.Position);
                enemy.Dispose();
                EnemiesKilled++;
                enemies.Remove(enemy);
                if (enemies.Count == 0)
                {
                    GameOver?.Invoke(this, EventArgs.Empty);
                }
            }

        }

        private void OnUnitMoved(object? sender, Unit.MovedEventArgs e)
        {
            map.Move(e.CurrentPos, e.Direction);
        }

        private void OnPlayerDied(object? sender, EventArgs e)
        {
            map.RemoveField(player.Position);
            player.Dispose();
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
            foreach (var bomb in activeBombs)
            {
                bomb.Dispose();
            }

            GameOver = null;
            StatUpdated = null;
            TimeElapsed = null;
            BombsChanged = null;
        }
    }
}
