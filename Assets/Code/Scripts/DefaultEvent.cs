using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "EncounterEvent/Default")]
    public class DefaultEvent : EncounterEvent
    {
        public override float FirstSpawn()
        {
            encounterController.SpawnUnit(units.cards[0], encounterController.spawnPoints[0]);
            return base.FirstSpawn();
        }
    }
}
