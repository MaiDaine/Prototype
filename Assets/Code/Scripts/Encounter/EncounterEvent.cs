using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class EncounterEvent : ScriptableObject
    {
        public CardDeck[] decks;
        public EncounterPhase[] phases;

        protected EncounterController encounterController;
        protected int phaseIndex;

        public virtual void Init(EncounterController encounterController, CardDeck[] decks, EncounterPhase[] phases)
        {
            this.encounterController = encounterController;
            this.decks = decks;
            this.phases = phases;
            phaseIndex = 0;
        }

        public virtual bool NextPhase(ref List<CardData> nextSpawn)
        {
            EncounterPhase currentPhase = phases[phaseIndex];
            WavePattern currentPattern = currentPhase.possibleWaves[Random.Range(0, currentPhase.possibleWaves.Length)];

            for (int i = 0; i < currentPattern.groups.Length; i++)
                AddToList(ref nextSpawn, currentPattern.groups[i] - 1, currentPhase.wavePoints / currentPattern.groups.Length, currentPattern.uniqueInGroup);
            phaseIndex++;
            for (int i = 0; i < currentPhase.events.Length; i++)
                currentPhase.events[i].Raise();
            return (phaseIndex == phases.Length);
        }

        protected virtual void AddToList(ref List<CardData> nextSpawn, int deckIndex, int availablePoints, bool unique)
        {
            int[] unitsIndex;
            int tmp;
            if (unique)
            {
                unitsIndex = new int[1];
                unitsIndex[0] = Random.Range(0, decks[deckIndex].cardsNumber);
            }
            else
            {
                int max = decks[deckIndex].cards.Length;
                unitsIndex = new int[Random.Range(1, max)];
                tmp = Random.Range(1, max);
                for (int i = 0; i < unitsIndex.Length; i++)
                    unitsIndex[i] = (tmp + i) % max;
            }

            for (int i = 0; i < unitsIndex.Length; i++)
            {
                CardData card = decks[deckIndex].cards[unitsIndex[i]];

                int count = (availablePoints / decks[deckIndex].cards[unitsIndex[i]].unitStats.cost) / unitsIndex.Length;
                for (int j = 0; j < count; j++)
                    nextSpawn.Add(card);
            }
        }
    }
}
