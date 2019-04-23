using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public IntVariable level;
    public Text levelText;

    private void Awake() { SetLevel(); }

    public void SetLevel() { levelText.text = level.value.ToString(); }
}