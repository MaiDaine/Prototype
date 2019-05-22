using UnityEngine;

namespace Prototype
{
    public abstract class NPCCommander : ScriptableObject
    {
        public CardDeck deck;
        public BoardStateReference board;
        public UnitSet playerUnits;
        public UnitSet npcUnits;

        protected Unit playerHero;
        protected Vector3 spawnPosition;

        public virtual void Initialize(Unit playerHero)
        {
            this.playerHero = playerHero;
            spawnPosition = board.Value.npcSpawnPosition.transform.position - playerHero.transform.forward * 2f;
        }

        public abstract void Think();

        protected virtual void SpawnUnit(CardData unitData, Vector3 position)
        {
            Unit unit = Instantiate<Unit>(
                unitData.unit,
                spawnPosition,
                board.Value.npcSpawnPosition.rotation
            );
            unit.Initialize(playerHero, unitData.unitStats, "EnemyTeam");
            npcUnits.Add(unit);
        }
    }
}