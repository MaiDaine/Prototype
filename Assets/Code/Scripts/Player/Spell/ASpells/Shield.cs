using UnityEngine;

namespace Prototype
{
    public class Shield : ASpell
    {
        public int shieldDuration;
        public float speedModifier = 0.25f;

        private GameObject unit;
        private SpeedStatus speedStatus;
        private PhaseStatus phaseStatus;

        public override void Init(string tag, GameObject unit)
        {
            this.unit = unit;
        }

        public override void Launch()
        {
            unit.GetComponent<UnitHealthShield>().AddShield(spellPower, shieldDuration, Clean);

            phaseStatus = new PhaseStatus();
            phaseStatus.duration = shieldDuration;
            phaseStatus.Init(unit.GetComponent<Unit>());

            speedStatus = new SpeedStatus();
            speedStatus.duration = 0f;
            speedStatus.speedModifier = speedModifier;
            speedStatus.Init(unit.GetComponent<Unit>());
            //Instantiate(visualEffectRef, unit.transform);
            Debug.Log(spellVisual);//TMP
        }

        public override void Clean()
        {
            //Destroy(spellVisual);
            speedStatus.OnStatusEnd(unit.GetComponent<Unit>());
            phaseStatus.OnStatusEnd(unit.GetComponent<Unit>());
            Destroy(gameObject);
        }
    }
}