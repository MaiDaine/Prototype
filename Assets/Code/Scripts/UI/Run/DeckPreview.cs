using UnityEngine;

namespace Prototype
{
    public class DeckPreview : MonoBehaviour
    {
        public CardDeck deck;

        private CardPreview[] slots;

        private void Start()
        {
            slots = GetComponentsInChildren<CardPreview>();

            RefreshDeck();
        }

        public void RefreshDeck()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < deck.cardsNumber)
                    slots[i].ActivateSlot(deck.cards[i]);
                else
                    slots[i].CleanSlot();
            }
        }
    }
}