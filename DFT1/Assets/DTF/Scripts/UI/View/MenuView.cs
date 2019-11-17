using System;
using UnityEngine;
using UnityEngine.UI;

namespace DTF.ui
{
    public class MenuView : MonoBehaviour, IView
    {
        public Action onStart;
        public Action onExit;

        [SerializeField] Text _text;

        public void OnStart()
        {
            onStart?.Invoke();
        }

        public void Setup(int round)
        {
            _text.text = $"ROUND {round}";
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