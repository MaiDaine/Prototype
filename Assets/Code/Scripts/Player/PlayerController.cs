using UnityEngine;

namespace Prototype
{
    public class PlayerController : MonoBehaviour
    {
        public ControllableUnit[] units;
        public Vector3[] spawnPoints;
        public ControllableUnit currentUnit = null;
        public bool useJoyStick = false;
        public UnitSet playerUnits;

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
            for (int i = 0; i < units.Length && units[i] != null; i++)
            {
                units[i] = Instantiate(units[i]);
                units[i].gameObject.SetActive(false);
                units[i].Initialize(this);
            }
            currentUnit = units[0];
            currentUnit.transform.position = spawnPoints[0];
            currentUnit.gameObject.SetActive(true);
            playerUnits.Add(currentUnit);

            GetComponent<CameraController>().UpdateTarget(currentUnit.gameObject);
            spellCasting = new SpellCasting(this);
            spellCasting.UpdateSpellBook(ref currentUnit.spellBook);
        }

        private void Update()
        {
            for (int i = 0; i < units.Length; i++)
                if (units[i] != null)
                    units[i].UpdateSpellCooldowns();

            if (currentUnit.stuns != 0)
                return;

            UnitUpdate();
            SpellUpdate();
            if (currentUnit.roots == 0)
                Move();

            if (useJoyStick)
            {
                if (spellCasting.casting && spellCasting.useCursor)
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

            tmp.Init("PlayerTeam", currentUnit.gameObject);
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

        //Units Handle
        public void OnUnitDeath()
        {
            int index = -1;
            for (int i = 0; i < units.Length; i++)
                if (units[i] != null && units[i].unitHealth.alive && units[i] != currentUnit)
                {
                    index = i;
                    break;
                }
            if (index == -1)
            {
                this.enabled = false;
                Application.Quit();//TODO GAMEOVER
            }
            else
                SwitchUnit(index);
        }

        private void UnitUpdate()
        {
            int index;
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
            if (units[index] != null && units[index].unitHealth.alive && units[index] != currentUnit)
                SwitchUnit(index);
        }

        private void SwitchUnit(int index)
        {
            units[index].transform.position = currentUnit.transform.position;
            units[index].transform.rotation = currentUnit.transform.rotation;
            units[index].gameObject.SetActive(true);
            playerUnits.items[0] = units[index];
            currentUnit.gameObject.SetActive(false);
            currentUnit = units[index];
            //Animation

            EncounterController.instance.activeHero = currentUnit;
            GetComponent<CameraController>().UpdateTarget(currentUnit.gameObject);

            spellCasting.CancelCast();
            spellCasting.UpdateSpellBook(ref currentUnit.spellBook);
        }

        //Movement
        private void Move()
        {
            float modifier = currentUnit.currentSpeed * Time.deltaTime;
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