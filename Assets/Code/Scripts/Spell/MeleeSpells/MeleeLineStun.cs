using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine;

namespace Prototype
{
    public class MeleeLineStun : MeleeSpell
    {
        public float spellRange;
        public float stunDuration;

        public override void Init(string tag, GameObject unit)
        {
            base.Init(tag, unit);
            spellIndicator.GetComponent<DecalProjectorComponent>().m_Size = new Vector3(spellRange * 2f, 4f, 1f);//TODO DECAL
            //TODO STATUS SLOW ON PLAYER
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
                    stun = enemyUnit.gameObject.AddComponent<StunStatus>();
                    stun.duration = stunDuration;
                    stun.Init(enemyUnit);
                }
            base.Launch();
            Clean();
        }

        public override void Clean()
        {
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
