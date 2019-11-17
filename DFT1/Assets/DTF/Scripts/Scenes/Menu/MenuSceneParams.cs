using System.Collections.Generic;

namespace DTF.Scenes
{
    public class MenuSceneParams : ISceneParams
    {
        public readonly SceneId SceneId;
        public readonly int Round;
        public readonly List<int> addCard;

        public MenuSceneParams(SceneId sceneId, int round, List<int> card)
        {
            SceneId = sceneId;
            Round = round;
            addCard = card;
        }
    }
}