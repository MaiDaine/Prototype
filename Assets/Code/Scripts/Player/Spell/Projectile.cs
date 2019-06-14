using UnityEngine;

namespace Prototype
{
    public class Projectile : MonoBehaviour
    {
        public float timer;
        public float explosionRadius;
        public int explosionDamage;
        public float speed = 1f;

        protected Vector3 direction;
        protected string ownerTag;
        protected bool alive = true;

        public virtual void Initialize(Vector3 direction, string ownerTag)
        {
            this.direction = direction;
            this.ownerTag = ownerTag;
        }

        protected virtual void Update()
        {
            if (!alive)
                return;
            transform.position += direction * speed;
            timer -= Time.deltaTime;
            if (timer < 0)
                Effect();
        }

        public virtual void Effect()
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
            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.tag != ownerTag)
                Effect();
        }
    }
}