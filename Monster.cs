using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    internal class Monster
    {
        public Monster(string name, int level, int attack, int defense, int health)
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
    }
}
