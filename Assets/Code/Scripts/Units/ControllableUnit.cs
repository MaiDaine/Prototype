using UnityEngine;

namespace Prototype
{
    public class ControllableUnit : MonoBehaviour
    {
        public enum OrderType { None, Def, Atk, Reg };
        public UnitStats unitStats;
        public ControllableUnitBrain brain;
        public bool playerControl = false;
        public ASpell[] spellBook = new ASpell[4];

        [HideInInspector]
        public UnitStats currentStats;

        private void Awake()
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.Assign(unitStats);
            GetComponent<UnitMovement>().Initialize(unitStats);
        }

        public void Initialize(PlayerController playerController)
        {
            brain = ScriptableObject.CreateInstance(brain.name) as ControllableUnitBrain;
            brain.Initialize(playerController, this);
        }

        private void Update()
        {
            if (!playerControl)
            {
                brain.Think();
                this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1f, transform.eulerAngles.z);
            }
        }

        public void ChangeOrder(OrderType order)
        {
            //Animation
            brain.ChangeOrder(order);
        }
    }
}