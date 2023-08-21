using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    class Warrior
    {
        public Warrior() 
        { 
            Level = 1;
            Chad = "전사";
            Attack = 10;
            Defense = 5;
            Health = 100;
            Gold = 1500;
            Inventory.AddItem(new Weapon("낡은 검", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 600, 2));
            Inventory.AddItem(new Armor("무쇠갑옷", ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.", 500, 5));
        }


        public Inventory Inventory = new Inventory();
        public int Level { get; set; }
        public string Chad { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public int AddAttack { get; set; }
        public int AddDefense { get; set; }

        public string PlayerInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Level}\n\nChad({Chad})\n\n");
            sb.Append(AddAttack > 0 ? $"공격력: {Attack + AddAttack} (+{AddAttack})\n\n" : $"공격력: {Attack}\n\n");
            sb.Append(AddDefense > 0 ? $"방어력 : {Defense + AddDefense} (+{AddDefense})\n\n" : $"방어력: {Defense}\n\n");
            sb.Append($"체력: {Health}\n\nGold: {Gold} G\n\n");
            return sb.ToString();
        }

        public void EquipItem(int i)
        {
            Item target = Inventory.PeekItem(i);

            if (target.IsEquip)
            {
                target.IsEquip = false;
                AddAttack -= target.Attack;
                AddDefense -= target.Defense;
            }
            else
            {
                target.IsEquip = true;
                AddAttack += target.Attack;
                AddDefense += target.Defense;
            }
        }
        /*public void EquipItem(int i)
        {
            Item target = Inventory.EquipItem(i);

            if (target.IsEquip)
            {
                if(target.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)target;
                    AddAttack += weapon.Attack;
                }
                else
                {
                    Armor armor = (Armor)target;
                    AddDefense += armor.Defense;
                }
            }
            else
            {
                if (target.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)target;
                    AddAttack -= weapon.Attack;
                }
                else
                {
                    Armor armor = (Armor)target;
                    AddDefense -= armor.Defense;
                }
            }
        }*/

        public void SellItem(int i)
        {
            Item sellitem = Inventory.PeekItem(i);

            if (sellitem.IsEquip)
            {
                EquipItem(i);
            }
            Console.WriteLine((int)(sellitem.Price * 0.85f));
            Gold += (int)(sellitem.Price * 0.85f);
            Inventory.DeleteItem(sellitem);
            
            
        }
     
    }
}
