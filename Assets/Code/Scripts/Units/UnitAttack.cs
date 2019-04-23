using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [HideInInspector]
    public float atkReload;

    private Unit unit;

    public void Initialize(Unit unit)
    {
        this.unit = unit;
        atkReload = unit.currentStats.atkReload;
    }

    private void FixedUpdate()
    {
        atkReload -= Time.deltaTime;
    }

    public bool Attack(Unit target)
    {
        //Atk Animation
        atkReload = unit.currentStats.atkReload;
        return target.GetComponent<UnitHealth>().TakeDamage(unit.currentStats.atkDmg);
    }
}
