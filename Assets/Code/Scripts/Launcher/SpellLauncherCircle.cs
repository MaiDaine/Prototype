using UnityEngine;

namespace Prototype
{
    public class SpellLauncherCircle : SpellLauncher
    {
        private Vector3 rotation = new Vector3(0, 10f, 0);
        private readonly float[] circleAnglesX = new float[8] { -1f, -0.5f, 0f, 0.5f, 1f, 0.5f, 0f, -0.5f };
        private readonly float[] circleAnglesY = new float[8] { 0f, 0.5f, 1f, 0.5f, 0f, -0.5f, -1f, -0.5f };

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
            for (int i = 0; i < circleAnglesX.Length; i++)
            {
                projectile = objectPool.GetPooledObject().GetComponent<LauncherBullet>();
                projectile.tag = tag;
                projectile.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
                projectile.Initialize((circleAnglesX[i] * transform.right) + (circleAnglesY[i] * transform.forward));
                projectile.gameObject.SetActive(true);
            }
        }
    }
}