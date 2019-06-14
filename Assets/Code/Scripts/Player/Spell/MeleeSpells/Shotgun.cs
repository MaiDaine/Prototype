using UnityEngine;

namespace Prototype
{
    public class Shotgun : MeleeSpell
    {
        public float radius = 5f;

        public override void Placement(Vector3 position)
        {
            spellIndicator.transform.position = new Vector3(unit.transform.position.x, 0.5f, unit.transform.position.z);

            Transform transform = spellIndicator.transform;
            transform.LookAt(unit.transform.position + unit.transform.forward);
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        public override void Launch()
        {
            Vector2 unitPos = new Vector2(unit.transform.position.x, unit.transform.position.z);
            Vector2 posForward = new Vector2(unitPos.x + unit.transform.forward.x, unitPos.y + +unit.transform.forward.z);
            Vector2 right = new Vector2(posForward.x + 2 * unit.transform.right.x, posForward.y + 2 * unit.transform.right.z);
            Vector2 left = new Vector2(posForward.x - 2 * unit.transform.right.x, posForward.y - 2 * unit.transform.right.z);
            UnitHealth tmpUnit;
            Vector2 tmpPos = new Vector2();

            foreach (RaycastHit hit in Physics.SphereCastAll(unit.transform.position, radius, Vector3.up))
                if ((tmpUnit = hit.collider.GetComponent<UnitHealth>()) != null)
                {
                    tmpPos.x = hit.transform.position.x;
                    tmpPos.y = hit.transform.position.z;
                    if (GetDelta(unitPos, right, tmpPos) < 0f && GetDelta(unitPos, left, tmpPos) > 0f)
                        tmpUnit.TakeDamage(spellPower);
                }
            base.Launch();
            Clean();
        }

        private float GetDelta(Vector2 a, Vector2 b, Vector2 p)
        {
            return ((p.x - a.x) * (b.y - a.y) - (p.y - a.y) * (b.x - a.x));
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