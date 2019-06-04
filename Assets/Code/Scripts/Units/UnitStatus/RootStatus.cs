namespace Prototype
{
    public class RootStatus : UnitStatus
    {
        public override void Init(Unit unit)
        {
            base.Init(unit);
            unit.OnRootStart();
        }

        public override void OnDestroy(Unit unit)
        {
            unit.OnRootEnd();
        }
    }
}