using System;
using UnityEngine;
using UnityEngine.UI;

namespace DTF
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private GameObject[] _value;
        [SerializeField] private Transform _transformValue;
        //[SerializeField] private Text _direction;

        [SerializeField] private GameObject _attackPoint;
        [SerializeField] private GameObject _attackZone;

        public Action<Card> onClick;
        private Card _card;

        public void SetValue(Card card)
        {
            //switch (card.type)
            //{
            //    case CardType.AttackPlus:
            //        _value.text = "+" + card.value;
            //        _direction.text = "power";
            //        break;
            //    case CardType.HpPlus:
            //        _value.text = "+" + card.value;
            //        _direction.text = "HP";
            //        break;
            //    case CardType.Attack:
            //    case CardType.Move:
            //        string zone = "";
            //        if (card.type == CardType.Attack && card.attackType == CardAttackType.Zone)
            //            zone = "***";
            //        _value.text = card.value.ToString() + zone;

            //        _direction.text = card.direction.ToString();
            //        break;
            //}
            if (_value != null)
                for (int i = 0; i < _value.Length; i++)
                {
                    _value[i].SetActive(i < card.value);
                }

            if (card.type == CardType.Attack || card.type == CardType.Move)
            {
                if (_transformValue != null)
                    _transformValue.localScale = new Vector3(card.direction == CardDirection.Rigth ? 1 : -1, 1, 1);
            }

            if (card.type == CardType.Attack)
            {
                if (card.attackType == CardAttackType.Point)
                {
                    _attackPoint.SetActive(true);
                    _attackZone.SetActive(false);
                }
                else
                {
                    _attackPoint.SetActive(false);
                    _attackZone.SetActive(true);
                }
            }

            _card = card;
        }

        public void OnClick()
        {
            onClick?.Invoke(_card);
        }
    }
}