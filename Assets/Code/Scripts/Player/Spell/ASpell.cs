using UnityEngine;

namespace Prototype
{
    public abstract class ASpell : MonoBehaviour
    {
        public enum CastType { Default, Channel, SmartCast, QuickCast };

        public CastType castType = CastType.Default;
        public GameObject spellIndicatorRef;
        public GameObject visualEffectRef;
        public float cooldown = 0f;
        public int spellPower;
        [HideInInspector]
        public bool useCursor = false;

        protected GameObject spellIndicator = null;
        protected GameObject spellVisual = null;

        public virtual void Init(string tag, GameObject unit)
        {
            this.tag = tag;
        }

        public virtual void Placement(Vector3 position) { }

        public virtual void Launch() { }

        public virtual void Clean() { }

        public virtual void Cancel()
        {
            if (spellIndicator != null)
                Destroy(spellIndicator);
            if (spellVisual != null)
                Destroy(spellVisual);
            Destroy(gameObject);
        }
    }
}