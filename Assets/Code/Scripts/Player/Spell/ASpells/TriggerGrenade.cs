using UnityEngine;

namespace Prototype
{
    public class TriggerGrenade : ASpell
    {
        public Projectile projectileRef;
        public float spellRange;

        private Projectile projectile = null;

        public override void Init(string tag, GameObject unit)
        {
            base.Init(tag, unit);
            projectile = Instantiate(projectileRef, this.transform);
            projectile.tag = tag;
            projectile.transform.position = new Vector3(unit.transform.position.x, 1f, unit.transform.position.z);
            projectile.Initialize(unit.transform.forward, tag);
        }

        public override void Placement(Vector3 position) { }

        public override void Launch()
        {
            projectile.Effect();
        }
    }
}