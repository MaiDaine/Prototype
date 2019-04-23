using UnityEngine;

[CreateAssetMenu(menuName = "Commander/Default")]
public class DefaultCommander : NPCCommander
{
    public float timerSpawn = 5f;

    private float timer = 0f;

    public override void Think()
    {
        timer += Time.deltaTime;
        if (timer >= timerSpawn)
        {
            timer = 0f;
            SpawnUnit(deck.cards[0], board.Value.npcSpawnPosition.transform.position);
        }
    }
}
