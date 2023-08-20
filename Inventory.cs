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

        public void EquipItem(Item _item)
        {
            if (!_item.IsEquip)
            {
                _item.IsEquip = true;
            }
            
        }

        public string MakeItemList()
        {
            StringBuilder str = new StringBuilder();

            int index = 0;
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

    }
}
