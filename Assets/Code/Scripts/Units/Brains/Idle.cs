using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Brains/Units/Idle")]
    public class Idle : UnitBrain
    {
        public override void Initialize(UnitBrain brainRef, NonControllableUnit unit, Unit enemyHero) { }

        public override void Think(Unit unit) { }
    }
}