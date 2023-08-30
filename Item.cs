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
        HpPotion,
        MpPotion,
    }

    public enum SortingInventory
    {
        Name,
        Equipment,
        Attack,
        Defense,
    }
    [Serializable]
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
                //if(IsEquip)
                //{
                //    sb.Append($"[E]"); // 아이템의 고유 이름값을 수정하는 건 지양
                //}
                sb.Append($"{_name}");

                return sb.ToString();
            }
            set { _name = value; }
        }       
        public ItemType Type { get; set; }
        public string Info { get; set; }
        public bool IsEquip { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int HpRecovery { get; set; }  // 체력 포션 회복량
        public int MpRecovery { get; set; }  // 마나 포션 회복량

        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value >= 0)  // 값이 0 이상인 경우에만 설정
                {
                    _quantity = value;
                }
                else
                {
                    Console.SetCursorPosition(10, 20);
                    Console.WriteLine("포션이 없으면 사용할 수 없습니다.");
                }
            }
        }
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

    [Serializable]
    class Weapon : Item

    {
        public Weapon(string _name, ItemType _type, string _Info, int _price, int _attack) : base(_name, _type, _Info, _price)
        {
            Attack = _attack;
        }

        
    }

    [Serializable]
    class Armor : Item
    {
        public Armor(string _name, ItemType _type, string _Info, int _price, int _defense) : base(_name, _type, _Info, _price)
        {
            Defense = _defense;
        }
    }

    [Serializable]
    class Potion : Item
    {
        public Potion(string _name, ItemType _type, string _Info, int _price, int _hpRecovery, int _mpRecovery, int _quantity) : base(_name, _type, _Info, _price)
        {
            HpRecovery = _hpRecovery;
            MpRecovery = _mpRecovery;
            Quantity = _quantity;
        }

    }
}
