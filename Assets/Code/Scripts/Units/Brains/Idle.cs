using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Brains/Units/Idle")]
    public class Idle : UnitBrain
    {
        public override void Initialize(Unit unit, Unit enemyHero)
        {
        }

        public override void Think(Unit unit)
        {
        }
    }
}