using System.Collections.Generic;

namespace DTF.Scenes
{
    public class MainSceneParams : ISceneParams
    {
        public readonly int SizeX;
        public readonly List<int> addCard;
        public readonly int Round;
        public readonly bool SlowTime;

        public MainSceneParams(int sizeX, List<int> card, int round, bool slowTime)
        {
            SizeX = sizeX;
            addCard = card;
            Round = round;
            SlowTime = slowTime;
        }
    }
}