using UnityEngine;

namespace Prototype
{
    public abstract class UnitStatus : MonoBehaviour
    {
        public float duration;
        public const bool canStack = false;

        public virtual void Init(Unit unit) { }

        protected virtual void Update()
        {
            duration -= Time.deltaTime;
            if (duration <= 0f)
                Destroy(this);
        }
    }
}