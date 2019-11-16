

using System;

namespace DTF
{
    public class Map
    {
        private Cell[,] _map;

        public static int SizeX { get; private set; } = -1;
        public static int SizeY { get; private set; } = -1;

        public Map(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            _map = new Cell[sizeX, sizeX];
        }

        public Cell this[Pos pos]
        {
            get
            {
                return this[pos.x, pos.y];
            }
            set
            {
                this[pos.x, pos.y] = value;
            }
        }

        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= SizeX)
                    return null;
                if (y < 0 || y >= SizeY)
                    return null;

                return _map[x, y];
            }
            set
            {
                if (x < 0 || x >= SizeX)
                    throw new Exception($"Out index X");
                if (y < 0 || y >= SizeY)
                    throw new Exception($"Out index Y");

                _map[x, y] = value;
            }
        }
    }
}