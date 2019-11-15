using DTF.ui;
using UnityEngine;

namespace DTF.Scenes
{
    public class MainScene : MonoBehaviour
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

            var param = ScenesManager.GetSceneParams(SceneId.Main) as MainSceneParams;

            _uIController = new UIController(Canvas);
            _uIController.TopPanelView().Show();
            _uIController.TopPanelView().onGoMenu = GoMenu;
        }

        private void GoMenu()
        {
            _uIController.TopPanelView().Close();
           _scenesManager.LoadScene(SceneId.Menu, new MenuSceneParams(SceneId.Main));
        }
    }
}