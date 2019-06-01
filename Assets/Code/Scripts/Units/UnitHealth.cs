using UnityEngine;

namespace Prototype
{
    public class UnitHealth : MonoBehaviour
    {
        public GameEvent onDeath;
        public HealthBarHandler healthBar;
        [HideInInspector]
        public bool alive = false;

        private Unit unit;
        private float currentHealth;

        public void Initialize(Unit unit)
        {
            healthBar.Initialize(unit);
            alive = true;
            currentHealth = unit.currentStats.health;
            this.unit = unit;
            SetHealthUI();
        }

        public bool TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth < 1 && alive)
            {
                OnDeath();
                return true;
            }
            SetHealthUI();
            return false;
        }

        private void SetHealthUI()
        {
            healthBar.SetFillAmount((float)currentHealth / (float)unit.currentStats.health);
        }

        private void OnDeath()
        {
            alive = false;
            //DeathAnimation
            GetComponent<Unit>().gameObject.SetActive(false);
            onDeath.Raise();
        }
    }
}