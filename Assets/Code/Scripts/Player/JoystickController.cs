using UnityEngine;

namespace Prototype
{
    public class JoystickController
    {
        public float joystickSensitivity = 30f;//TODO userPref

        private bool[] fireAxis = new bool[2] { false, false };

        public JoystickController()
        {
        }

        public void UpdateJoystickCursor(ref Vector3 joystickCursor)
        {
            joystickCursor.x += Input.GetAxis("Rotation X") * Time.deltaTime * joystickSensitivity;
            joystickCursor.z += Input.GetAxis("Rotation Y") * Time.deltaTime * joystickSensitivity;
        }

        public void RotateTowardCursor(ref Vector3 joystickCursor, ref ControllableUnit currentUnit)
        {
            float tmp = joystickCursor.y;

            joystickCursor.y = currentUnit.transform.position.y;
            currentUnit.transform.LookAt(joystickCursor);
            joystickCursor.y = tmp;
        }

        public void StickRotate(ref ControllableUnit currentUnit)
        {
            if (Input.GetAxis("Rotation X") != 0f || Input.GetAxis("Rotation Y") != 0f)
                currentUnit.transform.eulerAngles = new Vector3(
                    currentUnit.transform.eulerAngles.x,
                    Mathf.Atan2(Input.GetAxis("Rotation X"), Input.GetAxis("Rotation Y")) * Mathf.Rad2Deg,
                    currentUnit.transform.eulerAngles.z);
        }

        public void SpellUpdate(SpellCasting spellCasting)
        {
            bool tmp = Input.GetAxisRaw("Spell 1") > 0f;
            if (tmp != fireAxis[0])
            {
                if (tmp)
                    spellCasting.SpellPressed(0);
                else
                    spellCasting.SpellReleased(0);
            }
            fireAxis[0] = tmp;

            tmp = Input.GetAxisRaw("Spell 2") < 0f;
            if (tmp != fireAxis[1])
            {
                if (tmp)
                    spellCasting.SpellPressed(1);
                else
                    spellCasting.SpellReleased(1);
            }
            fireAxis[1] = tmp;

            if (Input.GetButtonDown("Spell 3"))
                spellCasting.SpellPressed(2);
            else if (Input.GetButtonUp("Spell 3"))
                spellCasting.SpellReleased(2);

            if (Input.GetButtonDown("Spell 4"))
                spellCasting.SpellPressed(3);
            else if (Input.GetButtonUp("Spell 4"))
                spellCasting.SpellReleased(3);
        }

        public void OrderUpdate(ref ControllableUnit currentUnit)
        {
            if (Input.GetAxisRaw("Order 1") > 0f)
                currentUnit.ChangeOrder(ControllableUnit.OrderType.None);
            if (Input.GetAxisRaw("Order 2") < 0f)
                currentUnit.ChangeOrder(ControllableUnit.OrderType.Def);
            if (Input.GetAxisRaw("Order 3") > 0f)
                currentUnit.ChangeOrder(ControllableUnit.OrderType.Atk);
            if (Input.GetAxisRaw("Order 4") < 0f)
                currentUnit.ChangeOrder(ControllableUnit.OrderType.Reg);
        }
    }
}