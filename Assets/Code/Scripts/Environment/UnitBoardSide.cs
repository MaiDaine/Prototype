using UnityEngine;

namespace Prototype
{
    public class UnitBoardSide : MonoBehaviour
    {
        public UnitSet playerSide;
        public UnitSet NPCSide;

        private void OnTriggerEnter(Collider other)
        {
            Unit unit = other.gameObject.GetComponent<Unit>();
            if (unit != null && unit.GetComponent<UnitHealth>().alive)
            {
                if (playerSide.items.Contains(unit))
                {
                    playerSide.Remove(unit);
                    NPCSide.Add(unit);
                }
                else
                {
                    NPCSide.Remove(unit);
                    playerSide.Add(unit);
                }
            }
        }
    }
}