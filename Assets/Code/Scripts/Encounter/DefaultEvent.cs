using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "EncounterEvent/Default")]
    public class DefaultEvent : EncounterEvent
    {
        public float tick = 1.5f;
        public int count = 1;

        public override float FirstSpawn()
        {
            return (SpawnCreeps());
        }

        public override float Update()
        {
            if (count < 10)
            {
                tick += 1f;
                count *= 2;
                return (SpawnCreeps());
            }
            Cleared();
            return 0f;
        }

        private float SpawnCreeps()
        {
            encounterController.SpawnUnit(units.cards[0], encounterController.spawnPoints[0]);
            encounterController.SpawnUnit(units.cards[0], encounterController.spawnPoints[1]);
            return tick;
        }
    }
}
