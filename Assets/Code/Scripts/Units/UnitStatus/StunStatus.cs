namespace Prototype
{
    public class StunStatus : UnitStatus
    {
        public override void Init(Unit unit)
        {
            base.Init(unit);
            unit.OnStunStart();
        }

        public override void OnDestroy(Unit unit)
        {
            base.OnDestroy(unit);
            unit.OnStunEnd();
        }
    }
}