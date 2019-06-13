using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class PhaseStatus : UnitStatus
    {
        List<Collider> colliders = new List<Collider>();
        public override bool Init(Unit unit)
        {
            foreach(Collider collider in unit.GetComponents<Collider>())
                if (!collider.isTrigger)
                {
                    colliders.Add(collider);
                    collider.isTrigger = true;
                }
            return base.Init(unit);
        }

        public override void OnStatusEnd(Unit unit)
        {
            foreach (Collider collider in colliders)
                collider.isTrigger = false;
            colliders.Clear();
            colliders = null;
        }
    }
}