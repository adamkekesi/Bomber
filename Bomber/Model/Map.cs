using Bomber.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class Map
    {
        private readonly IField?[,] fields;

        public Map(CellContent[,] cells)
        {
            fields = new IField[cells.Length, cells.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells.Length; j++)
                {
                    switch (cells[i,j])
                    {
                        case CellContent.Enemy:
                            fields[i, j] = new Enemy();

                            break;
                        case CellContent.Wall:
                            fields[i, j] = new Wall();

                            break;
                        case CellContent.Empty:
                            fields[i, j] = null;

                            break;
                        default:
                            break;
                    }
                }
            }
            
        }
    }
}
