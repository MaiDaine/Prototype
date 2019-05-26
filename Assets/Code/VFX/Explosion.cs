using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Invoke("StartExplosion", 1.5f);
    }

    private void StartExplosion()
    {
        GetComponent<VisualEffect>().SendEvent("OnExplosion");
        Invoke("EndExplosion", 1f);
    }

    private void EndExplosion()
    {
        this.gameObject.SetActive(false);
    }
}
