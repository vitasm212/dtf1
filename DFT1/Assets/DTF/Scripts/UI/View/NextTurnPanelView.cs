using System;
using UnityEngine;
using UnityEngine.UI;

namespace DTF.ui
{
    public class NextTurnPanelView : MonoBehaviour, IView
    {
        public Action onNextTurn;

        [SerializeField] private Button _button;

        public void SetInteractable(bool value)
        {
            _button.interactable = value;
        }

        public void OnNextTurn()
        {
            onNextTurn?.Invoke();
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