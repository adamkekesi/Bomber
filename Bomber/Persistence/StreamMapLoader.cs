using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Persistence
{
    public class StreamMapLoader : IMapLoader
    {
        private readonly Stream stream;

        public StreamMapLoader(Stream stream)
        {
            this.stream = stream;
        }

        public async Task<CellContent[,]> LoadAsync()
        {
            using StreamReader reader = new StreamReader(stream);
            List<string> lines = new List<string>();
            while (!reader.EndOfStream)
            {
                string? line = await reader.ReadLineAsync();
                if (line != null)
                {
                    lines.Add(line);
                }
            }

            int mapSize = lines.Count;
            CellContent[,] matrix = new CellContent[mapSize, mapSize];

            for (int i = 0; i < lines.Count; i++)
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
