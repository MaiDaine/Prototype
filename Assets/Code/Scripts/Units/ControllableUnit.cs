using UnityEngine;

namespace Prototype
{
    public class ControllableUnit : Unit
    {
        public enum OrderType { None, Def, Atk, Reg };
        public UnitStats unitStats;
        public ControllableUnitBrain brain;
        public bool playerControl = false;
        public SpellSlot[] spellBook;

        private void Awake()
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.Assign(unitStats);
            GetComponent<UnitMovement>().Initialize(unitStats);
            for (int i = 0; i < spellBook.Length; i++)
                    spellBook[i].spellCooldown = 0f;
        }

        public void Initialize(PlayerController playerController)
        {
            base.Initialize(currentStats, "PlayerTeam");
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
            UpdateSpellCooldowns();
        }

        public void ChangeOrder(OrderType order)
        {
            //Animation
            brain.ChangeOrder(order);
        }

        private void UpdateSpellCooldowns()
        {
            for (int i = 0; i < spellBook.Length; i++)
                spellBook[i].spellCooldown -= Time.deltaTime;
        }
    }
}