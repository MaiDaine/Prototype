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

        public void OnTriggerEnter(Collider otherGameObject)
        {
            Unit other = otherGameObject.GetComponent<Unit>();
            if (otherGameObject.gameObject.activeSelf && other != null && unitHealth.alive)
                brain.UnitInRange(other);
        }

        public void OnTriggerExit(Collider otherGameObject)
        {
            Unit other = otherGameObject.GetComponent<Unit>();
            if (otherGameObject.gameObject.activeSelf && other != null && unitHealth.alive)
                brain.UnitExitRange(other);
        }
    }
}
