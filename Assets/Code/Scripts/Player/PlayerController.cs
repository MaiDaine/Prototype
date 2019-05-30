using UnityEngine;

namespace Prototype
{
    public class PlayerController : MonoBehaviour
    {
        public ControllableUnit[] units;
        public ControllableUnit currentUnit = null;
        public bool useJoyStick = false;
        public UnitSet playerUnits;//Tmp

        private KeyboardController keyboardController;
        private JoystickController joystickController;
        private Vector3 joystickCursor;
        private SpellCasting spellCasting;
        private RayCast rayCast;

        private void Awake()
        {
            rayCast = this.GetComponent<Prototype.RayCast>();
            joystickController = new JoystickController();
            keyboardController = new KeyboardController(rayCast);

            playerUnits.items.Clear();
            foreach (ControllableUnit unit in units)
            {
                unit.Initialize(this);
                playerUnits.Add(unit);
            }
            currentUnit = units[0];
            currentUnit.playerControl = true;
            currentUnit.GetComponent<UnitMovement>().StopMovement();

            GetComponent<CameraController>().UpdateTarget(currentUnit.gameObject);
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
            ASpell tmp = Instantiate(spell);
            return (tmp);
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
            int index = -1;
            if (Input.GetButtonDown("Unit 1"))
                index = 0;
            else if (Input.GetButtonDown("Unit 2"))
                index = 1;
            else if (Input.GetButtonDown("Unit 3"))
                index = 2;
            else if (Input.GetButtonDown("Unit 4"))
                index = 3;
            else
                return;


            //Todo transition
            if (currentUnit != units[index])
            {
                if (currentUnit != null)
                {
                    currentUnit.GetComponent<UnitMovement>().ResumeMovement();
                    currentUnit.playerControl = false;
                }
                currentUnit = units[index];
                GetComponent<CameraController>().UpdateTarget(currentUnit.gameObject);
                currentUnit.playerControl = true;
                currentUnit.GetComponent<UnitMovement>().StopMovement();
                spellCasting.CancelCast();
                spellCasting.UpdateSpellBook(ref currentUnit.spellBook);
            }
        }

        //Movement
        private void Move()
        {
            float modifier = currentUnit.currentStats.moveSpeed * Time.deltaTime;
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