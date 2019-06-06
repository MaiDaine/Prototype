using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine;

namespace Prototype
{
    public class Bullet : MeleeSpell
    {
        public Projectile projectileRef;
        public float spellRange;

        private Projectile projectile = null;
        private RootStatus rootStatus;

        public override void Init(string tag, GameObject unit)
        {
            base.Init(tag, unit);
            spellIndicator.GetComponent<DecalProjectorComponent>().m_Size = new Vector3(spellRange * 2f, 2f, 1f);
            rootStatus = new RootStatus();
            rootStatus.Init(unit.GetComponent<Unit>());
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
        }

        public override void Launch()
        {
            Destroy(spellIndicator);
            unit.GetComponent<UnitStatusManager>().UnRegisterStatus(rootStatus);
            rootStatus.OnDestroy(unit.GetComponent<Unit>());
            if (castTime > 0.5)
            {
                ChanneledLaunch();
                return;
            }
            base.Launch();
            CreateProjectile(this.tag, unit.transform.position, unit.transform.forward);
            Effect();
        }

        private void ChanneledLaunch()
        {
            Cursor.visible = true;
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

        public void Effect()//TMP LAUNCHER
        {
            Destroy(gameObject);
        }

        public override void Cancel()
        {
            unit.GetComponent<UnitStatusManager>().UnRegisterStatus(rootStatus);
            rootStatus.OnDestroy(unit.GetComponent<Unit>());
            if (spellIndicator != null)
                Destroy(spellIndicator);
            if (projectile != null)
                Destroy(projectile);
            Destroy(gameObject);
        }
    }
}