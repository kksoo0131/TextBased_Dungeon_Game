using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    [Serializable]
    public class Unit
    {
        public Unit()
        {

        }

        public Unit(string name, int level, int attack, int defense, int health, int maxHealth)
        {
            Init(name, level, attack, defense, health, maxHealth);
        }

        public void Init(string name, int level, int attack, int defense, int health, int maxHealth)
        {
            Name = name;
            Level = level;
            Attack = attack;
            Defense = defense;
            MaxHealth = maxHealth;
            Health = health;
            IsDead = false;
        }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        private int health;
        
        public int Health
        {
            get { return health; }
            set
            {
                health = Math.Min(value, MaxHealth);
            }
        }
        public int MaxHealth { get; set; }

        public bool IsDead { get; set; }
        public bool IsCritical()
        {
            Random rand = new Random();
            return rand.Next(0, 101) < 15 ? true : false;
        }

        public bool IsAvoid()
        {
            Random rand = new Random();
            return rand.Next(0, 101) < 10 ? true : false;

        }
        public int SetAttackPower(bool critical)
        {
            Random rand = new Random();

            float errorFloat = (Attack) * 0.1f;
            int errorInt = (int)errorFloat;
            int errorDamage = errorInt < errorFloat ? errorInt + 1 : errorInt;

            int damage = rand.Next(Attack - errorDamage, Attack + errorDamage);
            return critical ? (int)(damage * 1.6f) : damage;
        }

        public string MonsterInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Lv.{Level} {Name} ");
            if (IsDead)
            {
                sb.Append("Dead\n");
            }
            else
            {
                sb.Append($"HP {Health}\n");
            }

            return sb.ToString();
        }
        public void AttackUnit(Unit _unit)
        {

            bool result = IsCritical();
            int damage = SetAttackPower(result);

            StringBuilder sb = new StringBuilder();

            sb.Append($"{Name}의 공격!\nLv.{_unit.Level} {_unit.Name}을 공격했습니다. [데미지 : {damage}]");
            sb.Append(result ? $" - 치명타 공격!!\n" : "\n");

            DungeonGame.Instance.message.Append(sb);

            _unit.Attacked(damage);

        }
        public void Attacked(int i)
        {
            StringBuilder sb = new StringBuilder();

            if (IsAvoid())
            {
                sb.Append($"Lv.{Level} {Name} 를 공격했지만 아무일도 일어나지 않았습니다.\n");
            }
            else
            {
                sb.Append($"Lv.{Level} {Name} HP {Health} -> ");
                Health -= i;

                if (Health <= 0)
                {
                    sb.Append("Dead\n");
                    IsDead = true;
                    DungeonGame.Instance.dungeon.DeadCount += 1;
                }
                else
                {
                    sb.Append($"{Health}\n");
                }
            }




            DungeonGame.Instance.message.Append(sb);


        }
    }
    public class Wolf : Unit
    {
        public Wolf() : base("늑대", 3, 15, 5, 20, 20) { }
    }
    public class Chicken : Unit
    {
        public Chicken() : base("닭", 1, 5, 5, 10, 10) { }
    }
    public class WildBoar : Unit
    {
        public WildBoar() : base("멧돼지", 5, 10, 10, 30, 30) { }
    }

}