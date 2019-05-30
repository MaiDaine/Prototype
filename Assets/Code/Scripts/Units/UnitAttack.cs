using UnityEngine;

namespace Prototype
{
    public class UnitAttack : MonoBehaviour
    {
        [HideInInspector]
        public float atkReload;

        private Unit unit;

        public void Initialize(Unit unit)
        {
            this.unit = unit;
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
    }
}