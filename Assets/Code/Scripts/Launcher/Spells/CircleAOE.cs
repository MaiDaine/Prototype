using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace Prototype
{
    public class CircleAOE : MonoBehaviour
    {
        public DecalProjectorComponent spellIndicatorRef;
        public float radius;
        public float timer;
        public int damage;

        private DecalProjectorComponent spellIndicator = null;
        private float charge;

        public void Start()
        {
            spellIndicator = Instantiate(spellIndicatorRef);
            spellIndicator.m_Material.enableInstancing = true;
            spellIndicator.transform.position = transform.position;
            spellIndicator.m_Size = new Vector3(radius, radius, 1);
            charge = 0f;
        }

        private void Update()
        {
            if (spellIndicator != null)
            {
                charge += Time.deltaTime;
                if (charge >= timer)
                {
                    Destroy(spellIndicator);
                    spellIndicator = null;
                    Effect();
                }
                else
                    spellIndicator.m_Material.SetFloat("_ChargingPercent", Mathf.Clamp01(charge / timer));
            }
        }

        private void Effect()
        {
            UnitHealth unit;
            foreach (RaycastHit hit in Physics.SphereCastAll(transform.position, radius, Vector3.up))
                if (hit.collider.tag == "PlayerTeam" && ((unit = hit.collider.GetComponent<UnitHealth>()) != null))
                    unit.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}