using UnityEngine;

namespace Prototype
{
    public class UnitAttack : MonoBehaviour
    {
        [HideInInspector]
        public float atkReload;

        private NonControllableUnit unit;

        public void Initialize(Unit unit, string tag)
        {
            this.unit = (NonControllableUnit)unit;
            this.tag = tag;
            atkReload = unit.currentStats.atkReload;

            CapsuleCollider atkRange = unit.gameObject.AddComponent<CapsuleCollider>();
            atkRange.isTrigger = true;
            atkRange.radius = unit.currentStats.atkRange;
            atkRange.enabled = true;
        }

        private void FixedUpdate()
        {
            atkReload -= Time.deltaTime;
        }

        public bool Attack(Unit target)
        {
            //Atk Animation
            atkReload = unit.currentStats.atkReload;
            return target.GetComponent<UnitHealth>().TakeDamage(unit.currentStats.atkDmg);
        }

        private void OnTriggerEnter(Collider other)
        {
            Unit otherUnit = other.GetComponent<Unit>();
            if (other.gameObject.activeSelf && other != null && unit.unitHealth.alive)
                unit.brain.UnitInRange(otherUnit);
        }

        private void OnTriggerExit(Collider other)
        {
            Unit otherUnit = other.GetComponent<Unit>();
            if (other.gameObject.activeSelf && other != null && unit.unitHealth.alive)
                unit.brain.UnitExitRange(otherUnit);
        }
    }
}