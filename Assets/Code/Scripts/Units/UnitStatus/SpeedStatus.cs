namespace Prototype
{
    public class SpeedStatus : UnitStatus
    {
        public float speedModifier;

        public override bool Init(Unit unit)
        {
            unit.OnMoveSpeedChange(speedModifier);
            return base.Init(unit);
        }

        public override void OnStatusEnd(Unit unit)
        {
            unit.OnMoveSpeedChange(-speedModifier);
        }
    }
}
