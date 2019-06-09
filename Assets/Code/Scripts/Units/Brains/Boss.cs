using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Brains/Units/Boss")]
    public class Boss : UnitBrain
    {
        public BossPhase[] phases;
        public int[] threshold;

        private int phase = 0;
        private int thresholdIndex;

        private List<SpellLauncher> activeLaunchers = new List<SpellLauncher>();
        private List<UnitStatus> activeStatus = new List<UnitStatus>();
        private Boss bossRef;

        public override void Initialize(UnitBrain brainRef, NonControllableUnit unit, Unit enemyHero)
        {
            base.Initialize(brainRef, unit, enemyHero);
            currentTarget = enemyHero;

            bossRef = ((Boss)brainRef);
            threshold = new int[bossRef.threshold.Length];
            for (int i = 0; i < threshold.Length; i++)
                threshold[i] = unit.currentStats.health * bossRef.threshold[i] / 100;
            thresholdIndex = 0;
            PhaseChange();
        }

        public override void Think(Unit unit)
        {
            if (unit.unitHealth.currentHealth < threshold[thresholdIndex])
            {
                thresholdIndex++;
                phase++;
                PhaseChange();
            }
            if (currentTarget == null || !currentTarget.isActiveAndEnabled)
                currentTarget = EncounterController.instance.activeHero;
            if (Vector3.Distance(unit.transform.position, currentTarget.transform.position) <= unit.currentStats.atkRange)
                unitMovement.StopMovement();
            else
                unitMovement.SetAgentDestination(currentTarget.transform.position);
        }

        private void PhaseChange()
        {
            foreach (SpellLauncher launcher in activeLaunchers)
            {
                launcher.active = false;
                Destroy(launcher);
            }
            activeLaunchers.Clear();

            foreach (UnitStatus status in activeStatus)
                status.OnDestroy(unit);
            activeStatus.Clear();

            BossPhase currentPhase = bossRef.phases[phase];
            int end = currentPhase.launchers.Length - currentPhase.activeLaunchers;
            int start = Random.Range(0, end);
            SpellLauncher tmpLauncher;
            BulletSpellLauncher bulletLauncher;
            for (int i = 0; i < currentPhase.activeLaunchers; i++)
            {
                tmpLauncher = Instantiate(currentPhase.launchers[i], unit.transform);
                activeLaunchers.Add(tmpLauncher);
                if ((bulletLauncher = tmpLauncher.gameObject.GetComponent<BulletSpellLauncher>()) != null)
                    bulletLauncher.objectPool = EncounterController.instance.bulletPool;
                tmpLauncher.active = true;
            }

            foreach (GameEvent gameEvent in currentPhase.mapEvents)
                gameEvent.Raise();

            UnitStatus tmpStatus;
            foreach (BossPhaseStatus status in currentPhase.phaseStatus)
            {
                tmpStatus = UnitStatus.CreateFromType(status.type, status.duration, status.power);
                activeStatus.Add(tmpStatus);
                tmpStatus.Init(unit);
            }
        }
    }
}