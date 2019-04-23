using UnityEngine;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour
{
    public Text cardName;
    public Text cardCost;

    private CardData card;

    public void ActivateSlot(CardData data)
    {
        card.Assign(data);

        cardName.text = card.unitStats.unitName;
        cardName.enabled = true;

        cardCost.text = card.unitStats.cost.ToString();
        cardCost.enabled = true;
    }

    public void CleanSlot()
    {
        cardName.text = "";
        cardName.enabled = false;

        cardCost.text = "";
        cardCost.enabled = false;
    }

    private void Awake()
    {
        card = ScriptableObject.CreateInstance("CardData") as CardData;
    }
}