using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(UnitMovement))]
    public class NonControllableUnit : Unit
    {
        public UnitBrain brain;
        public UnitAttack unitAttack;

        public void Initialize(Unit enemyHero, UnitStats stats, string tag)
        {
            base.Initialize(stats, tag);
            GetComponent<UnitMovement>().Initialize(currentStats);
            unitAttack.Initialize(this, tag);
            brain = ScriptableObject.CreateInstance(brain.name) as UnitBrain;
            brain.Initialize(this, enemyHero);
        }

        private void Update()
        {
            if (unitHealth.alive && stuns == 0)
                brain.Think(this);
        }
    }
}
