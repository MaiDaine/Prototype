using UnityEngine;

namespace Prototype
{
    public class LauncherBullet : MonoBehaviour
    {
        public float timer;
        public float explosionRadius;
        public int explosionDamage;
        public float speed = 1f;

        private Vector3 direction;
        private bool alive;

        public virtual void Initialize(Vector3 direction)
        {
            this.direction = direction;
            alive = true;
        }

        protected virtual void Update()
        {
            this.transform.position += direction * speed;
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

                if ((tmp = hit.collider.GetComponent<Unit>()) != null && hit.collider.tag == "PlayerTeam")
                {
                    alive = false;
                    tmp.GetComponent<UnitHealth>().TakeDamage(explosionDamage);
                }
            }
            alive = false;
            gameObject.SetActive(false);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerTeam")
                Effect();
            else if (other.tag == "Arena")
                gameObject.SetActive(false);
        }
    }
}