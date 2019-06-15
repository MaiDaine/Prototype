using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class EncounterController : MonoBehaviour
    {
        public static EncounterController instance;

        public Encounter encounterEventRef;
        public Transform[] spawnPoints;
        public Transform bossSpawnPoint;
        public CardData boss;
        public Unit activeHero;
        public UnitSet playerUnits;
        public UnitSet enemyUnits;
        public ObjectPool bulletPool;

        private Encounter encounterEvent;
        private int spawnPointCount;
        private float timerEvent = 0f;
        private float timerSpawn;
        private List<CardData> nextSpawn;
        private bool lastWave;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                if (encounterEventRef == null)
                    return;
                spawnPointCount = spawnPoints.Length;
                nextSpawn = new List<CardData>();
                encounterEvent = ScriptableObject.CreateInstance(encounterEventRef.name) as Encounter;
                encounterEvent.Init(this, encounterEventRef.decks, encounterEventRef.phases);
                encounterEvent.NextPhase(ref nextSpawn);
                timerSpawn = Time.deltaTime;
                lastWave = false;
                enemyUnits.items.Clear();
            }
            else
                Destroy(this);
        }

        private void Update()
        {
            if (timerEvent > 0f)
            {
                timerEvent -= Time.deltaTime;
                if (timerEvent <= 0f)
                {
                    lastWave = encounterEvent.NextPhase(ref nextSpawn);
                    if (!lastWave)
                        timerSpawn = Time.deltaTime;
                    else
                    {
                        NonControllableUnit tmp = Instantiate((NonControllableUnit)boss.unit, bossSpawnPoint);
                        tmp.Initialize(activeHero, boss.unitStats, "EnemyTeam");
                        enemyUnits.Add(tmp);//TODO EVENT CLEAR
                    }
                }

            }
            if (timerSpawn > 0f)
            {
                timerSpawn -= Time.deltaTime;
                if (timerSpawn <= 0f)
                {
                    timerSpawn = SpawnUnit(nextSpawn[0]);
                    nextSpawn.RemoveAt(0);
                    timerEvent -= timerSpawn;
                    if (nextSpawn.Count == 0)
                    {
                        timerEvent *= -1f;
                        timerSpawn = -1f;
                    }
                }
            }
        }

        public void OnUnitDeath(NonControllableUnit unit)
        {
            enemyUnits.Remove(unit);
            if (enemyUnits.items.Count == 0 && lastWave)
                Application.Quit();//TODO GAMEOVER
        }

        private float SpawnUnit(CardData unitData)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                NonControllableUnit tmp = Instantiate((NonControllableUnit)unitData.unit, spawnPoints[i]);
                tmp.Initialize(FindClosestTarget(spawnPoints[i].position), unitData.unitStats, "EnemyTeam");
                enemyUnits.Add(tmp);//TODO EVENT CLEAR
            }
            return unitData.unitStats.cooldown;
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