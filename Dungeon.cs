using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    internal class Dungeon
    {
        public Dungeon()
        {

            Init();

        }

         List<Unit> monsterList = new List<Unit>();

        public void Init()
        {
            monsterList.Add(new Wolf());
            monsterList.Add(new Chicken());
            monsterList.Add(new Unit("공허충", 3, 10, 0, 10, 10));
            DeadCount = 0;
            Result = true;
        }
        public bool Result { get; set; } 
        public int DeadCount { get; set; }
        public Unit GetUnit(int index)
        {
            return monsterList[index];
        }
        public List<Unit> GetUnitList()
        {
            return monsterList;
        }
        public int Count()
        {
            return monsterList.Count;
        }        
        public bool DungeonClear()
        {
            return DeadCount >= Count();
        }
        public string MonsterListInfo()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Unit m in monsterList)
            {
                sb.Append(m.MonsterInfo());
            }
                
            return sb.ToString();
        }
        public string MonsterSelectInfo()
        {
            StringBuilder sb = new StringBuilder();

            for(int i =0; i< monsterList.Count; i++) 
            {
                sb.Append($"{i + 1}. ");
                sb.Append(monsterList[i].MonsterInfo());
            }

            return sb.ToString();
        }

        public void Clear()
        {
            monsterList.Clear();
        }
    }
}
