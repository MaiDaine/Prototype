using UnityEngine;

public class Hand : MonoBehaviour
{
    public CardDeck deck;
    public CardData selectedCard;
    public GameEvent onValidCardLeaveHand;

    private CardSlot[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<CardSlot>();

        for (int i = 0; i < deck.cardsNumber; i++)
        {
            slots[i].ActivateSlot(deck.cards[i], OnCardSelected);
        }
    }

    public void OnCardSelected(CardData Card)
    {
        selectedCard.Assign(Card);
        // FIXME: Validate move
        onValidCardLeaveHand.Raise();
    }
}