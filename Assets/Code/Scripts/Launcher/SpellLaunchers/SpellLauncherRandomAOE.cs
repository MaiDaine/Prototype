using UnityEngine;

namespace Prototype
{
    public class SpellLauncherRandomAOE : Launcher
    {
        public Vector2 boundingStart;
        public Vector2 boundingEnd;

        public override void Fire()
        {
            Vector3 position = new Vector3(
                Random.Range(boundingStart.x, boundingEnd.x),
                0.5f,
                Random.Range(boundingStart.y, boundingEnd.y));
            Instantiate(projectileRef, position, transform.rotation);
        }
    }
}