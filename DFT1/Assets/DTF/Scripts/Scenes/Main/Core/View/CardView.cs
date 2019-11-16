using System;
using UnityEngine;
using UnityEngine.UI;

namespace DTF
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public Action<Card> onClick;
        private Card _card;

        public void SetValue(Card card)
        {
            _text.text = card.value.ToString();
            _card = card;
        }

        public void OnClick()
        {
            onClick?.Invoke(_card);
        }
    }
}