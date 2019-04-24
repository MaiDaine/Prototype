using UnityEngine;

public class RerollPanel : MonoBehaviour
{
    public CardPool cardPool;
    public CardDeck deck;
    public GameEvent onCardAddedToDeck;

    private RerollSlot[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<RerollSlot>();
        TriggerReroll();
    }

    public void TriggerReroll()
    {
        CardData[] roll = cardPool.GetCardRoll();

        for (int i = 0; i < roll.Length; i++)
        {
            slots[i].CleanSlot();
            slots[i].ActivateSlot(roll[i], OnCardPicked);
        }
    }

    public void OnCardPicked(CardData card)
    {
        deck.cards[deck.cardsNumber] = ScriptableObject.CreateInstance("CardData") as CardData;
        deck.cards[deck.cardsNumber].Assign(card);
        deck.cardsNumber++;

        onCardAddedToDeck.Raise();
    }
}