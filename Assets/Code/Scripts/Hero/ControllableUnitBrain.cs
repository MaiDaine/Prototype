using UnityEngine;

namespace Prototype
{
    public abstract class ControllableUnitBrain : ScriptableObject
    {
        protected PlayerController playerController;
        protected ControllableUnit unit;

        public virtual void Initialize(PlayerController playerController, ControllableUnit unit)
        {
            this.playerController = playerController;
            this.unit = unit;
        }

        public virtual void Think(ControllableUnit.OrderType currentOrder)
        {
            switch (currentOrder)
            {
                case ControllableUnit.OrderType.Def:
                    Defense();
                    break;
                case ControllableUnit.OrderType.Atk:
                    Attack();
                    break;
                case ControllableUnit.OrderType.Reg:
                    Regroup();
                    break;
                default:
                    return;
            }
        }

        public virtual void Defense() { }
        public virtual void Attack() { }

        public virtual void Regroup()
        {
            unit.GetComponent<UnitMovement>().SetAgentDestination(playerController.currentUnit.transform.position);
        }
    }
}
