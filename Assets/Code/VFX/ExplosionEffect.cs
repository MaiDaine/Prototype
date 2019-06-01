using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Prototype
{
    public class ExplosionEffect : MonoBehaviour
    {
        private void Start()
        {
            Invoke("StartExplosion", 1.5f);
        }

        private void StartExplosion()
        {
            GetComponent<VisualEffect>().SendEvent("OnExplosion");
        }
    }
}