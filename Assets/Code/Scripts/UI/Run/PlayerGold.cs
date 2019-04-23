using UnityEngine;
using UnityEngine.UI;

public class PlayerGold : MonoBehaviour
{
    public IntVariable gold;
    public Text goldText;

    private void Awake() { SetGold(); }

    public void SetGold() { goldText.text = gold.value.ToString(); }
}