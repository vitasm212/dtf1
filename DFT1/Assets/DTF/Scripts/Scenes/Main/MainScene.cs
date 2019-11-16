using DTF.ui;
using UnityEngine;

namespace DTF.Scenes
{
    public class MainScene : MonoBehaviour
    {
        private static Canvas _canvas;
        public static Canvas Canvas { get { if (_canvas == null) _canvas = GameObject.Find("CanvasUI").GetComponent<Canvas>(); return _canvas; } }

        private ScenesManager _scenesManager;
        private UIController _uIController;

        private Map _map;
        private Unit[] _units;

        void Start()
        {
            _scenesManager = GameObject.FindObjectOfType<ScenesManager>();

            var param = ScenesManager.GetSceneParams(SceneId.Main) as MainSceneParams;

            _uIController = new UIController(Canvas);
            _uIController.TopPanelView().Show();
            _uIController.TopPanelView().onGoMenu = GoMenu;

            _units = new Unit[10];
            for (int i = 0; i < _units.Length; i++)
            {
                _units[i] = new Unit(UnitType.Type1, new Pos(1 + i, 1));
            }

            _map = new Map(Settings.MapSize, Settings.MapSize);
            for (int x = 0; x < Map.SizeX; x++)
                for (int y = 0; y < Map.SizeY; y++)
                {
                    _map[x, y] = new Cell();
                }
        }

        private void GoMenu()
        {
            _uIController.TopPanelView().Close();
            _scenesManager.LoadScene(SceneId.Menu, new MenuSceneParams(SceneId.Main));
        }

        private void Update()
        {
            for (int i = 0; i < _units.Length; i++)
            {
                Unit unit = _units[i];

                unit.Update();
            }
        }
    }
}