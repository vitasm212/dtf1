using DTF.Scenes.Loading;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DTF.Scenes
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField] private LoadingPanel _loadingPanel;

        private static readonly Dictionary<SceneId, ISceneParams> _scenesParams = new Dictionary<SceneId, ISceneParams>();

        public SceneId Current;
        public Action<SceneId> _sceneHasBeenLoaded;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            StartSettings();

            LoadScene(SceneId.Menu, new MenuSceneParams(Current));
        }

        private void StartSettings()
        {

        }

        public void LoadScene(SceneId sceneId, ISceneParams @params = null)
        {
            Current = sceneId;
            _scenesParams[SceneId.Loading] = new LoadingSceneParams(sceneId, () => LoadSceneWithoutIntermediate(sceneId, @params));
            if (_loadingPanel != null)
                _loadingPanel.Show(true, () => SceneManager.LoadSceneAsync(GetSceneName(SceneId.Loading), LoadSceneMode.Single));
        }

        private void LoadSceneWithoutIntermediate(SceneId sceneId, ISceneParams @params = null)
        {
            _scenesParams[sceneId] = @params;
            if (_loadingPanel != null)
                _loadingPanel.Show(true);

            SceneManager.LoadSceneAsync(GetSceneName(sceneId), LoadSceneMode.Single);
        }

        public static ISceneParams GetSceneParams(SceneId sceneId)
        {
            if (!_scenesParams.ContainsKey(sceneId))
                return null;
            //throw new Exception($"scenes manager doesnt contain params for {sceneId} scene");

            return _scenesParams[sceneId];
        }

        private string GetSceneName(SceneId sceneId)
        {
            return sceneId.ToString().ToLower();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            Resources.UnloadUnusedAssets();
            _sceneHasBeenLoaded?.Invoke(Current);
            if (_loadingPanel != null)
                _loadingPanel.Show(scene.name == SceneId.Loading.ToString());
        }
    }
}