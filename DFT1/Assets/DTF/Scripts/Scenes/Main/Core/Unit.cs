namespace DTF
{
    public class Unit
    {
        public UnitType type;
        public Pos pos;

        public Unit(UnitType unitType, Pos unitPos)
        {
            type = unitType;
            pos = unitPos;
        }

        public void Update()
        {

        }
    }
}