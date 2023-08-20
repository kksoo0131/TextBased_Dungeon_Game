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

        private string name;
        public string Name 
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (IsEquip)
                {
                    sb.Append($"[E]{name}");
                }
                else
                {
                    sb.Append($"{name}");
                }

                while(sb.Length < 10)
                {
                    sb.Append(" ");
                }
                return sb.ToString();
                // 버그해결
                // 프로퍼티 안에서 프로퍼티를 호출하고 있어서 스택 오버플로우 발생
                // 자동 -> 수동 프로퍼티로 바꾸려면 값을 저장할 변수가 따로 필요하게된다.
            }
            private set { name = value; }
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
