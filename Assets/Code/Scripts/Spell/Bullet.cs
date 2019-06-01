using UnityEngine;

namespace Prototype
{
    public class Bullet : ASpell
    {
        public GameObject spellIndicatorRef;
        public Projectile projectileRef;
        public float spellRange;

        private GameObject spellIndicator = null;
        private Projectile projectile = null;
        private Unit unit;

        private void Awake()
        {
            spellIndicator = Instantiate(spellIndicatorRef);
            spellIndicator.transform.position = new Vector3(spellIndicator.transform.position.x, 0.5f, spellIndicator.transform.position.z);
        }

        public override bool Init(string tag, ref Unit unit)
        {
            this.unit = unit;
            return base.Init(tag, ref unit);
        }

        public override void Placement(Vector3 position)
        {
            Vector3 tmp = unit.transform.position + spellRange * unit.transform.forward;
            tmp.y = 0.5f;
            spellIndicator.transform.position = tmp;

            Transform transform = spellIndicator.transform;
            transform.LookAt(unit.transform);
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, (transform.eulerAngles.z + 90f) % 360);

            spellIndicator.transform.rotation = transform.rotation;
            //base.Placement(position);
        }

        public override void Launch()
        {
            base.Launch();
            projectile = Instantiate(projectileRef);
            projectile.transform.position = unit.transform.position;
            projectile.transform.position = new Vector3(projectile.transform.position.x, 1f, projectile.transform.position.z);
            projectile.Initialize(unit.transform.forward, unit.tag);
            Destroy(spellIndicator);
        }

        public override void Effect()
        {
            Destroy(gameObject);
            //base.Effect();
        }

        public override void Clean()
        {
            //base.AfterEffect();
        }

        public override void Cancel()
        {
            if (spellIndicator != null)
                Destroy(spellIndicator);
            if (projectile != null)
                Destroy(projectile);
            Destroy(gameObject);
            //base.Cancel();
        }
    }
}
