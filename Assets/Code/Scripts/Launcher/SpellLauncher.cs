﻿using UnityEngine;

namespace Prototype
{
    public class SpellLauncher : MonoBehaviour
    {
        public LauncherBullet projectileRef;
        public float projectileDistance;
        public ObjectPool objectPool;
        public float tick;
        public bool active = false;

        protected float timer = 0f;
        protected LauncherBullet projectile;

        protected void Awake()
        {
            projectileRef.timer = projectileDistance / projectileRef.speed;
        }

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

        public virtual void Fire()
        {
            projectile = objectPool.GetPooledObject().GetComponent<LauncherBullet>();
            projectile.tag = tag;
            projectile.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            projectile.Initialize(transform.right);
            projectile.gameObject.SetActive(true);
        }

        public void OnEventReceived() { active = !active; }
    }
}