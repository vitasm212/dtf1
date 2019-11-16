using System;
using UnityEngine;

namespace DTF.ui
{
    public class WinPanelView : MonoBehaviour, IView
    {
        public Action onGoMenu;

        public void OnGoMenu()
        {
            onGoMenu?.Invoke();
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