using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class CardSlot : MonoBehaviour
    {
        public Button button;
        public Text cardName;
        public Text cardCost;
        public CardCooldown cooldown;

        private CardData card;
        private Action<CardData> callback;

        private void Awake()
        {
            card = ScriptableObject.CreateInstance("CardData") as CardData;
        }

        public void ActivateSlot(CardData data, Action<CardData> cb)
        {
            card.Assign(data);
            cooldown.Initialize(card.unitStats.cooldown);
            callback = cb;

            button.onClick.AddListener(CardClicked);

            cardName.text = card.unitStats.unitName;
            cardName.enabled = true;

            cardCost.text = card.unitStats.cost.ToString();
            cardCost.enabled = true;
        }

        public void CardClicked()
        {
            if (!cooldown.IsOnCooldown())
            {
                cooldown.StartCooldown();
                callback(card);
            }
            else
            {
                Debug.Log("Card is on Cooldown");
            }
        }

        public void CleanSlot()
        {
            button.onClick.RemoveListener(CardClicked);

            cardName.text = "";
            cardName.enabled = false;

            cardCost.text = "";
            cardCost.enabled = false;
        }
    }
}