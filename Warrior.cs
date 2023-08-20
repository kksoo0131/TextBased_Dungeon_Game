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
            Inventory.AddItem(new Weapon("낡은 검", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 0, 2));
            Inventory.AddItem(new Armor("무쇠갑옷", ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 5));
        }


        Inventory Inventory = new Inventory();
        public int Level { get; set; }
        public string Chad { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }

        public string PlayerInfo()
        {
            return $"Lv. {Level}\n\nChad({Chad})\n\n공격력: {Attack}\n\n방어력: {Defense}\n\n체력: {Health}\n\nGold: {Gold} G\n\n";
        }

        public string InventoryInfo()
        {
            return Inventory.MakeItemList();
        }
     
    }
}
