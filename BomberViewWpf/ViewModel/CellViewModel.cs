using Bomber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberViewWpf.ViewModel
{
    public class CellViewModel : ViewModelBase
    {
        #region Fields
        private bool bombPlaced;

        #endregion

        #region Properties
        public bool BombPlaced
        {
            get { return bombPlaced; }
            set
            {
                if (bombPlaced == value)
                {
                    return;
                }
                bombPlaced = value;
                OnPropertyChanged();
            }
        }

        private IField? field;

        public IField? Field
        {
            get { return field; }
            set
            {
                if (field == value)
                {
                    return;
                }

                bool playerVisible = PlayerVisible;
                bool enemyVisible = EnemyVisible;
                bool wallVisible = WallVisible;

                field = value;
                OnPropertyChanged();
                if (playerVisible != PlayerVisible)
                {
                    OnPropertyChanged(nameof(PlayerVisible));
                }
                if (enemyVisible != EnemyVisible)
                {
                    OnPropertyChanged(nameof(EnemyVisible));
                }
                if (wallVisible != WallVisible)
                {
                    OnPropertyChanged(nameof(WallVisible));
                }
            }
        }
        public bool PlayerVisible => field != null && field is Player;

        public bool EnemyVisible => field != null && field is Enemy;

        public bool WallVisible => field != null && field is Wall;

        public int X { get; set; }

        public int Y { get; set; }

        public Tuple<int, int> XY
        {
            get { return new(X, Y); }
        }

        #endregion


    }
}
