using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Card/CardData")]
    public class CardData : ScriptableObject
    {
        public UnitStats unitStats;
        public Unit unit;

        public void Assign(CardData Card)
        {
            unitStats = Card.unitStats;
            unit = Card.unit;
        }
    }
}