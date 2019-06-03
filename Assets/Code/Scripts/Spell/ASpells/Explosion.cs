using UnityEngine;

namespace Prototype
{
    public class Explosion : ASpell
    {
        public GameObject spellIndicatorRef;
        public GameObject visualEffectRef;

        private GameObject spellIndicator = null;
        private GameObject spellVisual = null;

        private void Awake()
        {
            spellIndicator = Instantiate(spellIndicatorRef);
        }

        public override void Placement(Vector3 position)
        {
            //base.Placement(position);
            spellIndicator.transform.position = new Vector3(position.x, 0.5f, position.z);
        }

        public override void Launch(float castTime)
        {
            base.Launch(castTime);
            spellVisual = Instantiate(visualEffectRef);
            spellVisual.transform.position = spellIndicator.transform.position;
            Destroy(spellIndicator);
        }

        public override void Effect()
        {
            base.Effect();
            Ray ray = new Ray(spellVisual.transform.position, spellVisual.transform.position + new Vector3(0, 0.1f, 0));
            RaycastHit[] hits = Physics.SphereCastAll(ray, 5.0f);
            Unit tmp;
            foreach (RaycastHit hit in hits)
                if (!hit.collider.isTrigger && (tmp = hit.collider.GetComponent<Unit>()) != null && hit.collider.tag != this.tag)
                    tmp.GetComponent<UnitHealth>().TakeDamage(spellDamage);
        }

        public override void Clean()
        {
            //base.AfterEffect();
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
            //base.Cancel();
        }
    }
}