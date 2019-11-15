namespace DTF.Scenes
{
    public class MenuSceneParams : ISceneParams
    {
        public readonly SceneId SceneId;

        public MenuSceneParams(SceneId sceneId)
        {
            SceneId = sceneId;
        }
    }
}