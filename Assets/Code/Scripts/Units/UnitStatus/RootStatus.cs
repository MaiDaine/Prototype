namespace Prototype
{
    public class RootStatus : UnitStatus
    {
        public override bool Init(Unit unit)
        {
            unit.OnRootStart();
            return base.Init(unit);
        }

        public override void OnStatusEnd(Unit unit)
        {
            unit.OnRootEnd();
        }
    }
}