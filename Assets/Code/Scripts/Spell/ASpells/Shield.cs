using UnityEngine;

namespace Prototype
{
    public class Shield : ASpell
    {
        public GameObject visualEffectRef;
        public int shieldDuration;

        private GameObject spellVisual = null;
        private GameObject unit;

        public override void Init(string tag, GameObject unit)
        {
            this.unit = unit;
        }

        public override void Launch(float castTime)
        {
            unit.GetComponent<UnitHealthShield>().AddShield(spellPower, shieldDuration, Clean);
            //Instantiate(visualEffectRef, unit.transform);
            Debug.Log(spellVisual);//TMP
        }

        public override void Clean()
        {
            //base.Clean();
            //Destroy(spellVisual);
            Destroy(gameObject);
        }

    }
}
