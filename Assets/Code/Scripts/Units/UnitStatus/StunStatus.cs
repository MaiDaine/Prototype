namespace Prototype
{
    public class StunStatus : UnitStatus
    {
        public override void Init(Unit unit)
        {
            base.Init(unit);
            unit.OnStunStart();
        }

        public override void OnStatusEnd(Unit unit)
        {
            base.OnStatusEnd(unit);
            unit.OnStunEnd();
        }
    }
}