using UnityEngine;

namespace Prototype
{
    public class PlayerController : MonoBehaviour
    {
        public ControllableUnit[] units;
        public bool useJoyStick = false;

        private KeyboardController keyboardController;
        private JoystickController joystickController;
        private Vector3 joystickCursor;
        private SpellCasting spellCasting;
        private ControllableUnit currentUnit = null;
        private RayCast rayCast;

        private void Start()
        {
            rayCast = this.GetComponent<Prototype.RayCast>();

            joystickController = new JoystickController();
            keyboardController = new KeyboardController(rayCast);

            currentUnit = units[0];
            currentUnit.playerControl = true;

            spellCasting = new SpellCasting(this);
            spellCasting.UpdateSpellBook(ref currentUnit.spellBook);
        }

        private void Update()
        {
            SwitchUnit();
            SpellUpdate();
            Move();

            if (useJoyStick)
            {
                if (spellCasting.casting)
                    joystickController.RotateTowardCursor(ref joystickCursor, ref currentUnit);
                else
                    joystickController.StickRotate(ref currentUnit);
                joystickController.OrderUpdate(ref currentUnit);
            }
            else
            {
                keyboardController.MouseRotate(ref currentUnit);
                keyboardController.OrderUpdate(ref currentUnit);
            }
        }

        //Spells
        public ASpell InstantiateSpell(ref ASpell spell)
        {
            return (Instantiate(spell));
        }

        private void SpellUpdate()
        {
            if (useJoyStick)
            {
                joystickController.SpellUpdate(spellCasting);
                if (spellCasting.casting)
                {
                    joystickController.UpdateJoystickCursor(ref joystickCursor);
                    spellCasting.CastUpdate(joystickCursor);
                }
            }
            else
                keyboardController.SpellUpdate(ref spellCasting);
        }

        //Unit Handle
        private void SwitchUnit()
        {
            int index;
            if (Input.GetKeyDown("Unit 1"))
                index = 0;
            else if (Input.GetKeyDown("Unit 2"))
                index = 1;
            else if (Input.GetKeyDown("Unit 3"))
                index = 2;
            else if (Input.GetKeyDown("Unit 4"))
                index = 3;
            else
                return;

            //Todo transition
            if (currentUnit != units[index])
            {
                if (currentUnit != null)
                    currentUnit.playerControl = false;
                currentUnit = units[index];
                GetComponent<CameraController>().UpdateTarget(currentUnit.gameObject);
                currentUnit.playerControl = true;
                spellCasting.CancelCast();
                spellCasting.UpdateSpellBook(ref currentUnit.spellBook);
            }
        }

        //Movement
        private void Move()
        {
            float modifier = currentUnit.unitStats.moveSpeed * Time.deltaTime;
            currentUnit.transform.position += new Vector3(
                Input.GetAxis("Horizontal") * modifier,
                0f,
                Input.GetAxis("Vertical") * modifier);
        }

        //JoystickHandle
        public void ResetJoystickCursor()
        {
            joystickCursor = currentUnit.transform.position;
            joystickCursor.y = 0f;
        }
    }
}