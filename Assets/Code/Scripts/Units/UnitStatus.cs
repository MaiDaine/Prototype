﻿namespace Prototype
{
    public abstract class UnitStatus
    {
        public enum statusTypes { None, Stun, Root, Speed };
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

        public static UnitStatus CreateFromType(statusTypes type, float duration, float power)
        {
            UnitStatus tmp = null;
            switch (type)
            {
                case statusTypes.Stun:
                    tmp = new StunStatus();
                    break;       
                case statusTypes.Root:
                    tmp = new RootStatus();
                    break;
                case statusTypes.Speed:
                    tmp = new SpeedStatus();
                    ((SpeedStatus)tmp).speedModifier = power;
                    break;
            }
            if (tmp != null)
                tmp.duration = duration;
            return tmp;
        }
    }
}