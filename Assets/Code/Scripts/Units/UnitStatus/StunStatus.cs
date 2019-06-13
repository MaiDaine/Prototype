namespace Prototype
{
    public class StunStatus : UnitStatus
    {
        public override bool Init(Unit unit)
        {
            unit.OnStunStart();
            return base.Init(unit);
        }

        public override void OnStatusEnd(Unit unit)
        {
            base.OnStatusEnd(unit);
            unit.OnStunEnd();
        }
    }
}