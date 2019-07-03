using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype
{
    public class PlayerController : MonoBehaviour
    {
        public ControllableUnit[] units;
        public UnitPortrait[] unitPortraits = new UnitPortrait[4];
        public Vector3 playerSpawn;
        public ControllableUnit currentUnit = null;
        public bool useJoyStick = false;
        public UnitSet playerUnits;

        private KeyboardController keyboardController;
        private JoystickController joystickController;
        private Vector3 joystickCursor;
        private SpellCasting spellCasting;
        private RayCast rayCast;
        private Animator currentAnimator;

        private void Awake()
        {
            rayCast = this.GetComponent<Prototype.RayCast>();
            joystickController = new JoystickController();
            keyboardController = new KeyboardController(rayCast);
            spellCasting = new SpellCasting(this);

            playerUnits.items.Clear();
            for (int i = 0; i < units.Length && units[i] != null; i++)
            {
                unitPortraits[i].gameObject.SetActive(true);
                units[i] = Instantiate(units[i]);
                units[i].gameObject.SetActive(false);
                units[i].Initialize(this, unitPortraits[i]);
            }
            currentUnit = units[0];
            currentAnimator = currentUnit.GetComponentInChildren<Animator>();
            currentUnit.transform.position = playerSpawn;
            currentUnit.gameObject.SetActive(true);
            playerUnits.Add(currentUnit);
            GetComponent<CameraController>().UpdateTarget(currentUnit.gameObject);
            spellCasting.UpdateSpellBook(ref currentUnit.spellBook);
        }

        private void Start()
        {
            EncounterController.instance.activeHero = currentUnit;
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
                GetComponent<CameraController>().enabled = false;
                SceneManager.LoadScene("TrainingGround");
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
            currentAnimator = currentUnit.GetComponentInChildren<Animator>();
            //Animation

            EncounterController.instance.activeHero = currentUnit;
            GetComponent<CameraController>().UpdateTarget(currentUnit.gameObject);

            spellCasting.CancelCast();
            spellCasting.UpdateSpellBook(ref currentUnit.spellBook);
        }

        //Movement
        private void Move()
        {
            float y = Input.GetAxis("Horizontal") * currentUnit.transform.forward.x + Input.GetAxis("Vertical") * currentUnit.transform.forward.z;
            float x = Input.GetAxis("Horizontal") * currentUnit.transform.forward.z + Input.GetAxis("Vertical") * -currentUnit.transform.forward.x;
            currentAnimator.SetFloat("Move_X", x);
            currentAnimator.SetFloat("Move_Y", y);
            currentAnimator.SetFloat("AnimationSpeed", (Mathf.Abs(x) + Mathf.Abs(y) / 2f));
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