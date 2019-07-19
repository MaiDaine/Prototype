using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(UnitHealth))]
    [RequireComponent(typeof(UnitStatusManager))]
    public class Unit : MonoBehaviour
    {
        //Status
        [HideInInspector]
        public short stuns = 0;
        [HideInInspector]
        public short roots = 0;

        [HideInInspector]
        public UnitStats currentStats;
        [HideInInspector]
        public UnitHealth unitHealth;
        [HideInInspector]
        public float moveSpeedModifier = 1f;

        public virtual void Initialize(UnitStats stats, string tag)
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.Assign(stats);
            unitHealth = GetComponent<UnitHealth>();
            unitHealth.Initialize(this);
            this.tag = tag;
        }

        public virtual void OnDeath()
        {
            //DeathAnimation
            Destroy(gameObject);
        }

        public int SortByDistance(Unit a, Unit b)
        {
            return ((this.transform.position - a.transform.position).sqrMagnitude.CompareTo((this.transform.position - b.transform.position).sqrMagnitude));
        }

        //Status
        public virtual void OnStunStart() { stuns += 1; }
        public virtual void OnStunEnd() { stuns -= 1; }
        public virtual void OnRootStart() { roots += 1; }
        public virtual void OnRootEnd() { roots -= 1; }
        public virtual void OnMoveSpeedChange(float changeAmount)
        {
            moveSpeedModifier = Mathf.Clamp(moveSpeedModifier + changeAmount, 0.25f, 2f);
        }
    }
}