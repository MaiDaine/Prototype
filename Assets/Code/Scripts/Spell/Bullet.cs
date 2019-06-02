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
        private GameObject unit;

        public override void Init(string tag, GameObject unit)
        {
            this.unit = unit;
            spellIndicator = Instantiate(spellIndicatorRef);
            spellIndicator.transform.position = new Vector3(spellIndicator.transform.position.x, 0.5f, spellIndicator.transform.position.z);
            ControllableUnit tmp = unit.GetComponent<ControllableUnit>();
            if (tmp != null)
                tmp.canMove = false;
            base.Init(tag, unit);
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

        public override void Launch(float castTime)
        {
            if (castTime > 0.5)
            {
                ChanneledLaunch();
                return;
            }
            base.Launch(castTime);
            CreateProjectile(this.tag, unit.transform.position, unit.transform.forward);
        }

        private void ChanneledLaunch()
        {
            SpawnProjectile();
            Invoke("SpawnProjectile", 0.1f);
            Invoke("SpawnProjectile", 0.2f);
            Invoke("Effect", 0.2f);
        }

        private void SpawnProjectile()
        {
            CreateProjectile(this.tag, unit.transform.position, unit.transform.forward);
        }

        public void CreateProjectile(string tag, Vector3 position, Vector3 direction)
        {
            projectile = Instantiate(projectileRef);
            projectile.tag = tag;
            projectile.transform.position = position;
            projectile.transform.position = new Vector3(projectile.transform.position.x, 1f, projectile.transform.position.z);
            projectile.Initialize(direction, tag);
        }

        public override void Effect()
        {
            if (unit != null)//tmp
            {
                ControllableUnit tmp = unit.GetComponent<ControllableUnit>();
                if (tmp != null)
                    tmp.canMove = true;
            }
            Destroy(spellIndicator);
            Destroy(gameObject);
            //base.Effect();
        }

        public override void Clean()
        {
            //base.AfterEffect();
        }

        public override void Cancel()
        {
            ControllableUnit tmp = unit.GetComponent<ControllableUnit>();
            if (tmp != null)
                tmp.canMove = true;
            if (spellIndicator != null)
                Destroy(spellIndicator);
            if (projectile != null)
                Destroy(projectile);
            Destroy(gameObject);
            //base.Cancel();
        }
    }
}