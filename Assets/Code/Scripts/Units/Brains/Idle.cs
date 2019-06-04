using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Brains/Units/Idle")]
    public class Idle : UnitBrain
    {
        public override void Initialize(NonControllableUnit unit, Unit enemyHero)
        {
        }

        public override void Think(Unit unit)
        {
        }
    }
}