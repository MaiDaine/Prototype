using UnityEngine;

namespace Prototype
{
    public class ControllableUnit : Unit
    {
        public enum OrderType { None, Def, Atk, Reg };//Items will replace order later
        public UnitStats unitStats;
        public SpellSlot[] spellBook;
        public float currentSpeed;
        public Transform spellOrigin;
        public Transform unitCenter;

        private PlayerController playerController;

        private void Awake()
        {
            currentStats = ScriptableObject.CreateInstance("UnitStats") as UnitStats;
            currentStats.Assign(unitStats);
            currentSpeed = currentStats.moveSpeed;
            for (int i = 0; i < spellBook.Length; i++)
                    spellBook[i].spellCooldown = 0f;
        }

        public void Initialize(PlayerController playerController)
        {
            this.playerController = playerController;
            base.Initialize(currentStats, "PlayerTeam");
        }

        public void ChangeOrder(OrderType order)
        {
            //Animation
        }

        public void UpdateSpellCooldowns()
        {
            for (int i = 0; i < spellBook.Length; i++)
                spellBook[i].spellCooldown -= Time.deltaTime;
        }

        public override void OnDeath()
        {
            playerController.OnUnitDeath();
            base.OnDeath();
        }

        public override void OnMoveSpeedChange(float changeAmount)
        {
            base.OnMoveSpeedChange(changeAmount);
            currentSpeed = currentStats.moveSpeed * moveSpeedModifier;
        }
    }
}