using UnityEngine;

namespace Prototype
{
    public class MeleeArc : MeleeSpell
    {
        public Weapon weaponRef;
        public float animSpeed = 1f;

        private Weapon weapon;

        public override void Placement(Vector3 position)
        {
            spellIndicator.transform.position = new Vector3(unit.transform.position.x, 0.5f, unit.transform.position.z);

            Transform transform = spellIndicator.transform;
            transform.LookAt(unit.transform.position + unit.transform.forward);
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        public override void Launch()
        {
            spellIndicator.transform.eulerAngles = new Vector3(0f, (spellIndicator.transform.eulerAngles.y - 90f) % 360f, spellIndicator.transform.eulerAngles.z);
            weapon = Instantiate(weaponRef, unit.transform.position, spellIndicator.transform.rotation);
            weapon.transform.rotation = spellIndicator.transform.rotation;
            weapon.Init(spellPower, this.tag, unit.GetComponent<Unit>(), animSpeed);
            Invoke("Clean", animSpeed - 0.01f);
            base.Launch();
        }

        public override void Clean()
        {
            Destroy(weapon);
            Destroy(gameObject);
        }

        public override void Cancel()
        {
            if (spellIndicator != null)
                Destroy(spellIndicator);
            if (weapon != null)
                Destroy(weapon);
            Destroy(gameObject);
        }
    }
}