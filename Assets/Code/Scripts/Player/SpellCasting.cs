using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class SpellCasting
    {
        public bool casting = false;

        private PlayerController player;
        private ASpell currentSpell = null;
        private ASpell[] currentSpellBook;
        private bool[] spellLocked = new bool[4];

        public SpellCasting(PlayerController player) { this.player = player; }

        public void CancelCast()
        {
            if (currentSpell != null)
                currentSpell.Cancel();
            currentSpell = null;
            casting = false;
            for (int i = 0; i < 4; i++)
                spellLocked[i] = false;
        }

        public void UpdateSpellBook(ref ASpell[] spellBook)
        {
            currentSpellBook = spellBook;
            CancelCast();
        }

        public void SpellPressed(int index)
        {
            if (!casting)
            {
                if (!spellLocked[index])
                {
                    currentSpell = player.InstantiateSpell(ref currentSpellBook[index]);
                    casting = true;
                    spellLocked[index] = true;
                    if (player.useJoyStick)
                        player.ResetJoystickCursor();
                }
            }
            else
                LaunchSpell();
        }

        public void SpellReleased(int index)
        {
            spellLocked[index] = false;
            if (currentSpell.launchOnRelease)
                LaunchSpell();
        }

        public void CastUpdate(Vector3 position)
        {
            currentSpell.Placement(position);
        }

        private void LaunchSpell()
        {
            currentSpell.Launch();
            currentSpell = null;
            casting = false;
        }
    }
}