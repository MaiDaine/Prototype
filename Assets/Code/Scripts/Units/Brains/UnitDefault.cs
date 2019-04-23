﻿using UnityEngine;

[CreateAssetMenu(menuName = "Brains/UnitDefault")]
public class UnitDefault : UnitBrain
{
    public override void Initialize(Unit unit, Unit enemyHero)
    {
        base.Initialize(unit, enemyHero);
        enemyUnits.Add(enemyHero);
    }

    public override void Think(Unit unit)
    {
        if ((currentTarget == null || !currentTarget.isActiveAndEnabled) && !SelectTarget(unit))
            return;
        if (Vector3.Distance(unit.transform.position, currentTarget.transform.position) <= unit.currentStats.atkRange)
        {
            if (unitAttack.atkReload <= 0f && unitAttack.Attack(currentTarget))
            {
                //OnKill
            }
            else
                unit.GetComponent<UnitMovement>().StopMovement();
        }
        else
            unit.GetComponent<UnitMovement>().SetAgentDestination(currentTarget.transform.position);

    }

    private bool SelectTarget(Unit unit)
    {
        for (int i = enemyUnits.Count - 1; i > -1; i--)
            if (enemyUnits[i] == null || !enemyUnits[i].isActiveAndEnabled)
                enemyUnits.RemoveAt(i);
        if (enemyUnits.Count == 0)
        {
            currentTarget = null;
            return false;
        }
        enemyUnits.Sort(unit.SortByDistance);
        currentTarget = enemyUnits[0];
        return true;
    }
}