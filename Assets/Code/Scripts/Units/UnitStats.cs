using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu]
    public class UnitStats : ScriptableObject
    {
        public string unitName = "default";
        public int cost = 0;
        public float cooldown = 1f;
        //Race;
        //Class;
        public int health = 0;
        public int mana = 0;
        //Atk Type;
        public int atkDmg = 0;
        public float atkReload = 0;
        public float atkRange = 0;
        public float moveSpeed = 0;
        //Spell;
        //Armor;

        public void Assign(UnitStats other)
        {
            this.unitName = other.unitName;
            this.cost = other.cost;
            this.cooldown = other.cooldown;
            //Race;
            //Class;
            this.health = other.health;
            this.mana = other.mana;
            //Atk Type;
            this.atkDmg = other.atkDmg;
            this.atkReload = other.atkReload;
            this.atkRange = other.atkRange;
            this.moveSpeed = other.moveSpeed;
            //Spell;
            //Armor;
        }
    }
}