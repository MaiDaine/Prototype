using UnityEngine;

namespace Prototype
{
    public class SpellLauncherDoubleCurve : BulletSpellLauncher
    {
        private Vector3 rotation = new Vector3(0, 5f, 0);

        protected override void Update()
        {
            if (active)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    Fire();
                    timer = tick;
                }
                transform.Rotate(rotation);
            }
        }

        public override void Fire()
        {
            projectile = objectPool.GetPooledObject().GetComponent<LauncherBullet>();
            projectile.tag = tag;
            projectile.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            projectile.Initialize(transform.right);
            projectile.gameObject.SetActive(true);

            projectile = objectPool.GetPooledObject().GetComponent<LauncherBullet>();
            projectile.tag = tag;
            projectile.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            projectile.Initialize(-transform.right);
            projectile.gameObject.SetActive(true);
        }
    }
}