using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace Prototype
{
    public class MeleeLineBack : MeleeSpell
    {
        public float spellRange;
        public float jumpLenght;

        private RootStatus rootStatus = null;
        private PhaseStatus phaseStatus = null;
        private float jumpTimer = -1f;

        public override void Init(string tag, GameObject unit)
        {
            base.Init(tag, unit);
            spellIndicator.GetComponent<DecalProjectorComponent>().m_Size = new Vector3(spellRange * 2f, 4f, 1f);
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
            UnitHealth enemyUnit;
            foreach (RaycastHit hit in Physics.BoxCastAll(spellIndicator.transform.position, new Vector3(spellRange, 2f, 2f), Vector3.up))
                if (hit.collider.tag != this.tag && (enemyUnit = hit.collider.GetComponent<UnitHealth>()) != null)
                    enemyUnit.TakeDamage(spellPower);
            Destroy(spellIndicator);
            unit.GetComponent<Rigidbody>().AddForce(-unit.transform.forward * 50f, ForceMode.VelocityChange);
            rootStatus = new RootStatus();
            if (rootStatus.Init(unit.GetComponent<Unit>()))
                rootStatus = null;
            phaseStatus = new PhaseStatus();
            if (phaseStatus.Init(unit.GetComponent<Unit>()))
                phaseStatus = null;
            jumpTimer = jumpLenght;
        }

        protected override void Update()
        {
            if (jumpTimer > 0f)
            {
                jumpTimer -= Time.deltaTime;
                if (jumpTimer < 0f)
                {
                    if (rootStatus != null)
                    {
                        rootStatus.OnStatusEnd(unit.GetComponent<Unit>());
                        rootStatus = null;
                    }
                    if (phaseStatus != null)
                    {
                        phaseStatus.OnStatusEnd(unit.GetComponent<Unit>());
                        phaseStatus = null;
                    }
                    unit.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                    Clean();
                }
            }
        }

        public override void Clean()
        {
            Destroy(gameObject);
        }
    }
}