using UnityEngine;

namespace Prototype
{
    public class WeaponTrigger : MonoBehaviour
    {
        private Weapon weapon;

        private void Awake()
        {
            weapon = GetComponentInParent<Weapon>();
            if (weapon == null)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            weapon.OnTriggerEnter(other);
        }

    }
}
