using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class MeleeArc : ASpell
    {
        public GameObject spellIndicatorRef;
        public Weapon weaponRef;
        public float animSpeed = 1f;

        private GameObject spellIndicator;
        private Weapon weapon;
        private GameObject unit;

        public override void Init(string tag, GameObject unit)
        {
            this.unit = unit;
            this.tag = tag;
            spellIndicator = Instantiate(spellIndicatorRef);
            spellIndicator.transform.position = new Vector3(spellIndicator.transform.position.x, 0.5f, spellIndicator.transform.position.z);
            base.Init(tag, unit);
        }

        public override void Placement(Vector3 position)
        {
            Vector3 tmp = unit.transform.position;
            tmp.y = 0.5f;
            spellIndicator.transform.position = tmp;

            Transform transform = spellIndicator.transform;
            transform.LookAt(unit.transform.position + unit.transform.forward);
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z);
            //base.Placement(position);
        }

        public override void Launch(float castTime)
        {
            Cursor.visible = true;
            spellIndicator.transform.eulerAngles = new Vector3(0f, (spellIndicator.transform.eulerAngles.y - 90f) % 360f, spellIndicator.transform.eulerAngles.z);
            weapon = Instantiate(weaponRef, unit.transform.position, spellIndicator.transform.rotation);
            weapon.transform.rotation = spellIndicator.transform.rotation;
            weapon.Init(spellDamage, this.tag, unit.GetComponent<Unit>(), animSpeed);
            Destroy(spellIndicator);
            Invoke("Clean", animSpeed - 0.01f);
        }

        public override void Clean()
        {
            Destroy(weapon);
            Destroy(gameObject);
            //base.Clean();
        }

        public override void Cancel()
        {
            if (spellIndicator != null)
                Destroy(spellIndicator);
            if (weapon != null)
                Destroy(weapon);
            Destroy(gameObject);
            //base.Cancel();
        }
    }
}
