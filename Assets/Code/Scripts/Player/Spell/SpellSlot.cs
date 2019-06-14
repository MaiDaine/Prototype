using System;
using UnityEngine;

namespace Prototype
{
    [Serializable]
    public struct SpellSlot
    {
        public ASpell spellRef;
        [HideInInspector]
        public float spellCooldown;
    }
}