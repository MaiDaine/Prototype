using UnityEngine;

namespace Prototype
{
    public class Shield : ASpell
    {
        public int shieldDuration;

        private GameObject unit;

        public override void Init(string tag, GameObject unit)
        {
            this.unit = unit;
        }

        public override void Launch()
        {
            unit.GetComponent<UnitHealthShield>().AddShield(spellPower, shieldDuration, Clean);
            //Instantiate(visualEffectRef, unit.transform);
            Debug.Log(spellVisual);//TMP
        }

        public override void Clean()
        {
            //Destroy(spellVisual);
            Destroy(gameObject);
        }
    }
}