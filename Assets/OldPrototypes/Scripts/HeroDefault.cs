using UnityEngine;

namespace Prototype
{
    public class HeroDefault : UnitBrain
    {
        public UnitSet targets;

        public override void Assign(UnitSet set) { targets = set; }

        public override void Initialize(NonControllableUnit unit, Unit enemyHero)
        {
            base.Initialize(unit, enemyHero);
            unit.GetComponent<UnitHealth>().alive = true;
        }

        public override void Think(Unit unit)
        {
            if (currentTarget == null && !SelectTarget(unit))
                return;
            if (unitAttack.atkReload <= 0f && unitAttack.Attack(currentTarget))
                currentTarget = null;
        }

        private bool SelectTarget(Unit unit)
        {
            if (targets.items.Count > 0)
            {
                foreach (Unit element in targets.items)
                    if (element != null && element.isActiveAndEnabled && element.tag != unit.tag)
                    {
                        currentTarget = element;
                        return true;
                    }
            }
            return false;
        }
    }
}