using System;

namespace DTF
{
    public class Map
    {
        private Cell[] _map;

        public static int Size { get; private set; } = -1;

        public Map(int size)
        {
            Size = size;
            _map = new Cell[size];
        }

        public Cell this[int x]
        {
            get
            {
                if (x < 0 || x >= Size)
                    return null;

                return _map[x];
            }
            set
            {
                if (x < 0 || x >= Size)
                    throw new Exception($"Out index X");

                _map[x] = value;
            }
        }
    }
}