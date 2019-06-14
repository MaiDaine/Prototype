using UnityEngine;

namespace Prototype
{
    public class Shield : ASpell
    {
        public int shieldDuration;
        public float speedModifier = 0.25f;

        private GameObject unit;
        private SpeedStatus speedStatus = null;
        private PhaseStatus phaseStatus = null;

        public override void Init(string tag, GameObject unit)
        {
            this.unit = unit;
        }

        public override void Launch()
        {
            unit.GetComponent<UnitHealthShield>().AddShield(spellPower, shieldDuration, Clean);

            phaseStatus = new PhaseStatus();
            phaseStatus.duration = shieldDuration;
            if (!phaseStatus.Init(unit.GetComponent<Unit>()))
                phaseStatus = null;

            speedStatus = new SpeedStatus();
            speedStatus.duration = 0f;
            speedStatus.speedModifier = speedModifier;
            if (!speedStatus.Init(unit.GetComponent<Unit>()))
                speedStatus = null;
            //Instantiate(visualEffectRef, unit.transform);
            Debug.Log(spellVisual);//TMP
        }

        public void OnShieldBreak() { }

        public override void Clean()
        {
            //Destroy(spellVisual);
            if (speedStatus != null) { }
                speedStatus.OnStatusEnd(unit.GetComponent<Unit>());
            if (phaseStatus != null)
            {
                phaseStatus.OnStatusEnd(unit.GetComponent<Unit>());
                unit.GetComponent<UnitStatusManager>().UnRegisterTimedStatus(phaseStatus);
            }
            Destroy(gameObject);
        }
    }
}