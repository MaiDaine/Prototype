using UnityEngine;

namespace Prototype
{
    public class UnitHealthShield : UnitHealth
    {
        public bool isShield = false;

        private int currentShield;
        private float shieldTimer;

        public override void Initialize(Unit unit)
        {
            base.Initialize(unit);
            currentShield = 0;
        }

        private void Update()
        {
            if (isShield)
            {
                shieldTimer -= Time.deltaTime;
                if (shieldTimer <= 0f)
                {
                    currentShield = 0;
                    SetShieldUI();
                }
            }
        }

        public void AddShield(int amount, float timer)
        {
            isShield = true;
            currentShield += amount;
            if (timer > shieldTimer)
                shieldTimer = timer;
            SetShieldUI();
        }

        public override bool TakeDamage(int amount)
        {
            if (isShield)
            {
                currentShield -= amount;
                SetShieldUI();
                if (currentShield <= 0)
                {
                    int damage = currentShield * -1;
                    currentShield = 0;
                    isShield = false;
                    return base.TakeDamage(currentShield * -1);
                }
            }
            else
                return base.TakeDamage(amount);

            return false;
        }

        private void SetShieldUI()
        {
            if (currentShield > unit.currentStats.health)
                ((HealthShieldBarHandler)healthBar).SetShieldFillAmount(1f);
            else
                ((HealthShieldBarHandler)healthBar).SetShieldFillAmount((float)currentShield / (float)unit.currentStats.health);
        }
    }
}