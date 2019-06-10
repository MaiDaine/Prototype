using System;

namespace Prototype
{
    [Serializable]
    public struct BossPhaseStatus
    {
        public UnitStatus.statusTypes type;
        public float power;
        public float duration;
    }

    [Serializable]
    public struct BossPhase
    {
        public BossPhaseStatus[] phaseStatus;
        public Launcher[] launchers;
        public int activeLaunchers;
        public GameEvent[] mapEvents;
    }
}