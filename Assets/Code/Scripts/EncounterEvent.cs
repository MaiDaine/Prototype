using UnityEngine;

namespace Prototype
{
    public class EncounterEvent : ScriptableObject
    {
        public CardDeck units;

        protected EncounterController encounterController;

        public virtual void Init(EncounterController encounterController, CardDeck units)
        {
            this.encounterController = encounterController;
            this.units = units;
        }

        public virtual float FirstSpawn() { return 0f; }

        public virtual float Update() { return 0f; }
    }
}
