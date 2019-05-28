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
            launchOnRelease = true;
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
            Invoke("Effect", 2.5f);
        }

        public override void Effect()
        {
            base.Effect();
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
