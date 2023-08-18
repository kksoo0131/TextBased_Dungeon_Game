using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    // 0. 아이템 이름
    // 1. 아이템 계열 type
    // 2. 아이템 능력치
    // 3. 아이템 설명 text
    // 4. 장착 중인가?

    // 결국 이 아이템은 player가 가지고있는 inventory에 들어가게됨

    // player는 inventory객체를 하나가지고 inventory객체는 아이템을 넣고 빼고 정렬할 수 있다.

    public enum ItemType
    {
        Weapon,
        Armor,
    }

    // 
    class Item
    {
        public Item(string _name, ItemType _type, string _Info, int _price)
        {
            Name = _name;
            Type = _type;
            Info = _Info;
            Price = _price;
            IsEquip = false;
        }

        public string Name 
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (IsEquip)
                {
                    sb.Append($"[E]{Name}");
                }
                else
                {
                    sb.Append($"{Name}");
                }

                while(sb.Length < 12)
                {
                    sb.Append(" ");
                }
                Console.WriteLine(sb.ToString());
                return sb.ToString();
            }
            private set { }
        }
        public ItemType Type { get; private set; }
        public string Info { get; private set; }
        public bool IsEquip { get; set; }

        public int Price { get; private set; }
    }

    class Weapon : Item
    {
        public Weapon(string _name, ItemType _type, string _Info, int _price, int _attack) : base(_name, _type, _Info, _price)
        {
            Attack = _attack;
        }

        public int Attack { get; private set;}
    }

    class Armor : Item
    {
        public Armor(string _name, ItemType _type, string _Info, int _price, int _defense) : base(_name, _type, _Info, _price)
        {
            Defense = _defense;
        }
        public int Defense { get; private set; }
    }
}
