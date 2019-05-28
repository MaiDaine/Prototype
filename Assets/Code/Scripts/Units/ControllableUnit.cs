using UnityEngine;

namespace Prototype
{
    public class ControllableUnit : MonoBehaviour
    {
        public enum OrderType { None, Def, Atk, Reg };
        public UnitStats unitStats;
        public bool playerControl = false;
        public ASpell[] spellBook = new ASpell[4];

        private void Update()
        {
            if (!playerControl)
            {
                //Test
                this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1f, transform.eulerAngles.z);
            }
        }

        public void ChangeOrder(OrderType order)
        {
            Debug.Log("Order Change to: " + order.ToString());
        }
    }
}