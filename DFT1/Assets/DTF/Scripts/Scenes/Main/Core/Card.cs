using UnityEngine;
namespace DTF
{
    public enum CardType
    {
        Attack = 1,
        Move = 2,
        HpPlus = 3,
        AttackPlus = 4,
    }

    public enum CardDirection
    {
        Left = -1,
        Rigth = 1,
    }

    public enum CardAttackType
    {
        Point = 1,
        Zone = 2,
    }

    public class Card
    {
        public CardView view;
        public CardType type;
        public CardDirection direction;
        public CardAttackType attackType;
        public int value;
        public int random;

        public Card(CardType cardType, int cardValue, CardDirection cardDirection, CardAttackType cardAttackType)
        {
            type = cardType;
            value = cardValue;
            direction = cardDirection;
            attackType = cardAttackType;
            random = Random.Range(0, 100);
        }
    }
}