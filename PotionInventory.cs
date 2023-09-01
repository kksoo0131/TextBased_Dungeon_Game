using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    [Serializable]
    internal class PotionInventory
    {
        public List<Potion> Potionlist = new List<Potion>();

        public void AddPotion(Potion _potion)
        {
            Potionlist.Add(_potion);
        }
        public Item PeekPotion(int i)
        {
            return Potionlist[i];
        }
        
        public string MakePotionList()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[음식 목록]\n");
            int index = 1;

            foreach (Potion _potion in Potionlist)
            {
                str.Append($"- {index++}. ");
                str.Append($"{MakePotionInfo(_potion)}");
                str.Append("\n");
            }

            return str.ToString();
        }

        public string MakePotionInfo(Potion _potion)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder temp = new StringBuilder();

            temp.Append($"{_potion.Name}");
            while (temp.Length < 10) temp.Append(" ");
            str.Append($"{temp} | ");
            temp.Clear();

            if (_potion.Type == ItemType.HpPotion)
            {
                Potion hpPotion = _potion;
                temp.Append($"체력 회복 +{hpPotion.HpRecovery}");
                while (temp.Length < 10) temp.Append(" ");
            }
            else
            {
                Potion mpPotion = _potion;
                temp.Append($"마나 회복 +{mpPotion.MpRecovery}");
                while (temp.Length < 10) temp.Append(" ");
            }
            str.Append($"{temp} | ");

            temp.Clear();
            temp.Append($"{_potion.Info}");
            str.Append(temp);
            str.Append($"보유 : {_potion.Quantity}개");
            return str.ToString();
        }

        public int Count()
        {
            return Potionlist.Count;
        }

        public void GetPotion(string name, int quantityToAdd)  // 매개변수로 이름, 추가할 개수 넣기
        {
            foreach (Potion _potion in Potionlist)
            {
                if (_potion.Name == name)
                {
                    _potion.Quantity += quantityToAdd;
                    return; // 원하는 아이템을 찾고 개수를 추가한 경우, 더 검색할 필요 없으므로 리턴
                }
            }
        }
    }
}
