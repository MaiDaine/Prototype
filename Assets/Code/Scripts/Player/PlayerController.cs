using Prototype;
using UnityEngine;

namespace Prototype
{
    public class PlayerController : MonoBehaviour
    {
        public ControllableUnit[] units;
        public bool useJoyStick = false;
        public float joystickSensitivity = 30f;//TODO userPref

        private RayCast rayCast;
        private SpellCasting spellCasting;
        private ControllableUnit currentUnit = null;
        private Vector3 joystickCursor;

        private void Start()
        {
            currentUnit = units[0];
            currentUnit.playerControl = true;

            spellCasting = new SpellCasting(this);
            spellCasting.UpdateSpellBook(ref currentUnit.spellBook);

            rayCast = this.GetComponent<Prototype.RayCast>();
        }

        private void Update()
        {
            SwitchUnit();

            SpellUpdate();

            Move();

            if (useJoyStick)
            {
                if (spellCasting.casting)
                    RotateTowardCursor();
                else
                    StickRotate();
            }
            else
                MouseRotate();
        }

        //Spells
        public ASpell InstantiateSpell(ref ASpell spell)
        {
            return (Instantiate(spell));
        }

        private void SpellUpdate()
        {
            if (Input.GetButtonDown("Spell 1"))
                spellCasting.SpellPressed(0);
            if (Input.GetButtonUp("Spell 1"))
                spellCasting.SpellReleased(0);

            if (spellCasting.casting)
            {
                if (useJoyStick)
                {
                    UpdateJoystickCursor();
                    spellCasting.CastUpdate(joystickCursor);
                }
                else
                    spellCasting.CastUpdate(rayCast.BoardRayCast());
            }
        }

        //Unit Handle
        private void SwitchUnit()
        {
            int index;
            if (Input.GetButtonDown("Control 1"))
                index = 0;
            else if (Input.GetButtonDown("Control 2"))
                index = 1;
            else if (Input.GetButtonDown("Control 3"))
                index = 2;
            else if (Input.GetButtonDown("Control 4"))
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
        
        //Rotation
        private void MouseRotate()
        {
            Vector3 tmp;

            rayCast.PlaneRayCast(out tmp);
            tmp.y = currentUnit.transform.position.y;
            currentUnit.transform.LookAt(tmp);
        }

        private void StickRotate()
        {
            if (Input.GetAxis("Rotation X") != 0f || Input.GetAxis("Rotation Y") != 0f)
                currentUnit.transform.eulerAngles = new Vector3(
                    currentUnit.transform.eulerAngles.x,
                    Mathf.Atan2(Input.GetAxis("Rotation X"), Input.GetAxis("Rotation Y")) * Mathf.Rad2Deg,
                    currentUnit.transform.eulerAngles.z);
        }

        private void RotateTowardCursor()
        {
            float tmp = joystickCursor.y;

            joystickCursor.y = currentUnit.transform.position.y;
            currentUnit.transform.LookAt(joystickCursor);
            joystickCursor.y = tmp;
        }

        //JoystickHandle
        public void ResetJoystickCursor()
        {
            joystickCursor = currentUnit.transform.position;
            joystickCursor.y = 0f;
        }

        private void UpdateJoystickCursor()
        {
            joystickCursor.x += Input.GetAxis("Rotation X") * Time.deltaTime * joystickSensitivity;
            joystickCursor.z += Input.GetAxis("Rotation Y") * Time.deltaTime * joystickSensitivity;
        }
    }
}