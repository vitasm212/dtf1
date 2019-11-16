using System;
using System.Linq;
using UnityEngine;

namespace DTF.ui
{
    public class PackPanelView : MonoBehaviour, IView
    {
        [SerializeField] private Transform _cardRoot;

        public Action onClose;

        public void OnClose()
        {
            onClose?.Invoke();
        }

        public void Setup(Card[] cards)
        {
            var pack = cards.OrderBy(c => c.random).ToList();
            for (int i = 0; i < pack.Count; i++)
                AddCard(pack[i]);
        }

        private void AddCard(Card newCard)
        {
            newCard.view = GameObject.Instantiate<CardView>(Resources.Load<CardView>("ui/card/Card" + (int)newCard.type), _cardRoot);
            newCard.view.SetValue(newCard);
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