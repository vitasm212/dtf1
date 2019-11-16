using UnityEngine;

namespace DTF
{
    public struct Pos
    {
        public int x;
        public int y;

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool Near(Pos pos1, Pos pos2)
        {
            return pos1 + Pos.Left == pos2 || pos1 + Pos.Rigth == pos2 || pos1 + Pos.Up == pos2 || pos1 + Pos.Down == pos2;
        }

        public Vector3 ToWorld()
        {
            return new Vector3(x * Settings.CellSizeX, 0, y * Settings.CellSizeY);
        }

        public static bool operator !=(Pos a, Pos b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static bool operator ==(Pos a, Pos b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static Pos operator -(Pos a, Pos b)
        {
            return new Pos(a.x - b.x, a.y - b.y);
        }

        public static Pos operator +(Pos a, Pos b)
        {
            return new Pos(a.x + b.x, a.y + b.y);
        }

        public static Pos Left { get { return new Pos(-1, 0); } }
        public static Pos Rigth { get { return new Pos(1, 0); } }
        public static Pos Down { get { return new Pos(0, -1); } }
        public static Pos Up { get { return new Pos(0, 1); } }
        public static Pos LeftUp { get { return new Pos(-1, 1); } }
        public static Pos RigthUp { get { return new Pos(1, 1); } }
        public static Pos LeftDown { get { return new Pos(-1, -1); } }
        public static Pos RigthDown { get { return new Pos(1, -1); } }

        public static Pos Zero { get { return new Pos(0, 0); } }
        public static Pos Outside { get { return new Pos(int.MinValue, int.MinValue); } }

        public static Pos[] Cross { get { return new Pos[] { Left, Up, Rigth, Down }; } }
        public static Pos[] Circle { get { return new Pos[] { Left, LeftUp, Up, RigthUp, Rigth, RigthDown, Down, LeftDown }; } }


        public override string ToString()
        {
            return $"Pos({x}, {y})";
        }
    }
}