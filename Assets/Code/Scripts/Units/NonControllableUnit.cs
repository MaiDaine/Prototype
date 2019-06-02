using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(UnitMovement))]
    [RequireComponent(typeof(UnitAttack))]
    public class NonControllableUnit : Unit
    {
        public UnitBrain brain;

        public void Initialize(Unit enemyHero, UnitStats stats, string tag)
        {
            base.Initialize(stats, tag);
            GetComponent<UnitMovement>().Initialize(currentStats);
            GetComponent<UnitAttack>().Initialize(this);
            brain = ScriptableObject.CreateInstance(brain.name) as UnitBrain;
            brain.Initialize(this, enemyHero);
        }

        private void Update()
        {
            if (unitHealth.alive)
                brain.Think(this);
        }

        private void OnTriggerEnter(Collider otherGameObject)
        {
            Unit other = otherGameObject.GetComponent<Unit>();
            if (otherGameObject.gameObject.activeSelf && other != null && unitHealth.alive)
                brain.UnitInRange(other);
        }

        private void OnTriggerExit(Collider otherGameObject)
        {
            Unit other = otherGameObject.GetComponent<Unit>();
            if (otherGameObject.gameObject.activeSelf && other != null && unitHealth.alive)
                brain.UnitExitRange(other);
        }
    }
}
