using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class SpellLauncher : MonoBehaviour
    {
        public ASpell spell;
        public float tick;

        private bool active = false;
        private float timer = 0f;
        private GameObject launcher;

        private void Awake()
        {
            launcher = this.gameObject;
        }

        private void Update()
        {
            if (active)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    Bullet tmp = (Bullet)Instantiate(spell);
                    tmp.FromLauncher(tag, transform.position, transform.right);
                    timer = tick;
                }
            }
        }

        public void OnEventReceived() { active = !active; }
    }
}
