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

    public enum SortingInventory
    {
        Name,
        Equipment,
        Attack,
        Defense,
    }

    class Item : IComparable<Item>
    {
        public Item(string _name, ItemType _type, string _Info, int _price)
        {
            Name = _name;
            Type = _type;
            Info = _Info;
            Price = _price;
            IsEquip = false;
            Attack = 0;
            Defense = 0;
            IsSell = false;
        }

        private string _name;
        public string Name 
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if(IsEquip)
                {
                    sb.Append($"[E]");
                }
                sb.Append($"{_name}");

                return sb.ToString();
            }
            private set { _name = value; }
        }       
        public ItemType Type { get; private set; }
        public string Info { get; private set; }
        public bool IsEquip { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Price { get; private set; }

        public bool IsSell { get; set; }
        

        public int CompareTo(Item? other)
        {
            return CompareBySortingInventory(other, SortingInventory.Name);
        }

        public int CompareBySortingInventory(Item? other, SortingInventory type)
        {
            switch (type)
            {
                case SortingInventory.Name:
                    return Name.CompareTo(other.Name);
                case SortingInventory.Equipment:
                    return -IsEquip.CompareTo(other.IsEquip);
                    // true가 위로 가기 위해서 CompareTo가 return한 int 값에 -1을 곱해서 방향을 바꿔준다.
                case SortingInventory.Attack:
                    return Attack.CompareTo(other.Attack);
                case SortingInventory.Defense:
                    return Defense.CompareTo(other.Defense);
                default:
                    return Name.CompareTo(other.Name);
            }
        }
    }

    class Weapon : Item

    {
        public Weapon(string _name, ItemType _type, string _Info, int _price, int _attack) : base(_name, _type, _Info, _price)
        {
            Attack = _attack;
        }

        
    }

    class Armor : Item
    {
        public Armor(string _name, ItemType _type, string _Info, int _price, int _defense) : base(_name, _type, _Info, _price)
        {
            Defense = _defense;
        }
    }
}
