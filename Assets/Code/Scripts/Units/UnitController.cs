using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(RayCast))]
    public class UnitController : MonoBehaviour
    {
        public IntReference spawnLayer;
        public BoardStateReference boardState;
        public NPCCommander commander;
        public CardData playerHeroCard;
        public CardData npcHeroCard;
        public CardData selectedCard;
        public UnitSet playerSide;
        public UnitSet npcSide;

        private Unit playerHero;
        private Unit npcHero;
        private Unit unit;
        private bool isSpawning = false;
        private float unitSizeOffset = 0f;
        private RayCast rayCast;

        private void Start()
        {
            rayCast = this.gameObject.GetComponent<RayCast>();
            playerSide.items.Clear();
            npcSide.items.Clear();

            playerHero = Instantiate<Unit>(playerHeroCard.unit,
                boardState.Value.playerSpawnPosition.position,
                boardState.Value.playerSpawnPosition.rotation);

            npcHero = Instantiate<Unit>(npcHeroCard.unit,
                boardState.Value.npcSpawnPosition.position,
                boardState.Value.npcSpawnPosition.rotation);

            ((NonControllableUnit)npcHero).Initialize(playerHero, npcHeroCard.unitStats, "EnemyTeam");
            ((NonControllableUnit)npcHero).brain.Assign(npcSide);
            
            ((NonControllableUnit)playerHero).Initialize(npcHero, playerHeroCard.unitStats, "PlayerTeam");
            ((NonControllableUnit)playerHero).brain.Assign(playerSide);

            commander.Initialize(playerHero);
        }

        private void Update()
        {
            if (isSpawning)
            {
                Vector3 boardPosition;
                bool validRayCastHit;

                validRayCastHit = rayCast.BoardRayCast(out boardPosition, spawnLayer.Value);
                unit.transform.position = new Vector3(boardPosition.x, unitSizeOffset, boardPosition.z);

                if (validRayCastHit && Input.GetMouseButtonDown(0))
                {
                    ((NonControllableUnit)unit).Initialize(npcHero, selectedCard.unitStats, "PlayerTeam");
                    playerSide.Add(unit);
                    unit = null;
                    isSpawning = false;
                }
            }
            commander.Think();
        }

        public void InstantiatePlayerUnit()
        {
            if (unit != null)
                Destroy(unit.gameObject);
            unit = Instantiate<Unit>(
                selectedCard.unit,
                boardState.Value.playerSpawnPosition.position,
                boardState.Value.playerSpawnPosition.rotation
            );
            unitSizeOffset = unit.GetComponent<Renderer>().bounds.size.y / 2f;
            isSpawning = true;
        }

        //FIXME Should change later
        public void OnUnitDeath()
        {
            playerSide.ClearSelf();
            npcSide.ClearSelf();
        }
    }
}