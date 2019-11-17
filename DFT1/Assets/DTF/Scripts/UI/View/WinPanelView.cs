using System;
using UnityEngine;

namespace DTF.ui
{
    public class WinPanelView : MonoBehaviour, IView
    {
        [SerializeField] private Transform _transformCard;
        public Action<int> onGoMenu;

        public void Setup(Card[] cards)
        {
            cards[0].view = GameObject.Instantiate<CardView>(Resources.Load<CardView>("ui/card/Card" + (int)cards[0].type), _transformCard);
            cards[0].view.SetValue(cards[0]);
            cards[0].view.onClick = SelectCard0;

            cards[1].view = GameObject.Instantiate<CardView>(Resources.Load<CardView>("ui/card/Card" + (int)cards[1].type), _transformCard);
            cards[1].view.SetValue(cards[1]);
            cards[1].view.onClick = SelectCard1;

            cards[2].view = GameObject.Instantiate<CardView>(Resources.Load<CardView>("ui/card/Card" + (int)cards[2].type), _transformCard);
            cards[2].view.SetValue(cards[2]);
            cards[2].view.onClick = SelectCard2;
        }

        private void SelectCard0(Card obj)
        {
            onGoMenu?.Invoke(0);
        }

        private void SelectCard1(Card obj)
        {
            onGoMenu?.Invoke(1);
        }

        private void SelectCard2(Card obj)
        {
            onGoMenu?.Invoke(2);
        }

        public void OnGoMenu()
        {
            
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