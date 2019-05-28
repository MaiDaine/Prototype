using UnityEngine;

public abstract class ASpell : MonoBehaviour
{
    public bool launchOnRelease = false;

    public virtual bool Init() { return false; }

    public virtual void Placement(Vector3 position) { }

    public virtual void Launch() { }
    
    public virtual void Effect() { }

    public virtual void Cancel() { }
}
