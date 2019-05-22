using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Variables/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
        public bool value;

        public void SetValue(bool value)
        {
            this.value = value;
        }

        public void SetValue(BoolVariable value)
        {
            this.value = value.value;
        }

        public void InvertValue()
        {
            value = !value;
        }
    }
}