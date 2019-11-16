using System;
using UnityEngine;

namespace DTF.ui
{
    public class UIController
    {
        private Canvas _canvas;

        private MenuView _menulView;
        private TopPanelView _topPanelView;
        private NextTurnPanelView _nextTurnPanelView;
        private LosePanelView _losePanelView;
        private WinPanelView _winPanelView;
        private PackPanelView _packPanelView;

        public PackPanelView PackPanelView()
        {
            if (_packPanelView == null)
                _packPanelView = InstantiateView<PackPanelView>("PackPanelView");

            return _packPanelView;
        }

        public WinPanelView WinPanelView()
        {
            if (_winPanelView == null)
                _winPanelView = InstantiateView<WinPanelView>("WinPanelView");

            return _winPanelView;
        }

        public LosePanelView LosePanelView()
        {
            if (_losePanelView == null)
                _losePanelView = InstantiateView<LosePanelView>("LosePanelView");

            return _losePanelView;
        }

        public NextTurnPanelView NextTurnPanelView()
        {
            if (_nextTurnPanelView == null)
                _nextTurnPanelView = InstantiateView<NextTurnPanelView>("NextTurnPanelView");

            return _nextTurnPanelView;
        }

        public TopPanelView TopPanelView()
        {
            if (_topPanelView == null)
                _topPanelView = InstantiateView<TopPanelView>("TopPanelView");

            return _topPanelView;
        }

        public MenuView MenuView()
        {
            if (_menulView == null)
                _menulView = InstantiateView<MenuView>("MenuView");

            return _menulView;
        }

        public UIController(Canvas canvas)
        {
            _canvas = canvas;
        }

        private T InstantiateView<T>(string resourcePath, bool useExist = true) where T : UnityEngine.Object, IView
        {
            T view = null;

            if (useExist)
                view = GameObject.FindObjectOfType<T>();

            if (view == null)
            {
                GameObject prefab = Resources.Load<GameObject>("ui/" + resourcePath);
                if (prefab == null)
                    throw new Exception(string.Format($"Не найден префаб ui/{resourcePath} для {typeof(T)}"));

                GameObject go = GameObject.Instantiate(prefab);
                go.transform.SetParent(_canvas.transform, false);
                view = go.GetComponent<T>();
            }
            view.Hide();
            return view;
        }
    }
}