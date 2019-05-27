using UnityEngine;

namespace Prototype
{
    class CameraState
    {
        public float x;
        public float y;
        public float z;

        public void SetFromTransform(Transform t)
        {
            x = t.position.x;
            y = t.position.y;
            z = t.position.z;
        }

        public void LerpTowards(CameraState target, float positionLerpPct)
        {
            x = Mathf.Lerp(x, target.x, positionLerpPct);
            y = Mathf.Lerp(y, target.y, positionLerpPct);
            z = Mathf.Lerp(z, target.z, positionLerpPct);
        }

        public void UpdateTransform(Transform t)
        {
            t.position = new Vector3(x, y, z);
        }
    }
}
