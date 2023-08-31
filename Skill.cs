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
    public class Skill
    {
        public string name;
        public string skillInfo;
        public int mp;
        public int targetCount;
        public float skillRate;
        public SkillType type;

        public Skill(string name, string skillInfo,float skillRate, int mp, int targetCount, SkillType type)
        {
            this.name = name;
            this.skillInfo = skillInfo;
            this.mp = mp;
            this.targetCount = targetCount;
            this.type = type;
            this.skillRate = skillRate;
        }


        // 스킬 마다 클래스로 생성
        // 플레이어의 Skill Ation에 등록해서 사용한다.
        // Skill은 스킬마다 대상 몬스터가 달라지니까 Unit의 List를 받는다.
        public virtual void Use(List<Unit> _units, int _attack) { }
    }
    [Serializable]
    public class LauraSkill1 : Skill
    {
        public LauraSkill1() : base("날카로운 꽃", "라우라가 지정한 적에게 한 번 채찍을 휘둘러 스킬 피해를 입힙니다.",1.5f, 10, 1, SkillType.Target) { }
        
        public override void Use(List<Unit> _units, int _attack)
        {
            // Scene에서 타겟 설정 다해서 player -> Skill.use로 넘김 Skill에서는 받은 타겟을 다처리하면됨

            int damage = (int)(_attack * skillRate);

            foreach (Unit unit in _units)
            {
                unit.Attacked(damage);
            }
        }
    }

    [Serializable]
    public class CathySkill1 : Skill
    {
        public CathySkill1() : base("동맥절제술", "캐시가 지정한 방향으로 빠르게 돌진하여 적에게 스킬 피해를 입힙니다.",2f , 10, 1, SkillType.Target) { }

        public override void Use(List<Unit> _units, int _attack)
        {
            // Scene에서 타겟 설정 다해서 player -> Skill.use로 넘김 Skill에서는 받은 타겟을 다처리하면됨

            int damage = (int)(_attack * skillRate);

            foreach (Unit unit in _units)
            {
                unit.Attacked(damage);
            }
        }
    }

    [Serializable]
    public class CathySkill2 : Skill
    {
        public CathySkill2() : base("엠퓨테이션", "캐시가 전방으로 톱을 날카롭게 휘둘러 두 명의 적에게 스킬 피해를 입힙니다.",2f, 20, 2, SkillType.Random) { }

        public override void Use(List<Unit> _units, int _attack)
        {
            // Scene에서 타겟 설정 다해서 player -> Skill.use로 넘김 Skill에서는 받은 타겟을 다처리하면됨

            int damage = (int)(_attack * skillRate);
            Random ran = new Random();

            int range = ran.Next( 0 , _units.Count -2);

            _units[range].Attacked(damage);
            _units[range+1].Attacked(damage);

        }
        
    }

    [Serializable]
    public class LauraSkill2 : Skill
    {
        public LauraSkill2() : base("황혼의 도둑", "라우라가 저지불가 상태가 되어 적에게 돌진합니다.", 1.5f, 15, 2, SkillType.Random) { }
 
        public override void Use(List<Unit> _units, int _attack)
        {
            // Scene에서 타겟 설정 다해서 player -> Skill.use로 넘김 Skill에서는 받은 타겟을 다처리하면됨

            int damage = (int)(_attack * skillRate);
            Random ran = new Random();

            int count = Math.Min(_units.Count, targetCount);

            for(int i =0; i< count; i++)
            {
                int randInt = ran.Next(0, _units.Count);
                
                _units[randInt].Attacked(damage);
              
                _units.RemoveAt(randInt);
            }
        }
    }
}
