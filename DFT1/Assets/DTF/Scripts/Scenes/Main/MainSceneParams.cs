namespace DTF.Scenes
{
    public class MainSceneParams : ISceneParams
    {
        public readonly int SizeX;
        public readonly int SizeY;
        public readonly int Round;
        public readonly bool SlowTime;

        public MainSceneParams(int sizeX, int sizeY, int round, bool slowTime)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Round = round;
            SlowTime = slowTime;
        }
    }
}