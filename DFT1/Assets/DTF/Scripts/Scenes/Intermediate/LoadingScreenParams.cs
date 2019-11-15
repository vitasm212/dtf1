using System;

namespace DTF.Scenes
{
    public class LoadingSceneParams : ISceneParams
    {
        public readonly SceneId SceneId;
        public readonly Action LoadSceneWithoutIntermediate;

        public LoadingSceneParams(SceneId sceneId, Action loadSceneWithoutIntermediate)
        {
            SceneId = sceneId;
            LoadSceneWithoutIntermediate = loadSceneWithoutIntermediate;
        }
    }
}