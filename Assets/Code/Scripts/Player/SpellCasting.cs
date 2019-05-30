﻿using UnityEngine;

namespace Prototype
{
    public class SpellCasting
    {
        public bool casting = false;

        private PlayerController player;
        private SpellSlot[] currentSpellBook;
        private int currentSpellIndex = -1;
        private ASpell currentSpell = null;

        public SpellCasting(PlayerController player) { this.player = player; }

        public void UpdateSpellBook(ref SpellSlot[] spellBook)
        {
            currentSpellBook = spellBook;
            CancelCast();
        }

        public void SpellPressed(int index)
        {
            if (!casting)
                StartCasting(index);
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
            if (currentSpell.launchOnRelease)
                LaunchSpell();
        }

        public void CancelCast()
        {
            if (currentSpell != null)
            {
                currentSpell.Cancel();
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
            if (currentSpellBook[index].spellCooldown > 0f)
                return;
            currentSpell = player.InstantiateSpell(ref currentSpellBook[index].spellRef);
            casting = true;
            if (player.useJoyStick)
                player.ResetJoystickCursor();
            currentSpellIndex = index;
        }

        private void LaunchSpell()
        {
            currentSpell.Launch();
            currentSpellBook[currentSpellIndex].spellCooldown = currentSpell.cooldown;
            currentSpell = null;
            casting = false;
            currentSpellIndex = -1;
        }
    }
}