using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class BulletSpellLauncher : SpellLauncher
    {
        public new LauncherBullet projectileRef;
        public float projectileDistance;
        public ObjectPool objectPool;

        protected LauncherBullet projectile;

        protected virtual void Awake()
        {
            projectileRef.timer = projectileDistance / projectileRef.speed;
        }

        public override void Fire()
        {
            projectile = objectPool.GetPooledObject().GetComponent<LauncherBullet>();
            projectile.tag = tag;
            projectile.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            projectile.Initialize(transform.right);
            projectile.gameObject.SetActive(true);
        }
    }
}
