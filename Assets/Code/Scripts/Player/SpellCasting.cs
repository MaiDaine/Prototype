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
        private int currentSpellIndex = -1;
        private ASpell[] currentSpellBook;
        private bool[] spellLocked = new bool[4];

        public SpellCasting(PlayerController player) { this.player = player; }

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
                    StartCasting(index);
            }
            else
            {
                if (currentSpellIndex != index)
                {
                    CancelCast();
                    StartCasting(index);
                }
                else
                    LaunchSpell();
            }
        }

        public void SpellReleased(int index)
        {
            if (currentSpell == null)
                return;
            spellLocked[index] = false;
            if (currentSpell.launchOnRelease)
                LaunchSpell();
        }

        public void CancelCast()
        {
            if (currentSpell != null)
            {
                currentSpell.Cancel();
                spellLocked[currentSpellIndex] = false;
                currentSpellIndex = -1;
            }
            currentSpell = null;
            casting = false;
        }

        public void CastUpdate(Vector3 position)
        {
            currentSpell.Placement(position);
        }

        private void StartCasting(int index)
        {
            currentSpell = player.InstantiateSpell(ref currentSpellBook[index]);
            casting = true;
            spellLocked[index] = true;
            if (player.useJoyStick)
                player.ResetJoystickCursor();
            currentSpellIndex = index;
        }

        private void LaunchSpell()
        {
            currentSpell.Launch();
            currentSpell = null;
            casting = false;
            spellLocked[currentSpellIndex] = false;
            currentSpellIndex = -1;
        }
    }
}