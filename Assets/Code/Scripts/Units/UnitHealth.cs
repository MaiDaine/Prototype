using UnityEngine;

namespace Prototype
{
    public class UnitHealth : MonoBehaviour
    {
        public HealthBarHandler healthBar;
        [HideInInspector]
        public bool alive = false;
        [HideInInspector]
        public float currentHealth;

        protected Unit unit;

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

        public virtual void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > unit.currentStats.health)
                currentHealth = unit.currentStats.health;
            SetHealthUI();
        }

        public bool IsFullLife() { return currentHealth == unit.currentStats.health; }

        private void SetHealthUI()
        {
            healthBar.SetHealthFillAmount((float)currentHealth / (float)unit.currentStats.health);
        }
    }
}