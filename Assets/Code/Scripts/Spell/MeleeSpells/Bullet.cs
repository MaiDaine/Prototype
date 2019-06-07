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
        private Material material;
        private const int maxCharge = 3;
        private const float chargeTime = 0.5f;
        private const float maxChargeTime = (float)maxCharge * chargeTime;

        public override void Init(string tag, GameObject unit)
        {
            base.Init(tag, unit);
            spellIndicator.GetComponent<DecalProjectorComponent>().m_Size = new Vector3(spellRange * 2f, 2f, 1f);
            material = spellIndicator.GetComponent<DecalProjectorComponent>().m_Material;
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
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, -((transform.eulerAngles.z + 90f) % 360));
            spellIndicator.transform.rotation = transform.rotation;
        }

        public override void Launch()
        {
            Destroy(spellIndicator);
            spellIndicator = null;
            unit.GetComponent<UnitStatusManager>().UnRegisterStatus(rootStatus);
            rootStatus.OnDestroy(unit.GetComponent<Unit>());
            base.Launch();
            int projectileCount = (int)Mathf.Clamp(castTime / chargeTime, 1f, maxCharge);
            for (int i = 0; i < projectileCount; i++)
                Invoke("SpawnProjectile", (float)i * 0.1f);
            Invoke("Effect", (float)projectileCount * 0.1f);
        }

        protected override void Update()
        {
            base.Update();
            if (spellIndicator != null)
            {
                if (castTime <= maxChargeTime)
                    material.SetFloat("_ChargingPercent", Mathf.Clamp01((castTime % chargeTime) / chargeTime));
                else
                    material.SetFloat("_ChargingPercent", 1f);
            }
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