using UnityEngine;
using UnityEngine.AI;

namespace Prototype
{
    public class Blink : ASpell
    {
        public float distance = 10f;

        private GameObject unit;

        public override void Init(string tag, GameObject unit)
        {
            this.unit = unit;
        }

        public override void Launch(float _)
        {
            Effect();
        }

        public override void Effect()
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(unit.transform.position + distance * unit.transform.forward, out hit, 5f, NavMesh.AllAreas))
                unit.transform.position = new Vector3(hit.position.x, unit.transform.position.y, hit.position.z);
            else
            {
                NavMesh.FindClosestEdge(unit.transform.position + distance * unit.transform.forward, out hit, NavMesh.AllAreas);
                if (hit.position.x != Mathf.Infinity && hit.position.y != Mathf.Infinity)
                    unit.transform.position = new Vector3(hit.position.x, unit.transform.position.y, hit.position.z);
                else if (NavMesh.Raycast(unit.transform.position, unit.transform.position + distance * unit.transform.forward, out hit, NavMesh.AllAreas))
                {
                    NavMesh.FindClosestEdge(hit.position, out hit, NavMesh.AllAreas);
                    if (hit.position.x != Mathf.Infinity && hit.position.y != Mathf.Infinity)
                        unit.transform.position = new Vector3(hit.position.x, unit.transform.position.y, hit.position.z);
                }
            }
            base.Effect();
        }
    }
}
