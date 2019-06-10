using UnityEngine;

namespace Prototype
{
    public abstract class TargetSpell : ASpell
    {
        public float delayToEffect = 0f;

        public new const bool useCursor = true;

        protected void Awake()
        {
            spellIndicator = Instantiate(spellIndicatorRef);
        }

        public override void Init(string tag, GameObject unit)
        {
            base.Init(tag, unit);
            Cursor.visible = false;
        }

        public override void Placement(Vector3 position)
        {
            spellIndicator.transform.position = new Vector3(position.x, 0.5f, position.z);
        }

        public override void Launch()
        {
            Cursor.visible = true;
            spellVisual = Instantiate(visualEffectRef);
            spellVisual.transform.position = spellIndicator.transform.position;
            Destroy(spellIndicator);
            if (delayToEffect == 0f)
                Effect();
            else
                Invoke("Effect", delayToEffect);
        }

        public virtual void Effect() { }
    }
}
