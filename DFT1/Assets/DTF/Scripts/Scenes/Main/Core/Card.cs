using UnityEngine;
using System.Collections;
namespace DTF
{
    public enum CardType
    {
        Attack = 0,
        Move = 1,
    }

    public class Card
    {
        public CardView view;
        public CardType type;
        public int value;

        public Card(CardType cardType, int cardValue)
        {
            type = cardType;
            value = cardValue;
        }
    }
}