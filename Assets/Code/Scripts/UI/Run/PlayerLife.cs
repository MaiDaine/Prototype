using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class PlayerLife : MonoBehaviour
    {
        public IntVariable life;
        public IntVariable maxLife;
        public Text lifeText;

        private void Awake() { SetLife(); }

        public void SetLife() { lifeText.text = life.value.ToString() + "/" + maxLife.value.ToString(); }
    }
}