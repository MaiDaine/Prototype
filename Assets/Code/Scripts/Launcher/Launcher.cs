using UnityEngine;

namespace Prototype
{
    public class Launcher : MonoBehaviour
    {
        public GameObject projectileRef;
        public float tick;
        public bool active = false;

        protected float timer = 0f;

        protected virtual void Update()
        {
            if (active)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    Fire();
                    timer = tick;
                }
            }
        }

        public virtual void Fire() { }

        public void OnEventReceived() { active = !active; }
    }
}