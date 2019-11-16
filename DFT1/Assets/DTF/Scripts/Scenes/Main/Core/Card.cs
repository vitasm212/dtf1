using UnityEngine;
using System.Collections;
namespace DTF
{
    public enum CardType
    {
        Attack = 1,
        Move = 2,
    }

    public class Card
    {
        public CardView view;
        public CardType type;
        public int value;
        public int random;

        public Card(CardType cardType, int cardValue)
        {
            type = cardType;
            value = cardValue;
            random = Random.Range(0, 100);
        }
    }
}