using UnityEngine;

namespace Prototype
{
    public class CameraController : MonoBehaviour
    {
        public GameObject target;
        public Vector3 offset = new Vector3(0f, 25f, -5f);
        public float positionLerpTime = 0.2f;

        private CameraState targetCameraState = new CameraState();
        private CameraState interpolatingCameraState = new CameraState();

        public void UpdateTarget(GameObject target)
        {
            this.target = target;
            targetCameraState.x = target.transform.position.x;
            targetCameraState.z = target.transform.position.z;
        }

        private void Update()
        {
            UpdateTarget(target);
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