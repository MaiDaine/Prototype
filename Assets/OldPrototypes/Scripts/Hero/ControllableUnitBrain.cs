using UnityEngine;

namespace Prototype
{
    public abstract class ControllableUnitBrain : ScriptableObject
    {
        protected PlayerController playerController;
        protected ControllableUnit unit;

        private ControllableUnit.OrderType currentOrder;

        public virtual void Initialize(PlayerController playerController, ControllableUnit unit)
        {
            this.playerController = playerController;
            this.unit = unit;
        }
        
        public virtual void ChangeOrder(ControllableUnit.OrderType currentOrder)
        {
            this.currentOrder = currentOrder;
        }

        public virtual void Think()
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

        protected virtual void Defense() { }

        protected virtual void Attack() { }

        protected virtual void Regroup()
        {
            unit.GetComponent<UnitMovement>().SetAgentDestination(playerController.currentUnit.transform.position);
        }
    }
}
