using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(UnitHealth))]
    [RequireComponent(typeof(UnitMovement))]
    [RequireComponent(typeof(UnitAttack))]
    public class Unit : MonoBehaviour
    {

        [HideInInspector]
        public UnitStats currentStats;

        protected UnitHealth unitHealth;

        public virtual void Initialize(UnitStats stats, string tag)
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.Assign(stats);
            unitHealth = GetComponent<UnitHealth>();
            this.tag = tag;
            GetComponent<UnitHealth>().Initialize(this);
            GetComponent<UnitMovement>().Initialize(currentStats);
            GetComponent<UnitAttack>().Initialize(this);
        }

        public int SortByDistance(Unit a, Unit b)
        {
            return ((this.transform.position - a.transform.position).sqrMagnitude.CompareTo((this.transform.position - b.transform.position).sqrMagnitude));
        }
    }
}