using DTF.ui;
using UnityEngine;

namespace DTF.Scenes
{
    public class MenuScene : MonoBehaviour
    {

        private ScenesManager _scenesManager;
        private UIController _uIController;

        private static Canvas _canvas;
        public static Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                    _canvas = GameObject.Find("CanvasUI").GetComponent<Canvas>();

                return _canvas;
            }
        }

        void Start()
        {
            _scenesManager = GameObject.FindObjectOfType<ScenesManager>();

            var param = ScenesManager.GetSceneParams(SceneId.Menu) as MenuSceneParams;

            _uIController = new UIController(Canvas);
            _uIController.MenuView().Show();
            _uIController.MenuView().onStart = OnStart;
            _uIController.MenuView().onExit  = OnExit;
        }

        private void OnStart()
        {
            _uIController.MenuView().Close();
            _scenesManager.LoadScene(SceneId.Main, new MainSceneParams(2, 2, 0, false));
        }

        private void OnExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}