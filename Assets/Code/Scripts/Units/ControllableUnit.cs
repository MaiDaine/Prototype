using UnityEngine;

namespace Prototype
{
    public class ControllableUnit : Unit
    {
        public enum OrderType { None, Def, Atk, Reg };//Items will replace order later
        public UnitStats unitStats;
        [HideInInspector]
        public UnitPortrait portrait;
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

        public void Initialize(PlayerController playerController, UnitPortrait unitPortrait)
        {
            this.playerController = playerController;
            unitHealth = GetComponent<UnitHealth>();
            unitHealth.healthBar = unitPortrait.GetComponent<HealthShieldBarHandler>();
            unitHealth.Initialize(this);
            portrait = unitPortrait;
            for (int i = 0; i < spellBook.Length; i++)
                portrait.spellSlots[i].icone.sprite = spellBook[i].spellIcone;
            this.tag = "PlayerTeam";
        }

        public void ChangeOrder(OrderType order)
        {
            //Animation
        }

        public void UpdateSpellCooldowns()
        {
            for (int i = 0; i < spellBook.Length; i++)
            {
                spellBook[i].spellCooldown -= Time.deltaTime;
                portrait.spellSlots[i].coolDown.text = spellBook[i].spellCooldown.ToString("#.0");
                if (spellBook[i].spellCooldown < 0f)
                {
                    portrait.spellSlots[i].mask.gameObject.SetActive(false);
                    portrait.spellSlots[i].coolDown.gameObject.SetActive(false);
                }
            }
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