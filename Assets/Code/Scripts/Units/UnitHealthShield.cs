using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class UnitHealthShield : UnitHealth
    {
        public bool isShield = false;

        private int currentShield;
        private float shieldTimer;
        private List<Action> callbacksOnShieldEnd = new List<Action>();

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
                    OnShieldEnd();
            }
        }

        public void AddShield(int amount, float timer, Action callback = null)
        {
            isShield = true;
            currentShield += amount;
            if (timer > shieldTimer)
                shieldTimer = timer;
            SetShieldUI();
            if (callback != null)
                callbacksOnShieldEnd.Add(callback);
        }

        public override bool TakeDamage(int amount)
        {
            if (isShield)
            {
                currentShield -= amount;
                if (currentShield <= 0)
                {
                    int damage = currentShield * -1;
                    OnShieldEnd();
                    return base.TakeDamage(currentShield * -1);
                }
                else
                    SetShieldUI();
            }
            else
                return base.TakeDamage(amount);

            return false;
        }

        private void OnShieldEnd()
        {
            currentShield = 0;
            SetShieldUI();
            isShield = false;
            foreach (Action callback in callbacksOnShieldEnd)
                if (callback != null)
                    callback.Invoke();
            callbacksOnShieldEnd.Clear();
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