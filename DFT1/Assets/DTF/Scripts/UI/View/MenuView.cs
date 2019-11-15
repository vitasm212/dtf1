using System;
using UnityEngine;

namespace DTF.ui
{
    public class MenuView : MonoBehaviour, IView
    {
        public Action onStart;
        public Action onExit;

        public void OnStart()
        {
            onStart?.Invoke();
        }

        public void OnExit()
        {
            onExit?.Invoke();
        }

        public void Close()
        {
            Hide();
            Destroy(gameObject);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}