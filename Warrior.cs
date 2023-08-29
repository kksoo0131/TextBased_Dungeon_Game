using System.Text;



namespace TextBased_Dungeon_Game
{
    [Serializable]
    class Warrior : Unit
    {
        public Warrior() : base("", 1, 10, 5, 100)
        {
            Chad = "전사";
            Gold = 1500;
            MaxHealth = 100;
            Exp = 0;
            ExpNeeded = CalculateExpNeeded();
            
            Inventory.AddItem(new Weapon("낡은 검", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 600, 2));
            Inventory.AddItem(new Armor("무쇠갑옷", ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.", 500, 5));
        }


        public Inventory Inventory = new Inventory();
        public string Chad { get; set; }
        public int Gold { get; set; }
        public int AddAttack { get; set; }
        public int AddDefense { get; set; }
        public Item? EquipWeapon { get; set; }
        public Item? EquipArmor { get; set; }
        public int MaxHealth { get; set; }

        public int PrevHealth { get; set; }
        public int PrevGold { get; set; }

        public int Exp { get; set; } // 추가: 경험치

        public int ExpNeeded { get; set; } // 추가: 다음 레벨까지 필요한 경험치

        public string PlayerInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Lv.{Level}\n{Name}({Chad})\n\n");
            sb.Append(AddAttack > 0 ? $"공격력: {Attack + AddAttack} (+{AddAttack})\n\n" : $"공격력: {Attack}\n\n");
            sb.Append(AddDefense > 0 ? $"방어력 : {Defense + AddDefense} (+{AddDefense})\n\n" : $"방어력: {Defense}\n\n");
            sb.Append($"체력: {Health} / {MaxHealth}\n\nGold: {Gold} G\n\n");
            sb.Append($"경험치: {Exp} / {ExpNeeded}\n\n");
            return sb.ToString();
        }

        public void EquipItem(int i)
        {
            Item target = Inventory.PeekItem(i);

            if (target.IsEquip)
            {
                target.IsEquip = false;
                AddAttack -= target.Attack;
                AddDefense -= target.Defense;
            }
            else
            {
                if (target.Type == ItemType.Weapon)
                {
                    if (EquipWeapon != null)
                    {
                        EquipWeapon.IsEquip = false;
                    }
                    EquipWeapon = target;
                }
                else
                {
                    if (EquipArmor != null)
                    {
                        EquipArmor.IsEquip = false;
                    }
                    EquipArmor = target;
                }
                target.IsEquip = true;
                AddAttack += target.Attack;
                AddDefense += target.Defense;
            }
        }
        /*public void EquipItem(int i)
        {
            Item target = Inventory.EquipItem(i);

            if (target.IsEquip)
            {
                if(target.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)target;
                    AddAttack += weapon.Attack;
                }
                else
                {
                    Armor armor = (Armor)target;
                    AddDefense += armor.Defense;
                }
            }
            else
            {
                if (target.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)target;
                    AddAttack -= weapon.Attack;
                }
                else
                {
                    Armor armor = (Armor)target;
                    AddDefense -= armor.Defense;
                }
            }
        }*/

        public void SellItem(int i)
        {
            Item sellitem = Inventory.PeekItem(i);

            if (sellitem.IsEquip)
            {
                EquipItem(i);
            }

            int price = (int)(sellitem.Price * 0.85f);
            Gold += price;
            DungeonGame.Instance.message += () => Console.WriteLine($"{sellitem.Name}을 {price} G 에 판매 하였습니다.");
            Inventory.DeleteItem(sellitem);


        }

        public void Rest()
        {
            if (Gold >= 500)
            {
                Gold -= 500;
                DungeonGame.Instance.message += () => Console.WriteLine("휴식을 완료했습니다.");
            }
            else
            {
                DungeonGame.Instance.message += () => Console.WriteLine("Gold 가 부족합니다.");
            }
        }

        public new void AttackUnit(Unit m)
        {
            Random rand = new Random();

            float errorFloat = (Attack + AddAttack) * 0.1f;
            int errorInt = (int)errorFloat;
            int errorDamage = errorInt < errorFloat ? errorInt + 1 : errorInt;

            int damage = rand.Next(Attack + AddAttack - errorDamage, Attack + AddAttack + errorDamage);

            DungeonGame.Instance.message += () => Console.WriteLine($"{Name}의 공격!");

            m.Attacked(damage);
        }

        public void GetExp(int amount)  // 경험치 얻는 메서드
        {
            Exp += amount;
            if (Exp >= ExpNeeded)
            {
                LevelUp();
                Exp = 0;
            }
        }

        private void LevelUp()  // 레벨 업
        {
            Level++;
            Exp -= ExpNeeded;
            ExpNeeded = CalculateExpNeeded();
            Attack += 1;
            Defense += 2;
            MaxHealth += 20;
            Health = MaxHealth;
        }

        private int CalculateExpNeeded() // 레벨 업에 필요한 경험치
        {
            return Level * 10;
        }
    }
}