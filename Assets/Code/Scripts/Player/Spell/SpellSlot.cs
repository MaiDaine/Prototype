using System;
using UnityEngine;

namespace Prototype
{
    [Serializable]
    public struct SpellSlot
    {
        public ASpell spellRef;
        public Sprite spellIcone;
        [HideInInspector]
        public float spellCooldown;
    }
}