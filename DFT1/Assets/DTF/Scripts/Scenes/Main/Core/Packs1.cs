using System.Collections.Generic;
using System.Linq;

namespace DTF
{
    public class Packs1
    {
        private int[] _updateIndex = new int[3];
        public int GetUpdateIndex(int index)
        {
            return _updateIndex[index];
        }

        public Card[] GetUpdateCard()
        {
            Card[] cards = new Card[3];
            _updateIndex[0] = UnityEngine.Random.Range(0, _cardsAditional.Length);
            _updateIndex[2] = UnityEngine.Random.Range(0, _cardsAditional.Length);
            _updateIndex[1] = UnityEngine.Random.Range(0, _cardsAditional.Length);

            return new Card[]{ _cardsAditional[_updateIndex[0]], _cardsAditional[_updateIndex[1]], _cardsAditional[_updateIndex[2]] };
        }
        private Card[] _cardsAditional = new Card[]
      {
                new Card(CardType.Move, 4, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Move, 5, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Move, 6, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Move, 4, CardDirection.Rigth, CardAttackType.Point),
                new Card(CardType.Move, 5, CardDirection.Rigth, CardAttackType.Point),
                new Card(CardType.Move, 6, CardDirection.Rigth, CardAttackType.Point),

                new Card(CardType.Attack, 4, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Attack, 5, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Attack, 6, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Attack, 4, CardDirection.Rigth, CardAttackType.Zone),
                new Card(CardType.Attack, 5, CardDirection.Rigth, CardAttackType.Zone),
                new Card(CardType.Attack, 6, CardDirection.Rigth, CardAttackType.Zone),

                new Card(CardType.AttackPlus, 1, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.AttackPlus, 2, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.AttackPlus, 3, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.AttackPlus, 4, CardDirection.Left, CardAttackType.Point),

                new Card(CardType.HpPlus, 1, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.HpPlus, 2, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.HpPlus, 3, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.HpPlus, 4, CardDirection.Left, CardAttackType.Point),
      };


        private List<Card> _packCard = new List<Card>();
        private List<Card> _packCard0 = new List<Card>();

        public int Count
        {
            get
            {
                return _packCard.Count;
            }
        }

        public Card[] ShowAll()
        {
            return _packCard.ToArray();
        }

        public void Reset()
        {
            _packCard = _packCard0.OrderBy(c => c.random).ToList();

            for (int i = 0; i < _packCard.Count; i++)
            {
                var card = _packCard[i];
                card.random = UnityEngine.Random.Range(0, 100);
                _packCard[i] = card;

                var card0 = _packCard0[i];
                card0.random = card.random;
                _packCard0[i] = card0;
            }
        }

        public Card Get(int index)
        {
            Card card = _packCard[index];
            _packCard.RemoveAt(index);
            return card;
        }

        public void GeneratePack(int count, List<int> additioal)
        {
            Card[] cards = new Card[]
                {
                new Card(CardType.Move, 3, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Move, 2, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Move, 1, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Move, 3, CardDirection.Rigth, CardAttackType.Point),
                new Card(CardType.Move, 2, CardDirection.Rigth, CardAttackType.Point),
                new Card(CardType.Move, 1, CardDirection.Rigth, CardAttackType.Point),

                new Card(CardType.Attack, 3, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Attack, 2, CardDirection.Left, CardAttackType.Point),
                new Card(CardType.Attack, 1, CardDirection.Left, CardAttackType.Point),
                 new Card(CardType.Attack, 3, CardDirection.Left, CardAttackType.Zone),
                new Card(CardType.Attack, 2, CardDirection.Left, CardAttackType.Zone),
                new Card(CardType.Attack, 1, CardDirection.Left, CardAttackType.Zone),
                new Card(CardType.Attack, 3, CardDirection.Rigth, CardAttackType.Point),
                new Card(CardType.Attack, 2, CardDirection.Rigth, CardAttackType.Point),
                new Card(CardType.Attack, 1, CardDirection.Rigth, CardAttackType.Point),
                new Card(CardType.Attack, 3, CardDirection.Rigth, CardAttackType.Zone),
                new Card(CardType.Attack, 2, CardDirection.Rigth, CardAttackType.Zone),
                new Card(CardType.Attack, 1, CardDirection.Rigth, CardAttackType.Zone),
                };

            if(additioal!=null)
            {
                foreach(var i in additioal)
                {
                    var card = _cardsAditional[i];
                    card.random = UnityEngine.Random.Range(0, 100);
                    _packCard.Add(card);
                    _packCard0.Add(card);
                }
            }

            foreach (var card in cards)
            {
                card.random = UnityEngine.Random.Range(0, 100);
                _packCard.Add(card);
                _packCard0.Add(card);
            }

            _packCard = _packCard.OrderBy(c => c.random).ToList();
            for (int i = 0; i < _packCard.Count; i++)
            {
                var card = _packCard[i];
                card.random = UnityEngine.Random.Range(0, 100);
                _packCard[i] = card;

                var card0 = _packCard0[i];
                card0.random = card.random;
                _packCard0[i] = card0;
            }
        }
    }
}