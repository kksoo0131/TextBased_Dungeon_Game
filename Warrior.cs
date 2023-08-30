using System.Text;
using System.Linq;


namespace TextBased_Dungeon_Game
{
    [Serializable]
    class Warrior : Unit
    {
        public Warrior() : base("전사전사전사", 1, 10, 5, 100, 100)
        {
            Chad = "전사";
            Gold = 1500;
            MaxHealth = 100;
            MaxMP = 50;
            MP = 50;
            Exp = 0;
            ExpNeeded = CalculateExpNeeded();
            SkillCount = 0;


            Inventory.AddItem(new Weapon("낡은 검", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 600, 2));
            Inventory.AddItem(new Armor("무쇠갑옷", ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.", 500, 5));
            potionInventory.AddPotion(new Potion("체력 회복 포션", ItemType.HpPotion, "소량의 체력(30)을 회복하는 포션. 딸기맛이다!", 50, 30, 0, 3));
            potionInventory.AddPotion(new Potion("마나 회복 포션", ItemType.MpPotion, "소량의 마나(15)를 회복하는 포션. 소다맛!", 50, 0, 15, 2));

            AddSkill(new AlphaStrike());
            AddSkill(new DoubleStrike());
        }

        public PotionInventory potionInventory = new PotionInventory();

        public Inventory Inventory = new Inventory();

        public Skill[] skillList = new Skill[3];

        public List<Unit> targetList = new List<Unit>();
        public int SkillCount { get; set; }
        public string Chad { get; set; }
        public int MP { get; set; }
        public int MaxMP { get; set; }
        public int Gold { get; set; }
        public Item? EquipWeapon { get; set; }
        public Item? EquipArmor { get; set; }  
        public int PrevHealth { get; set; } // 던전 입장전의 체력, 골드를 저장해서 클리어 후 비교하는 용도
        public int PrevGold { get; set; }
        public int Exp { get; set; } // 추가: 경험치
        public int ExpNeeded { get; set; } // 추가: 다음 레벨까지 필요한 경험치

        public string PlayerInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Lv.{Level}\n{Name}({Chad})\n\n");
            sb.Append(EquipWeapon != null ? $"공격력: {Attack + EquipWeapon.Attack} (+{EquipWeapon.Attack})\n\n" : $"공격력: {Attack}\n\n");
            sb.Append(EquipArmor != null ? $"방어력 : {Defense + EquipArmor.Defense} (+{EquipArmor.Defense})\n\n" : $"방어력: {Defense}\n\n");
            sb.Append($"HP: {Health} / {MaxHealth}\n\nMP: {MP} / {MaxMP}\n\nGold: {Gold} G\n\n");
            sb.Append($"EXP: {Exp} / {ExpNeeded}\n\n");
            return sb.ToString();
        }

        public string PlayerSkillInfo()
        {
            StringBuilder sb = new StringBuilder();

            for(int i =0; i< SkillCount; i++)
            {
                Skill skill = skillList[i];
                sb.Append($"{i + 1}. {skill.name} - MP {skill.mp}\n   {skill.skillInfo}\n");
            }

            return sb.ToString();
        }
        public void EquipItem(int i)
        {
            Item target = Inventory.PeekItem(i);

            if (target.IsEquip)
            {
                target.IsEquip = false;
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
                else if(target.Type == ItemType.Armor)
                {
                    if (EquipArmor != null)
                    {
                        EquipArmor.IsEquip = false;
                    }
                    EquipArmor = target;
                }
                target.IsEquip = true;
            }
        }

        public void DrinkPotion(int i)
        {
            Item target = potionInventory.PeekPotion(i);



            if (target.Type == ItemType.HpPotion && target.Quantity != 0)
            {
                if (Health != MaxHealth)
                {
                    target.Quantity--;
                    Health += target.HpRecovery;
                }
                else
                {
                    Console.WriteLine("체력이 충분합니다.");
                    Thread.Sleep(500);
                }

            }
            else if (target.Type == ItemType.MpPotion && target.Quantity != 0)
            {
                if (MP != MaxMP)
                {
                    target.Quantity--;
                    MP += target.MpRecovery;
                }
                else
                {
                    Console.WriteLine("마나가 충분합니다.");
                    Thread.Sleep(500);
                }
            } 
            else
            {
                Console.WriteLine("포션이 없습니다.");
                Thread.Sleep(500);
            }
        }
        
        //public void GetHpPotion(int i)
        //{
        //    Item target = potionInventory.PeekPotion(i);

        //    if (target.Type == ItemType.HpPotion)
        //    {
        //        target.Quantity++;
        //    }
        //}
        //public void GetMpPotion(int i)
        //{
        //    Item target = potionInventory.PeekPotion(i);

        //    if (target.Type == ItemType.MpPotion)
        //    {
        //        target.Quantity++;
        //    }
        //}
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

        public int SetAttackPower()
        {
            Random rand = new Random();

            int AddAttack = EquipWeapon == null ? 0 : EquipWeapon.Attack;

            float errorFloat = (Attack + AddAttack) * 0.1f;
            int errorInt = (int)errorFloat;
            int errorDamage = errorInt < errorFloat ? errorInt + 1 : errorInt;

            return rand.Next(Attack + AddAttack - errorDamage, Attack + AddAttack + errorDamage);
        }
        public new void AttackUnit(Unit m)
        {
            DungeonGame.Instance.message += () => Console.WriteLine($"{Name}의 공격!");

            m.Attacked(SetAttackPower());
        }
        public void AddSkill(Skill skill)
        {
            if (SkillCount < 3)
            {
                skillList[SkillCount++] = skill;
            }
            else
            {
                Console.WriteLine("더 이상 스킬을 배울수 없습니다.");
            }

        }
        public void UseSkill(int index)
        {
            
            // Scene에서 _units를 생성해서 전달하고 SKill에서는 그냥 전부 맞게하면될듯
            if (index >=0 && index < SkillCount)
            {
                DungeonGame.Instance.message += () => Console.WriteLine($"{Name}의 {skillList[index].name}!");
                skillList[index].Use(targetList, SetAttackPower());
                MP -= skillList[index].mp;
                targetList.Clear();
            }
            else
            {
                DungeonGame.Instance.message += () => Console.WriteLine("잘못된 입력 입니다.");
            }

            
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
            Console.WriteLine("축하합니다! 레벨업!");
        }
        private int CalculateExpNeeded() // 레벨 업에 필요한 경험치
        {
            return Level * 10;
        }

        
    }
}