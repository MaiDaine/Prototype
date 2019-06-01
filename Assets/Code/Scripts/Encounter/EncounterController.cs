using UnityEngine;

namespace Prototype
{
    public class EncounterController : MonoBehaviour
    {
        public static EncounterController instance;

        public EncounterEvent encounterEventRef;
        public Transform[] spawnPoints;
        public Unit activeHero;
        public UnitSet playerUnits;
        public UnitSet enemyUnits;

        private EncounterEvent encounterEvent;
        private int spawnPointCount;
        private float timer;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                spawnPointCount = spawnPoints.Length;
                encounterEvent = ScriptableObject.CreateInstance(encounterEventRef.name) as EncounterEvent;
                encounterEvent.Init(this, encounterEventRef.units);
            }
            else
                Destroy(this);
        }

        private void Start()
        {
            timer = encounterEvent.FirstSpawn();
        }

        private void Update()
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                if (timer < 0f)
                    timer = encounterEvent.Update();
            }
        }

        public Unit SpawnUnit(CardData unitData, Transform spawnPoint, Unit target = null)
        {
            NonControllableUnit tmp = Instantiate((NonControllableUnit)unitData.unit, spawnPoint);
            tmp.Initialize(target == null ? FindClosestTarget(spawnPoint.position) : target, unitData.unitStats, "EnemyTeam");
            enemyUnits.Add(tmp);//TODO EVENT CLEAR
            return tmp;
        }

        private Unit FindClosestTarget(Vector3 position)
        {
            int index = 0;
            float distance = Vector3.Distance(position, playerUnits.items[0].transform.position);

            for (int i = 1; i < playerUnits.items.Count; i++)
                if (Vector3.Distance(position, playerUnits.items[i].transform.position) < distance)
                    index = i;
            return playerUnits.items[index];
        }
    }
}