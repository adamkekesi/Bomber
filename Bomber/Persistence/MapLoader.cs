using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Persistence
{
    public class MapLoader : IMapLoader
    {
        private readonly string _path;

        public MapLoader(string path)
        {
            _path = path;
        }

        public async Task<CellContent[,]> LoadAsync()
        {
            string[] lines = await File.ReadAllLinesAsync(_path);
            int mapSize = lines.Length;
            CellContent[,] matrix = new CellContent[mapSize, mapSize];

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Length != mapSize)
                {
                    throw new IncorrectMapFileException("The map file needs to contain an NxN map");
                }
                for (int j = 0; j < line.Length; j++)
                {
                    char cell = line[j];
                    try
                    {
                        matrix[i, j] = (CellContent)int.Parse(cell.ToString());
                    }
                    catch (Exception)
                    {
                        throw new IncorrectMapFileException("The map's cells should be one of: 0 - Empty, 1 - Enemy, 2 - Wall");
                    }
                }
            }

            return matrix;
        }
    }
}
