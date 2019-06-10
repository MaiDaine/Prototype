using UnityEngine;

namespace Prototype
{
    public class BulletLauncher : Launcher
    {
        public float projectileDistance;
        public ObjectPool objectPool;

        protected LauncherBullet projectile;
        protected float projectileTimer;

        protected virtual void Awake()
        {

            projectileTimer = projectileDistance / projectileRef.GetComponent<LauncherBullet>().speed;
        }

        public override void Fire()
        {
            projectile = objectPool.GetPooledObject().GetComponent<LauncherBullet>();
            projectile.tag = tag;
            projectile.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            projectile.timer = projectileTimer;
            projectile.Initialize(transform.right);
            projectile.gameObject.SetActive(true);
        }
    }
}