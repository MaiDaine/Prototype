using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class RerollSlot : MonoBehaviour
    {
        public Button button;
        public Text cardName;
        public Text cardCost;
        public IntVariable playerGold;
        public GameEvent onMoneyChanged;
        public CardDeck deck;

        private CardData card;
        private Action<CardData> callback;

        private void Awake()
        {
            card = ScriptableObject.CreateInstance("CardData") as CardData;
        }

        public void ActivateSlot(CardData data, Action<CardData> cb)
        {
            card.Assign(data);
            callback = cb;

            button.onClick.AddListener(CardClicked);

            cardName.text = card.unitStats.unitName;
            cardName.enabled = true;

            cardCost.text = card.unitStats.cost.ToString();
            if (card.unitStats.cost > playerGold.value)
                cardCost.color = new Color(255, 0, 0, 255);
            cardCost.enabled = true;
        }

        public void CleanSlot()
        {
            button.onClick.RemoveListener(CardClicked);

            cardName.text = "";
            cardName.enabled = false;

            cardCost.text = "";
            cardCost.enabled = false;
        }

        public void OnGoldChange()
        {
            if (card.unitStats.cost > playerGold.value)
                cardCost.color = new Color(255, 0, 0, 255);
            else
                cardCost.color = new Color(0, 0, 0, 255);
        }

        public void CardClicked()
        {
            if (deck.cardsNumber < deck.cards.Length
                && playerGold.value >= card.unitStats.cost)
            {

                playerGold.value -= card.unitStats.cost;
                onMoneyChanged.Raise();
                callback(card);
            }
        }
    }
}