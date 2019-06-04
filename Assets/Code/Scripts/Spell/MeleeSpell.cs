using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class MeleeSpell : ASpell
    {
        public new const bool useCursor = false;

        protected GameObject unit;
        protected float castTime;

        protected void Awake()
        {
            spellIndicator = Instantiate(spellIndicatorRef);
        }

        public override void Init(string tag, GameObject unit)
        {
            base.Init(tag, unit);
            this.unit = unit;
            spellIndicator.transform.position = new Vector3(spellIndicator.transform.position.x, 0.5f, spellIndicator.transform.position.z);
            Cursor.visible = false;
            castTime = 0f;
        }

        protected void Update()
        {
            castTime += Time.deltaTime;
        }

        public override void Launch()
        {
            Cursor.visible = true;
            Destroy(spellIndicator);
        }
    }
}
