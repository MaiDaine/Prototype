using UnityEngine;

namespace Prototype
{
    public class Bullet : ASpell
    {
        public GameObject spellIndicatorRef;
        public Projectile visualEffectRef;
        public int projectileNumber;
        public float projectileDamage;
        public float spellDistance;

        private GameObject spellIndicator = null;
        private Projectile spellVisual = null;
        private Unit unit;

        private void Awake()
        {
            spellIndicator = Instantiate(spellIndicatorRef);
            spellIndicator.transform.position = new Vector3(spellIndicator.transform.position.x, 0.5f, spellIndicator.transform.position.z);
        }

        public override bool Init(string tag, ref Unit unit)
        {
            this.unit = unit;
            return base.Init(tag, ref unit);
        }

        public override void Placement(Vector3 position)
        {
            Vector3 tmp = unit.transform.position + spellDistance * unit.transform.forward;
            tmp.y = 0.5f;
            spellIndicator.transform.position = tmp;

            Transform transform = spellIndicator.transform;
            transform.LookAt(unit.transform);
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, (transform.eulerAngles.z + 90f) % 360);

            spellIndicator.transform.rotation = transform.rotation;
            //base.Placement(position);
        }

        public override void Launch()
        {
            base.Launch();
            spellVisual = Instantiate(visualEffectRef);
            spellVisual.transform.position = unit.transform.position;
            spellVisual.transform.position = new Vector3(spellVisual.transform.position.x, 1f, spellVisual.transform.position.z);
            spellVisual.Initialize(unit.transform.forward, unit.tag);
            Destroy(spellIndicator);
        }

        public override void Effect()
        {
            Destroy(gameObject);
            //base.Effect();
        }

        public override void AfterEffect()
        {
            //base.AfterEffect();
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
