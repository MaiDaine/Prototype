namespace Prototype
{
    public class StunStatus : UnitStatus
    {
        public override void Init(Unit unit)
        {
            unit.OnStunStart();
        }

        private void OnDestroy()
        {
            Unit unit = GetComponent<Unit>();
            if (unit != null)
                unit.OnStunEnd();
        }
    }
}