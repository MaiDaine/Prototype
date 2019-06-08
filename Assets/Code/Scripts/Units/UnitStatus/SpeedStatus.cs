﻿namespace Prototype
{
    public class SpeedStatus : UnitStatus
    {
        public float speedModifier;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            unit.OnMoveSpeedChange(speedModifier);
        }

        public override void OnDestroy(Unit unit)
        {
            unit.OnMoveSpeedChange(-speedModifier);
        }
    }
}