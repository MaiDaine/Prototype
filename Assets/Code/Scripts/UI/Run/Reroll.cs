using UnityEngine;
using UnityEngine.UI;

public class Reroll : MonoBehaviour
{
    public int rerollCost;
    public IntVariable playerGold;
    public GameEvent onReroll;
    public GameEvent onMoneyChanged;
    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(TriggerReroll);
    }

    public void TriggerReroll()
    {
        if (playerGold.value >= rerollCost)
        {
            playerGold.value -= rerollCost;
            onMoneyChanged.Raise();
            onReroll.Raise();
        }
    }
}