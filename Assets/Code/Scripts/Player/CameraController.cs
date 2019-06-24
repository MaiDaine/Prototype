using UnityEngine;

namespace Prototype
{
    public class CameraController : MonoBehaviour
    {
        public float positionLerpTime = 0.5f;

        private CameraState targetCameraState = new CameraState();
        private CameraState interpolatingCameraState = new CameraState();
        private float zOffset = -5f;
        private Rigidbody target;

        public void UpdateTarget(GameObject target)
        {
            this.target = target.GetComponent<Rigidbody>();
        }

        private void LateUpdate()
        {
            targetCameraState.x = target.transform.position.x;
            targetCameraState.z = target.transform.position.z + zOffset;
            var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
            interpolatingCameraState.LerpTowards(targetCameraState, positionLerpPct);
            interpolatingCameraState.UpdateTransform(transform);
        }

        private void OnEnable()
        {
            targetCameraState.SetFromTransform(transform);
            interpolatingCameraState.SetFromTransform(transform);
        }
    }
}