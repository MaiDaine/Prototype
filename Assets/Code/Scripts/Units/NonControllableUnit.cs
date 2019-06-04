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

        public override void OnDeath()
        {
            EncounterController.instance.OnUnitDeath(this);
            base.OnDeath();
        }

        //Status
        public override void OnStunStart()
        {
            if (stuns == 0)
                GetComponent<UnitMovement>().StopMovement();
            base.OnStunStart();
        }

        public override void OnStunEnd()
        {
            base.OnStunEnd();
            if (stuns == 0)
                GetComponent<UnitMovement>().ResumeMovement();
        }

        public override void OnRootStart()
        {
            if (roots == 0)
                GetComponent<UnitMovement>().StopMovement();
            base.OnRootStart();
        }

        public override void OnRootEnd()
        {
            base.OnRootEnd();
            if (roots == 0)
                GetComponent<UnitMovement>().StopMovement();
        }
    }
}