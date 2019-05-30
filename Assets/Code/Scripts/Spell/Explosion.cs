using System.Collections.Generic;
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
            //launchOnRelease = true;
            spellIndicator = Instantiate(spellIndicatorRef);
        }

        public override void Placement(Vector3 position)
        {
            base.Placement(position);
            spellIndicator.transform.position = new Vector3(position.x, 0.5f, position.z);
        }

        public override void Launch()
        {
            base.Launch();
            spellVisual = Instantiate(visualEffectRef);
            spellVisual.transform.position = spellIndicator.transform.position;
            Destroy(spellIndicator);
        }

        public override void Effect()
        {
            base.Effect();
            Ray ray = new Ray(spellVisual.transform.position, spellVisual.transform.position + new Vector3(0, 0.1f, 0));
            RaycastHit[] hits = Physics.SphereCastAll(ray, 5.0f);
            foreach (RaycastHit hit in hits)
                if (!hit.collider.isTrigger && hit.collider.GetComponent<Unit>() && hit.collider.tag != this.tag)
                    Debug.Log(hit.collider.GetComponent<Unit>().name);
        }

        public override void AfterEffect()
        {
            base.AfterEffect();
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
            base.Cancel();
        }
    }
}