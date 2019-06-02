using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Encounter/EncounterPhase/DefaultPhase")]
    public class EncounterPhase : ScriptableObject
    {
        public WavePattern[] possibleWaves;
        public int wavePoints;
        public GameEvent[] events;
    }
}