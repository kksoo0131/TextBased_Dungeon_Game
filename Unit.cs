using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    enum MonsterType
    {
        //여기에 유닛을 상속받은 몬스터들의 클래스이름을 추가.
    }
    [Serializable]
    public class Unit
    {
        public Unit(string name, int level, int attack, int defense, int health, int maxHealth)
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
        public int MaxHealth { get; set; }
        public int Health
        {
            get { return health; }
            set
            {
                health = Math.Min(value, MaxHealth);
            }
        }
        

        public bool IsDead { get; set; }
        
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
            Random rand = new Random();

            float errorFloat = (Attack) * 0.1f;
            int errorInt = (int)errorFloat;
            int errorDamage = errorInt < errorFloat ? errorInt + 1 : errorInt;

            int damage = rand.Next(Attack  - errorDamage, Attack  + errorDamage);

            DungeonGame.Instance.message += () => Console.WriteLine($"{Name}의 공격!\n Lv.{_unit.Level} {_unit.Name}을 공격했습니다. [데미지 : {damage}]\n");

            _unit.Attacked(damage);
        }
        public void Attacked(int i)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append($"Lv.{Level} {Name} HP {Health} -> ");
            Health -= i;

            if (Health <= 0) 
            {
                sb.Append("Dead");
                IsDead = true;
                DungeonGame.Instance.dungeon.DeadCount+=1;
            }
            else
            {
                sb.Append($"{Health}");
            }

            
            DungeonGame.Instance.message += () => Console.WriteLine(sb.ToString());

            
        }
    }
}
