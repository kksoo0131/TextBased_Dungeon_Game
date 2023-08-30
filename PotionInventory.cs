using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    [Serializable]
    internal class PotionInventory
    {
        protected List<Item> Potionlist = new List<Item>();
        //Inventory.AddItem(new Potion("회복 포션(소)", ItemType.HpPotion, "소량의 체력(30)을 회복시켜주는 물약. 딸기맛!", 100, 30, 0, 3));
        public void AddPotion(Item _item)
        {
            Potionlist.Add(_item);
        }
        public Item PeekPotion(int i)
        {
            return Potionlist[i];
        }
        public string MakePotionList()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[아이템 목록]\n");

            foreach (Item _item in Potionlist)
            {
                str.Append("- ");
                str.Append($"{MakePotionInfo(_item)}");
                str.Append("\n");
            }

            return str.ToString();
        }

        public string MakePotionInfo(Item _item)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder temp = new StringBuilder();

            temp.Append($"{_item.Name}");
            while (temp.Length < 10) temp.Append(" ");
            str.Append($"{temp} | ");
            temp.Clear();

            if (_item.Type == ItemType.HpPotion)
            {
                Potion hpPotion = (Potion)_item;
                temp.Append($"체력 회복 +{hpPotion.HpRecovery}");
                while (temp.Length < 10) temp.Append(" ");
            }
            else
            {
                Potion mpPotion = (Potion)_item;
                temp.Append($"마나 회복 +{mpPotion.MpRecovery}");
                while (temp.Length < 10) temp.Append(" ");
            }
            str.Append($"{temp} | ");

            temp.Clear();
            temp.Append($"{_item.Info}");
            while (temp.Length < 30) temp.Append(" ");
            str.Append(temp);
            return str.ToString();
        }
    }
}
