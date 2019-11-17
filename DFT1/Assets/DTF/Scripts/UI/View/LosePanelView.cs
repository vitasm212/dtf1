﻿using System;
using UnityEngine;

namespace DTF.ui
{
    public class LosePanelView : MonoBehaviour, IView
    {
        public Action<int> onGoMenu;

        public void OnGoMenu()
        {
            onGoMenu?.Invoke(-1);
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