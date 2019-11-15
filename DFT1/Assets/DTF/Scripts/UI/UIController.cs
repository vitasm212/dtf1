using System;
using UnityEngine;

namespace DTF.ui
{
    public class UIController
    {
        private Canvas _canvas;

        private MenuView _menulView;
        private TopPanelView _topPanelView;

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