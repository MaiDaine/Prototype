using UnityEngine;

namespace Prototype
{
    public class Projectile : MonoBehaviour
    {
        public float timer;
        public float explosionRadius;
        public int explosionDamage;

        private Vector3 direction;
        private string ownerTag;
        private bool alive = true;

        public virtual void Initialize(Vector3 direction, string ownerTag)
        {
            this.direction = direction;
            this.ownerTag = ownerTag;
        }

        protected virtual void Update()
        {
            this.transform.position += direction;
            timer -= Time.deltaTime;
            if (timer < 0)
                Effect();
        }

        protected virtual void Effect()
        {
            if (!alive)
                return;
            Ray ray = new Ray(this.transform.position, this.transform.up);
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
            Destroy(this.gameObject);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.tag != ownerTag)
                Effect();
        }
    }
}