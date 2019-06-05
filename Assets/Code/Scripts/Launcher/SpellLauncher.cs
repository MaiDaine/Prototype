using UnityEngine;

namespace Prototype
{
    public class SpellLauncher : MonoBehaviour
    {
        public LauncherBullet projectileRef;
        public float projectileDistance;
        public ObjectPool objectPool;
        public float tick;

        private bool active = false;
        private float timer = 0f;
        private LauncherBullet projectile;

        private void Awake()
        {
            projectileRef.timer = projectileDistance / projectileRef.speed;
        }

        private void Update()
        {
            if (active)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    projectile = objectPool.GetPooledObject().GetComponent<LauncherBullet>();
                    projectile.tag = tag;
                    projectile.transform.position = transform.position;
                    projectile.transform.position = new Vector3(projectile.transform.position.x, 1f, projectile.transform.position.z);
                    projectile.Initialize(transform.right);
                    projectile.gameObject.SetActive(true);
                    timer = tick;
                }
            }
        }

        public void OnEventReceived() { active = !active; }
    }
}