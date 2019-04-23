using UnityEngine;

public class UnitBoardSide : MonoBehaviour
{
    public UnitSet playerSide;
    public UnitSet enemySide;

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null && unit.GetComponent<UnitHealth>().alive)
        {
            if (playerSide.items.Contains(unit))
            {
                playerSide.Remove(unit);
                enemySide.Add(unit);
            }
            else
            {
                enemySide.Remove(unit);
                playerSide.Add(unit);
            }
        }
    }
}
