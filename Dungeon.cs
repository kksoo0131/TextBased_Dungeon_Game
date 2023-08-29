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
            monsterList.Add(new Unit("미니언", 2, 10, 0, 15, 15));
            monsterList.Add(new Unit("대포미니언", 5, 10, 0, 25, 25));
            monsterList.Add(new Unit("공허충", 3, 10, 0, 10, 10));
        }

        public List<Unit> monsterList = new List<Unit>();

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
                sb.Append($"{i + 1} ");
                sb.Append(monsterList[i].MonsterInfo());
            }

            return sb.ToString();
        }
    }
}
