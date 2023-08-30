using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    public enum SkillType
    {
        Target,
        Random,
    }

    [Serializable]
    public abstract class Skill
    {
        public string name;
        public string skillInfo;
        public int mp;
        public int targetCount;
        public SkillType type;

        public Skill(string name, string skillInfo, int mp, int targetCount, SkillType type)
        {
            this.name = name;
            this.skillInfo = skillInfo;
            this.mp = mp;
            this.targetCount = targetCount;
            this.type = type;
        }


        // 스킬 마다 클래스로 생성
        // 플레이어의 Skill Ation에 등록해서 사용한다.
        // Skill은 스킬마다 대상 몬스터가 달라지니까 Unit의 List를 받는다.
        public abstract void Use(List<Unit> _units, int _attack);
    }
    [Serializable]
    public class AlphaStrike : Skill
    {
        public AlphaStrike() : base("알파 스트라이크", "공격력 * 2로 하나의 적을 공격합니다.", 10, 1, SkillType.Target) { }
        

        public override void Use(List<Unit> _units, int _attack)
        {
            // Scene에서 타겟 설정 다해서 player -> Skill.use로 넘김 Skill에서는 받은 타겟을 다처리하면됨

            int damage = (int)(_attack * 2f);

            foreach (Unit unit in _units)
            {
                unit.Attacked(damage);
            }
        }
    }
    [Serializable]
    public class DoubleStrike : Skill
    {
        public DoubleStrike() : base("더블 스트라이크", " * 1.5로 2명의 적을 랜덤으로 공격합니다.", 15, 2, SkillType.Random) { }
 
        public override void Use(List<Unit> _units, int _attack)
        {
            // Scene에서 타겟 설정 다해서 player -> Skill.use로 넘김 Skill에서는 받은 타겟을 다처리하면됨

            int damage = (int)(_attack * 1.5f);
            Random ran = new Random();

            int count = Math.Max(_units.Count, targetCount);
            for(int i =0; i< targetCount; i++)
            {
                int randInt = ran.Next(0, _units.Count);
                
                _units[randInt].Attacked(damage);
              
                _units.RemoveAt(randInt);
            }
        }
    }
}
