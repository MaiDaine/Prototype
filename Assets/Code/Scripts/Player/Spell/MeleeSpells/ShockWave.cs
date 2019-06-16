using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine;

namespace Prototype
{
    public class ShockWave : MeleeSpell
    {
        public float spellRange;
        public float stunDuration;

        private SpeedStatus slowStatus = null;

        public override void Init(string tag, GameObject unit)
        {
            base.Init(tag, unit);
            spellIndicator.GetComponent<DecalProjectorComponent>().m_Size = new Vector3(spellRange * 2f, 4f, 1f);
            slowStatus = new SpeedStatus();
            slowStatus.speedModifier = -0.5f;
            slowStatus.duration = 0f;
            if (!slowStatus.Init(unit.GetComponent<Unit>()))
                slowStatus = null;
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
            StunStatus stun;
            Unit enemyUnit;
            foreach (RaycastHit hit in Physics.BoxCastAll(spellIndicator.transform.position, new Vector3(spellRange, 2f, 4f), Vector3.up))
                if (hit.collider.tag != this.tag && (enemyUnit = hit.collider.GetComponent<Unit>()) != null)
                {
                    stun = new StunStatus();
                    stun.duration = stunDuration;
                    stun.Init(enemyUnit);
                }
            if (slowStatus != null)
            {
                slowStatus.OnStatusEnd(unit.GetComponent<Unit>());
                slowStatus = null;
            }
            base.Launch();
            spellVisual = Instantiate(visualEffectRef, unit.transform);
            Invoke("Clean", 0.2f);
        }

        public override void Clean()
        {
            Destroy(spellVisual);
            Destroy(gameObject);
        }

        public override void Cancel()
        {
            if (spellIndicator != null)
                Destroy(spellIndicator);
            Destroy(gameObject);
        }
    }
}