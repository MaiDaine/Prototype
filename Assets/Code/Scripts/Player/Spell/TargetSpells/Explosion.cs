using UnityEngine;

namespace Prototype
{
    public class Explosion : TargetSpell
    {
        public override void Effect()
        {
            base.Effect();
            Ray ray = new Ray(spellVisual.transform.position, spellVisual.transform.position + new Vector3(0, 0.1f, 0));
            RaycastHit[] hits = Physics.SphereCastAll(ray, 5.0f);
            Unit tmp;
            foreach (RaycastHit hit in hits)
                if (!hit.collider.isTrigger && (tmp = hit.collider.GetComponent<Unit>()) != null && hit.collider.tag != this.tag)
                    tmp.GetComponent<UnitHealth>().TakeDamage(spellPower);
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
            if (spellVisual != null)
                Destroy(spellVisual);
            Destroy(gameObject);
        }
    }
}