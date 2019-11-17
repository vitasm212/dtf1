using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DTF.ui
{
    public class TopPanelView : MonoBehaviour, IView
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Text _curentPower;
        [SerializeField] private Transform _cardRoot;

        [SerializeField] private RectTransform _startNewCard;
        [SerializeField] private RectTransform _targetNewCard;
        [SerializeField] private RectTransform _targetOldCard;
        [SerializeField] private RectTransform _pakOldCard;

        private List<Card> _listCard = new List<Card>();
        private RectTransform _flyNewCard;
        private float _flyNewProgress;
        private float _flyNewDist;

        private RectTransform _flyOldCard;
        private float _flyOldProgress;
        private float _flyOldDist;

        public Action onShowPack;
        public Action onEndAnimationNewCard;
        public Action onGoMenu;
        public Action<Card> onSelectCard;

        public void OnShowPack()
        {
            onShowPack?.Invoke();
        }

        public void AddCard(Card newCard)
        {
            newCard.view = GameObject.Instantiate<CardView>(Resources.Load<CardView>("ui/card/Card" + (int)newCard.type), _startNewCard);
            newCard.view.SetValue(newCard);
            newCard.view.onClick = SelectCard;
            _listCard.Add(newCard);
        }

        private void Update()
        {
            if (_flyNewCard == null)
            {
                if (_startNewCard.childCount > 0)
                {
                    _targetNewCard.SetAsLastSibling();
                    var f = _startNewCard.GetChild(0);
                    f.SetParent(_targetNewCard);
                    _flyNewCard = f.GetComponent<RectTransform>();
                    _flyNewProgress = 1;
                    _flyNewDist = _flyNewCard.localPosition.magnitude;
                }
            }

            if (_flyNewCard != null)
            {
                _flyNewProgress -= Time.deltaTime * 2;
                _flyNewCard.localPosition = _flyNewProgress * _flyNewDist * _flyNewCard.localPosition.normalized;
                if (_flyNewProgress < 0.1f)
                {
                    _flyNewCard.transform.SetParent(_cardRoot);
                    _flyNewCard = null;
                    _targetNewCard.SetAsLastSibling();
                    if (_startNewCard.childCount == 0)
                        onEndAnimationNewCard?.Invoke();
                }
            }
            //*************
            if (_flyOldCard == null)
            {
                if (_targetOldCard.childCount > 0)
                {
                    var f = _targetOldCard.GetChild(0);
                    //f.SetParent(_targetNewCard);
                    _flyOldCard = f.GetComponent<RectTransform>();
                    _flyOldProgress = 1;
                    _flyOldDist = _flyOldCard.localPosition.magnitude;
                }
            }

            if (_flyOldCard != null)
            {
                _flyOldProgress -= Time.deltaTime * 2;
                _flyOldCard.localPosition = _flyOldProgress * _flyOldDist * _flyOldCard.localPosition.normalized;
                if (_flyOldProgress < 0.1f)
                {
                    _flyOldCard.transform.SetParent(_pakOldCard);
                    _flyOldCard = null;
                }
            }
        }

        public void ClealCard()
        {
            for (int i = 0; i < _listCard.Count; i++)
            {
                _listCard[i].view.gameObject.GetComponent<Button>().interactable = false;
                _listCard[i].view.transform.SetParent(_targetOldCard);
            }

            for (int i = 0; i < _pakOldCard.childCount; i++)
            {
                GameObject.Destroy(_pakOldCard.GetChild(i).gameObject);
            }
            _listCard.Clear();
        }

        private void SelectCard(Card card)
        {
            if (_startNewCard.childCount > 0 || _targetOldCard.childCount > 0)
                return;

            onSelectCard?.Invoke(card);
            GameObject.Destroy(card.view.gameObject);
            _listCard.Remove(card);
        }

        public void SetInteractable(bool value)
        {
            _canvasGroup.interactable = value;
        }

        public void SetCurentPower(int power)
        {
            if(_curentPower!= null)
            {
                if (power > 0)
                    _curentPower.text = $"power +{power}";
                else
                    _curentPower.text = "";
            }
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