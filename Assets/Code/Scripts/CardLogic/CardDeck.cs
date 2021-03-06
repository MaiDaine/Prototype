﻿using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Card/CardDeck")]
    public class CardDeck : ScriptableObject
    {
        public int cardsNumber;
        public CardData[] cards;
    }
}
