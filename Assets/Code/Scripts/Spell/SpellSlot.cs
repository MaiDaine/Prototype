using System;

namespace Prototype
{
    [Serializable]
    public struct SpellSlot
    {
        public ASpell spellRef;
        public float spellCooldown;
    }
}
