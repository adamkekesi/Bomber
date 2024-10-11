using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Persistence
{
    public interface IMapLoader
    {
        public Field[,] Load();
    }
}
