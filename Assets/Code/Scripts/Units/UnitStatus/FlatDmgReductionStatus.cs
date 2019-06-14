using System;

namespace Prototype
{
    public class FlatDmgReductionStatus : UnitStatus
    {
        public int reduction;

        private Func<int, int> callback;

        public override bool Init(Unit unit)
        {
            DmgReductionStatus tmp = unit.GetComponent<DmgReductionStatus>();

            if (tmp != null && tmp.reduction > this.reduction)
                return false;
            callback = new Func<int, int>(DamageOverride);
            unit.GetComponent<UnitHealth>().flatCallback = callback;
            return base.Init(unit);
        }

        public int DamageOverride(int amount) { return (amount - reduction); }

        public override void OnStatusEnd(Unit unit)
        {
            unit.GetComponent<UnitHealth>().percentCallback = null;
        }
    }
}