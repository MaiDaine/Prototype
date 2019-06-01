using UnityEngine;

namespace Prototype
{
    public class KeyboardController
    {
        private RayCast rayCast;

        public KeyboardController(RayCast rayCast)
        {
            this.rayCast = rayCast;
        }

        public void MouseRotate(ref ControllableUnit currentUnit)
        {
            Vector3 tmp;

            rayCast.PlaneRayCast(out tmp);
            tmp.y = currentUnit.transform.position.y;
            currentUnit.transform.LookAt(tmp);
        }

        public void SpellUpdate(ref SpellCasting spellCasting)
        {
            if (Input.GetButtonDown("Spell 1"))
                spellCasting.SpellPressed(0);
            else if (Input.GetButtonUp("Spell 1"))
                spellCasting.SpellReleased(0);

            if (Input.GetButtonDown("Spell 2"))
                spellCasting.SpellPressed(1);
            else if (Input.GetButtonUp("Spell 2"))
                spellCasting.SpellReleased(1);

            if (Input.GetButtonDown("Spell 3"))
                spellCasting.SpellPressed(2);
            else if (Input.GetButtonUp("Spell 3"))
                spellCasting.SpellReleased(2);

            if (Input.GetButtonDown("Spell 4"))
                spellCasting.SpellPressed(3);
            else if (Input.GetButtonUp("Spell 4"))
                spellCasting.SpellReleased(3);

            if (spellCasting.casting)
                spellCasting.CastUpdate(rayCast.BoardRayCast());
        }

        public void OrderUpdate(ref ControllableUnit currentUnit)
        {
            if (Input.GetButtonDown("Order 1"))
                currentUnit.ChangeOrder(ControllableUnit.OrderType.None);
            if (Input.GetButtonDown("Order 2"))
                currentUnit.ChangeOrder(ControllableUnit.OrderType.Def);
            if (Input.GetButtonDown("Order 3"))
                currentUnit.ChangeOrder(ControllableUnit.OrderType.Atk);
            if (Input.GetButtonDown("Order 4"))
                currentUnit.ChangeOrder(ControllableUnit.OrderType.Reg);
        }
    }
}
