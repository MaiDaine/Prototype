using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public IntVariable life;
    public IntVariable maxLife;
    public Text lifeText;

    private void Awake() { SetLife(); }

    public void SetLife() { lifeText.text = life.value.ToString() + "/" + maxLife.value.ToString(); }
}