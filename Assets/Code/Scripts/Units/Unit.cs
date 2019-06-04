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
        public short roots = 0;

        [HideInInspector]
        public UnitStats currentStats;
        [HideInInspector]
        public UnitHealth unitHealth;

        public virtual void Initialize(UnitStats stats, string tag)
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.Assign(stats);
            unitHealth = GetComponent<UnitHealth>();
            this.tag = tag;
            GetComponent<UnitHealth>().Initialize(this);
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
    }
}