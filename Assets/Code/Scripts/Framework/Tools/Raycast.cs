using UnityEngine;

namespace Prototype
{
    public class RayCast : MonoBehaviour
    {
        private Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        private Vector3 lastValidBordPos = new Vector3(0f, 0f, 0f);

        public bool PlaneRayCast(out Vector3 position)
        {
            float rayDistance;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (this.groundPlane.Raycast(ray, out rayDistance))
            {
                position = ray.GetPoint(rayDistance);
                return true;
            }
            position = ray.GetPoint(rayDistance);
            return false;
        }

        public Vector3 BoardRayCast()
        {
            float rayDistance;
            RaycastHit hit;
            Vector3 position;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (this.groundPlane.Raycast(ray, out rayDistance)
               && Physics.Raycast(ray, out hit, rayDistance))
            {
                position = ray.GetPoint(rayDistance);
                lastValidBordPos = position;
                return lastValidBordPos;
            }
            position = lastValidBordPos;
            return lastValidBordPos;
        }

        public bool BoardRayCast(out Vector3 position, int layer = 0)
        {
            float rayDistance;
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (this.groundPlane.Raycast(ray, out rayDistance)
               && Physics.Raycast(ray, out hit, rayDistance, layer))
            {
                position = ray.GetPoint(rayDistance);
                lastValidBordPos = position;
                return true;
            }
            position = lastValidBordPos;
            return false;
        }

        public bool BoardRayCast(out Vector3 position, string tag, int layer = 0)
        {
            float rayDistance;
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (this.groundPlane.Raycast(ray, out rayDistance)
               && Physics.Raycast(ray, out hit, rayDistance, layer) && hit.collider.tag == tag)
            {
                position = ray.GetPoint(rayDistance);
                lastValidBordPos = position;
                return true;
            }
            position = lastValidBordPos;
            return false;
        }
    }
}