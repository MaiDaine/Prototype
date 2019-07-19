using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class UnitPortrait : MonoBehaviour
    {
        [Serializable]
        public struct SpellSlot
        {
            public Image icone;
            public Image mask;
            public Text coolDown;
        }

        public SpellSlot[] spellSlots = new SpellSlot[4];
        public HealthShieldBarHandler healthBarHandler;
    }
}
