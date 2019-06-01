using UnityEngine;

namespace Prototype
{
    public abstract class ASpell : MonoBehaviour
    {
        public bool launchOnRelease = false;
        public bool useCursor = false;
        public float delayToEffect = 0f;
        public float delayToClean = 0f;
        public float cooldown = 0f;

        public virtual bool Init(string tag, ref Unit unit)
        {
            this.tag = tag;
            Cursor.visible = false;
            return false;
        }

        public virtual void Placement(Vector3 position) { }

        public virtual void Launch()
        {
            Cursor.visible = true;
            Invoke("Effect", delayToEffect);
        }

        public virtual void Effect()
        {
            if (delayToClean >= 0f)
                Invoke("Clean", delayToClean);
        }

        public virtual void Clean() { }

        public virtual void Cancel() { }
    }
}