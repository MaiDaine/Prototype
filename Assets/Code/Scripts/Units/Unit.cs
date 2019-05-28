using UnityEngine;
using UnityEngine.AI;

namespace Prototype
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Unit : MonoBehaviour
    {

        public UnitBrain brain;
        [HideInInspector]
        public UnitStats currentStats;

        private UnitHealth unitHealth;

        private void Awake()
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            unitHealth = GetComponent<UnitHealth>();
        }

        public void Initialize(Unit enemyHero, UnitStats stats, string tag)
        {
            currentStats.Assign(stats);
            this.tag = tag;
            GetComponent<UnitHealth>().Initialize(this);
            GetComponent<UnitMovement>().Initialize(currentStats);
            GetComponent<UnitAttack>().Initialize(this);
            GetComponent<CapsuleCollider>().radius = stats.atkRange;
            GetComponent<CapsuleCollider>().enabled = true;
            brain = ScriptableObject.CreateInstance(brain.name) as UnitBrain;
            brain.Initialize(this, enemyHero);
        }

        private void Update()
        {
            if (unitHealth.alive)
                brain.Think(this);
        }

        public int SortByDistance(Unit a, Unit b)
        {
            return ((this.transform.position - a.transform.position).sqrMagnitude.CompareTo((this.transform.position - b.transform.position).sqrMagnitude));
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