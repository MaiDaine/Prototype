using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Weapon : MonoBehaviour
    {
        private int damage;
        private Unit unit;

        public void Init(int damage, string tag, Unit unit, float animSpeed = 1f)
        {
            this.damage = damage;
            this.tag = tag;
            this.unit = unit;
            if (animSpeed == 0f)
                animSpeed = 0.01f;
            GetComponentInChildren<Animator>().speed = 1f / animSpeed;
        }

        public void OnTriggerEnter(Collider other)
        {
            UnitHealth unitHealth;
            if (other.tag != this.tag && (unitHealth = other.GetComponent<UnitHealth>()) != null)
                unitHealth.TakeDamage(damage);
        }

        private void Update()
        {
            this.transform.position = unit.transform.position;
        }

        private void OnDestroy()
        {
            GetComponentInChildren<WeaponTrigger>().gameObject.SetActive(false);
        }
    }
}
