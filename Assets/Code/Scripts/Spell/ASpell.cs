using UnityEngine;

public abstract class ASpell : MonoBehaviour
{
    public bool launchOnRelease = false;
    public float delayToEffect = 0f;
    public float delayToAfterEffect = 0f;
    public float cooldown = 0f;

    public virtual bool Init(string tag)
    {
        this.tag = tag;
        return false;
    }

    public virtual void Placement(Vector3 position) { }

    public virtual void Launch()
    {
        Invoke("Effect", delayToEffect);
    }
    
    public virtual void Effect()
    {
        if (delayToAfterEffect >= 0f)
            Invoke("AfterEffect", delayToAfterEffect);
    }

    public virtual void AfterEffect() { }

    public virtual void Cancel() { }
}
