using UnityEngine.UI;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    public GameEvent onDeath;
    [HideInInspector]
    public bool alive = false;
    public HealthBarHandler healthBar;

    private Unit unit;
    private float currentHealth;

    public void Initialize(Unit unit)
    {
        HealthBarHandler hbHandler = healthBar.GetComponentsInParent(typeof(HealthBarHandler), true)[0] as HealthBarHandler;
        hbHandler.Initialize();
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
