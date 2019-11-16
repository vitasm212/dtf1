using System;
using System.Collections.Generic;
using UnityEngine;

namespace DTF.ui
{
    public class TopPanelView : MonoBehaviour, IView
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _cardRoot;
        [SerializeField] private CardView[] _cards;

        private List<Card> _listCard = new List<Card>();

        public Action onGoMenu;
        public Action<Card> onSelectCard;

        public void AddCard(Card newCard)
        {
            newCard.view = GameObject.Instantiate<CardView>(_cards[(int)newCard.type], _cardRoot);
            newCard.view.SetValue(newCard);
            newCard.view.onClick = SelectCard;
            _listCard.Add(newCard);
        }

        public void ClealCard()
        {
            for (int i = 0; i < _listCard.Count; i++)
                GameObject.Destroy(_listCard[i].view.gameObject);
            _listCard.Clear();
        }

        private void SelectCard(Card card)
        {
            onSelectCard?.Invoke(card);
            GameObject.Destroy(card.view.gameObject);
            _listCard.Remove(card);
        }

        public void SetInteractable(bool value)
        {
            _canvasGroup.interactable = value;
        }

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