using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    [Serializable]
    internal class Unit
    {
        public Unit(string name, int level, int attack, int defense, int health)
        {
            Name = name;
            Level = level;
            Attack = attack;
            Defense = defense;
            Health = health;
            IsDead = false;
        }

        public string Name { get; set; }
        public int Level { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
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

        public void AttackUnit(Unit m)
        {
            Random rand = new Random();

            float errorFloat = (Attack ) * 0.1f;
            int errorInt = (int)errorFloat;
            int errorDamage = errorInt < errorFloat ? errorInt + 1 : errorInt;

            int damage = rand.Next(Attack  - errorDamage, Attack  + errorDamage);

            DungeonGame.message += () => Console.WriteLine($"{Name}의 공격!");

            m.Attacked(damage);
        }

        public void Attacked(int i)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append($"Lv.{Level} {Name}을 맞췄습니다. [데미지 : {i}]\n");
            sb.Append($"Lv.{Level} {Name} HP {Health} -> ");
            Health -= i;

            if (Health <= 0) 
            {
                sb.Append("Dead");
                IsDead = true;
                DungeonGame.dungeon.DeadCount++;
            }
            else
            {
                sb.Append($"{Health}");
            }

            
            DungeonGame.message += () => Console.WriteLine(sb.ToString());

            
        }
    }
}
