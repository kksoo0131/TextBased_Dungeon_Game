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
        protected List<Item> itemlist = new List<Item>();

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
                str.Append($"- {index++} ");

                if (_item.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)_item;
                    str.Append(MakeItemInfo(weapon));
                }
                else
                {
                    Armor armor = (Armor)_item;
                    str.Append(MakeItemInfo(armor));
                }

                str.Append("\n");

            }

            return str.ToString();
        }

        public string MakeItemList()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[아이템 목록]\n");

            foreach (Item _item in itemlist)
            {
                str.Append("- ");
                str.Append($"{MakeItemInfo(_item)}");
                str.Append("\n");
            }

            return str.ToString();
        }

        public string MakeItemInfo(Item _item)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder temp = new StringBuilder();

            temp.Append($"{_item.Name}");
            while (temp.Length < 10) temp.Append(" ");
            str.Append($"{temp} | ");
            temp.Clear();

            if (_item.Type == ItemType.Weapon)
            {
                Weapon weapon = (Weapon)_item;
                temp.Append($"공격력 +{weapon.Attack}");
                while (temp.Length < 10) temp.Append(" ");
            }
            else
            {
                Armor armor = (Armor)_item;
                temp.Append($"방어력 +{armor.Defense}");
                while (temp.Length < 10) temp.Append(" ");
            }
            str.Append($"{temp} | ");

            temp.Clear();
            temp.Append($"{_item.Info}");
            while (temp.Length < 30) temp.Append(" ");
            str.Append(temp);
            return str.ToString();
        }

        public void SortInventory(SortingInventory type)
        {
            itemlist.Sort((x, y) => x.CompareBySortingInventory(y, type));
        }

    }
    
    class Shop : Inventory
    {
        public Shop()
        {
            AddItem(new Armor("수련자 갑옷", ItemType.Armor, "수련에 도움을 주는 갑옷입니다.", 1000, 5));
            AddItem(new Armor("무쇠갑옷", ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9));
            AddItem(new Armor("스파르타의 갑옷", ItemType.Armor, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, 15));
            AddItem(new Weapon("낡은 검", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 600, 2));
            AddItem(new Weapon("청동 도끼", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 1500, 5));
            AddItem(new Weapon("스파르타의 창", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 4000, 7));

        }
        public new string MakeItemList()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[아이템 목록]\n");

            foreach (Item _item in itemlist)
            {
                str.Append("- ");
                str.Append($"{MakeItemInfo(_item)} |");
                str.Append("\n");
            }

            return str.ToString();
        }
        public new string MakeItemInfo(Item _item)
        {
            StringBuilder str = new StringBuilder();
            str.Append(base.MakeItemInfo(_item));
            str.Append(_item.IsSell ? "구매완료" : $"{_item.Price} G");

            return str.ToString();
        }
        public string MakeShopList()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[아이템 목록]\n");

            int index = 1;
            foreach (Item _item in itemlist)
            {
                str.Append($"- {index++} ");

                if (_item.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)_item;
                    str.Append(MakeItemInfo(weapon));
                }
                else
                {
                    Armor armor = (Armor)_item;
                    str.Append(MakeItemInfo(armor));
                }

                str.Append("\n");

            }

            return str.ToString();
        }

        public Item? BuyItem(int index)
        {
            if (itemlist[index].IsSell)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                return null;
            }

            if (DungeonGame.player.Gold >= itemlist[index].Price)
            {
                DungeonGame.player.Gold -= itemlist[index].Price;
                itemlist[index].IsSell = true;
                Console.WriteLine("구매를 완료했습니다.");
                return itemlist[index];
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
                return null;
            }
                
            
        }
    }
}
