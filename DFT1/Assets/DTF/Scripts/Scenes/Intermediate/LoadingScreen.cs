using System.Collections;
using UnityEngine;

namespace DTF.Scenes
{
    public class LoadingScreen : MonoBehaviour
    {
        private int _countTask;

        private void Start()
        {
            AudioListener.volume = PlayerPrefs.GetInt("sound_on", 1);

            _countTask = 0;

            StartCoroutine(CoInitialize());
        }

        private IEnumerator CoInitialize()
        {
            while (_countTask > 0)
            {
                yield return new WaitForEndOfFrame();
            }

            var loadingSceneParams = (LoadingSceneParams)ScenesManager.GetSceneParams(SceneId.Loading);

            loadingSceneParams.LoadSceneWithoutIntermediate();
        }
    }
}