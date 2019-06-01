using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public abstract class UnitBrain : ScriptableObject
    {
        protected Unit unit;
        protected Unit enemyHero;
        protected List<Unit> enemyUnits = new List<Unit>();
        protected List<Unit> allyUnits = new List<Unit>();
        protected Unit currentTarget = null;
        protected UnitAttack unitAttack;

        public virtual void Assign(UnitSet set) { }

        public virtual void Initialize(Unit unit, Unit enemyHero)
        {
            this.unit = unit;
            this.enemyHero = enemyHero;
            unitAttack = unit.GetComponent<UnitAttack>();
        }

        public virtual void UnitInRange(Unit other)
        {
            //TODO should change targets
            if (other.tag == unit.tag)
                allyUnits.Add(other);
            else
            {
                enemyUnits.Add(other);
                if (currentTarget == enemyHero)
                    currentTarget = other;
            }
        }

        public virtual void UnitExitRange(Unit other)
        {
            if (other.tag == unit.tag)
                allyUnits.Remove(other);
            else
                enemyUnits.Remove(other);
        }

        public abstract void Think(Unit unit);
    }
}