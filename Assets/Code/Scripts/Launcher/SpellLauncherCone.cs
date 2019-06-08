using UnityEngine;

namespace Prototype
{
    public class SpellLauncherCone : SpellLauncher
    {
        private readonly float[] coneAnglesX = new float[9] { -0.4f, -0.3f, -0.2f, -0.1f, 0f, 0.1f, 0.2f, 0.3f, 0.4f };
        private readonly float[] coneAnglesY = new float[9] { 0.6f, 0.7f, 0.8f, 0.9f, 1f, 0.9f, 0.8f, 0.7f, 0.6f };

        public override void Fire()
        {
            for (int i = 0; i < coneAnglesX.Length; i++)
            {
                projectile = objectPool.GetPooledObject().GetComponent<LauncherBullet>();
                projectile.tag = tag;
                projectile.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
                projectile.Initialize((coneAnglesX[i] * transform.right) + (coneAnglesY[i] * transform.forward));
                projectile.gameObject.SetActive(true);
            }
        }
    }
}