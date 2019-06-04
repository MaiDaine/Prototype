namespace Prototype
{
    public abstract class UnitStatus
    {
        public float duration;

        public virtual void Init(Unit unit)
        {
            if (duration > 0f)
                unit.GetComponent<UnitStatusManager>().RegisterTimedStatus(this);
            else
                unit.GetComponent<UnitStatusManager>().RegisterStatus(this);
        }

        public virtual void Update() { }

        public virtual void OnDestroy(Unit unit) { }
    }
}