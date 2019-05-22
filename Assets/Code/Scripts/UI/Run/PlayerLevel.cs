using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class PlayerLevel : MonoBehaviour
    {
        public IntVariable level;
        public Text levelText;

        private void Awake() { SetLevel(); }

        public void SetLevel() { levelText.text = level.value.ToString(); }
    }
}