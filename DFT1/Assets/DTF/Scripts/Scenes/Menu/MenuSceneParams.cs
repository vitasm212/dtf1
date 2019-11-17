namespace DTF.Scenes
{
    public class MenuSceneParams : ISceneParams
    {
        public readonly SceneId SceneId;
        public readonly int Round;

        public MenuSceneParams(SceneId sceneId, int round)
        {
            SceneId = sceneId;
            Round = round;
        }
    }
}