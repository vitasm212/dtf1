using System;
using UnityEngine;
using UnityEngine.UI;

namespace DTF
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Text _value;
        [SerializeField] private Text _direction;

        public Action<Card> onClick;
        private Card _card;

        public void SetValue(Card card)
        {
            switch (card.type)
            {
                case CardType.AttackPlus:
                    _value.text = "+" + card.value;
                    _direction.text = "power";
                    break;
                case CardType.HpPlus:
                    _value.text = "+" + card.value;
                    _direction.text = "HP";
                    break;
                case CardType.Attack:
                case CardType.Move:
                    string zone = "";
                    if (card.type == CardType.Attack && card.attackType == CardAttackType.Zone)
                        zone = "***";
                    _value.text = card.value.ToString() + zone;

                    _direction.text = card.direction.ToString();
                    break;
            }
            _card = card;
        }

        public void OnClick()
        {
            onClick?.Invoke(_card);
        }
    }
}