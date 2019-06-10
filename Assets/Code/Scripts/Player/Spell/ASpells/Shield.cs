using UnityEngine;

namespace Prototype
{
    public class Shield : ASpell
    {
        public int shieldDuration;
        public float speedModifier = 0.25f;

        private GameObject unit;
        private SpeedStatus speedStatus;

        public override void Init(string tag, GameObject unit)
        {
            this.unit = unit;
        }

        public override void Launch()
        {
            unit.GetComponent<UnitHealthShield>().AddShield(spellPower, shieldDuration, Clean);
            speedStatus = new SpeedStatus();
            speedStatus.duration = 0f;
            speedStatus.speedModifier = speedModifier;
            speedStatus.Init(unit.gameObject.GetComponent<Unit>());
            //Instantiate(visualEffectRef, unit.transform);
            Debug.Log(spellVisual);//TMP
        }

        public override void Clean()
        {
            //Destroy(spellVisual);
            speedStatus.OnStatusEnd(unit.gameObject.GetComponent<Unit>());
            Destroy(gameObject);
        }
    }
}