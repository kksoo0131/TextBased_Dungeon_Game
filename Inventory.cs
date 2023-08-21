using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    //아이템 리스트를 관리하는 클래스
    internal class Inventory
    {
        List<Item> itemlist = new List<Item>();

        public void AddItem(Item _item)
        {
            itemlist.Add(_item);
        }

        public void DeleteItem(Item _item)
        {
            itemlist.Remove(_item);
        }

        public int Count()
        {
            return itemlist.Count;
        }

        public Item EquipItem(int i)
        {
            if (!itemlist[i].IsEquip)
            {
                itemlist[i].IsEquip = true;
                return itemlist[i];
            }
            else
            {
                itemlist[i].IsEquip = false;
                return itemlist[i];
            }
            
        }

        public string MakeEquipList()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[아이템 목록]\n");

            int index = 1;
            foreach (Item _item in itemlist)
            {
                if (_item.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)_item;
                    str.Append($" - {index++}. {_item.Name}| 공격력 +{weapon.Attack} | {_item.Info}\n");
                }
                else
                {
                    Armor armor = (Armor)_item;
                    str.Append($" - {index++}. {_item.Name}| 방어력 +{armor.Defense} | {_item.Info}\n");

                }
     
            }

            return str.ToString();
        }

        public string MakeItemList()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[아이템 목록]\n");

            foreach (Item _item in itemlist)
            {
                if (_item.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)_item;
                    str.Append($" - {_item.Name}| 공격력 +{weapon.Attack} | {_item.Info}\n");
                }
                else
                {
                    Armor armor = (Armor)_item;
                    str.Append($" - {_item.Name}| 방어력 +{armor.Defense} | {_item.Info}\n");

                }

            }

            return str.ToString();
        }

    }
}
