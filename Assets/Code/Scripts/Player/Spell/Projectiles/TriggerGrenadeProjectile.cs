﻿using UnityEngine;

namespace Prototype
{
    public class TriggerGrenadeProjectile : Projectile
    {
        public GameObject charginEffectRef;
        public GameObject explosionEffectRef;

        private GameObject charginEffect;
        private GameObject explosionEffect;

        public override void Initialize(Vector3 direction, string ownerTag)
        {
            base.Initialize(direction, ownerTag);
            charginEffect = Instantiate(charginEffectRef, transform);
            this.tag = "Arena";
        }

        public override void Effect()
        {
            if (!alive)
                return;
            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit[] hits = Physics.SphereCastAll(ray, explosionRadius);
            foreach (RaycastHit hit in hits)
            {
                Unit tmp;

                if ((tmp = hit.collider.GetComponent<Unit>()) != null && hit.collider.tag != ownerTag)
                {
                    alive = false;
                    tmp.GetComponent<UnitHealth>().TakeDamage(explosionDamage);
                }
            }
            alive = false;
            Destroy(charginEffect);
            explosionEffect = Instantiate(explosionEffectRef, transform);
            Destroy(GetComponentInChildren<Light>().gameObject, 0.1f);
            Invoke("Clean", 0.35f);
        }

        private void Clean()
        {
            Destroy(explosionEffect);
            Destroy(gameObject);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Arena")
                Effect();
        }
    }
}