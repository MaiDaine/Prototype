using UnityEngine;

public class RayCast : MonoBehaviour
{
    private Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    public bool BoardRayCast(out Vector3 position, int layer = 0)
    {
        float rayDistance;
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (this.groundPlane.Raycast(ray, out rayDistance)
           && Physics.Raycast(ray, out hit, rayDistance, layer))
        {
            position = ray.GetPoint(rayDistance);
            return true;
        }
        position = ray.GetPoint(rayDistance);
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
            return true;
        }
        position = ray.GetPoint(rayDistance);
        return false;
    }
}
