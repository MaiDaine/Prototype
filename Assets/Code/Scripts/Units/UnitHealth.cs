using UnityEngine;

namespace Prototype
{
    public class UnitHealth : MonoBehaviour
    {
        public HealthBarHandler healthBar;
        [HideInInspector]
        public bool alive = false;

        protected Unit unit;
        protected float currentHealth;

        public virtual void Initialize(Unit unit)
        {
            healthBar.Initialize(unit);
            alive = true;
            currentHealth = unit.currentStats.health;
            this.unit = unit;
            SetHealthUI();
        }

        public virtual bool TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth < 1 && alive)
            {
                alive = false;
                unit.OnDeath();
                return true;
            }
            SetHealthUI();
            return false;
        }

        private void SetHealthUI()
        {
            healthBar.SetHealthFillAmount((float)currentHealth / (float)unit.currentStats.health);
        }
    }
}