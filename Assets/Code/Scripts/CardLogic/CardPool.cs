using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/CardPool")]
public class CardPool : ScriptableObject
{
    [Serializable]
    public struct RollProbability
    {

        [Serializable]
        public struct Roll
        {
            public int cost;
            public int probability;
        }
        public int level;
        public Roll[] probability;
    }

    public CardData[] units;
    public RollProbability[] rollProbability;
    public IntVariable playerLevel;
    public int rollSize;

    private Dictionary<int, List<CardData>> perCostUnits = new Dictionary<int, List<CardData>>()
    {
        {1, new List<CardData>()},
        {2, new List<CardData>()},
        {3, new List<CardData>()},
        {4, new List<CardData>()},
        {5, new List<CardData>()}
    };

    public CardData[] GetCardRoll()
    {
        CardData[] roll = new CardData[rollSize];
        RollProbability odds = Array.Find(rollProbability, e => e.level == playerLevel.value);

        for (int i = 0; i < roll.Length; i++)
        {
            float rollValue = UnityEngine.Random.Range(0, 100);
            int unitCostRolled = 1;
            int range = 0;

            for (int j = 0; j < odds.probability.Length; j++)
            {
                range += odds.probability[j].probability;

                if (rollValue <= range)
                {
                    unitCostRolled = odds.probability[j].cost;
                    break;
                }
            }
            roll[i] = GetRandomCardPerCost(unitCostRolled);
        }
        return roll;
    }

    private CardData GetRandomCardPerCost(int cost)
    {
        int randomIdx = Mathf.RoundToInt(UnityEngine.Random.Range(0, perCostUnits[cost].Count - 1));

        return perCostUnits[cost][randomIdx];
    }

    private void OnEnable()
    {
        for (int i = 0; i < units.Length; i++)
            perCostUnits[units[i].unitStats.cost].Add(units[i]);
    }
}