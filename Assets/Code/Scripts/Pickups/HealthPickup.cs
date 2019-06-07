using UnityEngine.Experimental.VFX;
using UnityEngine;

namespace Prototype
{
    public class HealthPickup : MonoBehaviour
    {
        public int healAmount = 20;

        public void ReActivate()
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<VisualEffect>().enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            UnitHealth health;
            if (other.tag == this.tag && (health = other.GetComponent<UnitHealth>()) != null && !health.IsFullLife())
            {
                health.Heal(healAmount);
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<VisualEffect>().enabled = false;
            }
        }
    }
}