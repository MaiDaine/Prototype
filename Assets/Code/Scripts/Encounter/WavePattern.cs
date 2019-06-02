using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName =  "Encounter/WavePattern/DefaultPattern")]
    public class WavePattern : ScriptableObject
    {
        public bool uniqueInGroup;
        public int[] groups;
    }
}
