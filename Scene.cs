using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    public enum SceneType
    {    
        StartScene,
        // 스타트 씬 메뉴
        StatusScene,
        InventoryScene,
        ShopScene,
        DungeonEnterScene,
        RestScene,
        SaveRoadScene,
        //나머지
        /* DungeonClearScene,
         DungeonFailScene,*/
        PlayerPhaseScene,
        MonsterPhaseScene,
        BattleResultScene,
        //마지막은 EndPoint여야함
        EndPoint,
    }
    class Scene
    {   // 인터페이스에서는 모든 멤버가 암시적으로 public이고,
        // 클래스나 메서드에서는 모든 멤버가 암시적으로 internal이다.

        //Type 리플렉션을 사용할때 인터페이스로는 형변환이 불가능하기 때문에
        //인스턴스를 생성할수 있는 클래스로 변경하고 메서드를 가상메서드로 선언
        public Scene()
        {
            _player = DungeonGame.Instance.player; 
        }
        protected Warrior _player;
        protected int[] options;

        public virtual int DrawScene() { return 0; }
        public virtual int InputKey(int[] options)
        {
            int input;

            while (!int.TryParse(Console.ReadLine(), out input) || !options.Contains(input))
            {
                Console.WriteLine("잘못된 입력입니다");
            }

            return input;
        }
        public virtual int[] MakeOption(int count)
        {
            int[] array = new int[count + 1];
            for (int i = 0; i < count + 1; i++)
            {
                array[i] = i;
            }

            return array;
        }
        public void DrawUI()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine("l");
                Console.SetCursorPosition(33, i);
                Console.WriteLine("l");
                Console.SetCursorPosition(Console.WindowWidth-1, i);
                Console.WriteLine("l");
            }

            for (int i =0;i< Console.WindowWidth/2; i++)
            {
                sb.Append("ㅡ");
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(sb.ToString());
            Console.SetCursorPosition(0, Console.WindowHeight - 16);
            Console.Write(sb.ToString());
            Console.SetCursorPosition(0, Console.WindowHeight -1);
            Console.Write(sb.ToString());

            
        }
        public void WriteTopMessage(string str)
        {
            string[] strArray = str.Split('\n');
            int xPos = 3;
            int yPos = 3;
            
            for(int i =0; i< strArray.Length; i++)
            {
                Console.SetCursorPosition(xPos, yPos++);
                Console.WriteLine(strArray[i]);
            }
        }
        public void WriteSelectMessage(string str)
        {
            string[] strArray = str.Split('\n');
            int xPos = 3;
            int yPos = Console.WindowHeight - 16;

            for (int i = 0; i < strArray.Length; i++)
            {
                Console.SetCursorPosition(xPos, yPos+=2);
                Console.WriteLine(strArray[i]);
            }
        }
        public void WriteLeftMessage(string str)
        {
            string[] strArray = str.Split('\n');
            int xPos = 3;
            int yPos = 1;

            for (int i = 0; i < strArray.Length; i++)
            {
                Console.SetCursorPosition(xPos, yPos += 1);
                Console.WriteLine(strArray[i]);
            }
        }
        public void WriteMessage(string str)
        {
            str += DungeonGame.Instance.PrintMessage();
            string[] strArray = str.Split('\n');
            int xPos = Console.WindowWidth / 2 - 23;
            int yPos = Console.WindowHeight - 16;

            for (int i = 0; i < strArray.Length; i++)
            {
                Console.SetCursorPosition(xPos, yPos+=2);
                Console.WriteLine(strArray[i]);
            }
            Console.SetCursorPosition(xPos, yPos+=2);
        }
        public void WriteRightMessage(string str)
        {
            string[] strArray = str.Split('\n');
            int xPos = Console.WindowWidth / 2 - 23;
            int yPos = 1;

            for (int i = 0; i < strArray.Length; i++)
            {
                Console.SetCursorPosition(xPos, yPos += 2);
                Console.WriteLine(strArray[i]);
            }
        }


    }


    class StartScene : Scene
    {
        int[] options;
        public StartScene() : base() { }    
        public override int DrawScene()
        {         
            options = new int[] { 1, 2, 3, 4, 5 };
            Console.Clear();
/*
            Console.WriteLine(MakeText());
                      Console.WriteLine();
            for (int i = 0; i <= 255; i += 5)
            {
                string s = "──────────────────────────────────────────────────────";
                Console.Write($"\u001b[38;2;255;{i};80m{s.Substring(i / 5, 1)}");
            }
            */
            DrawUI();
            WriteText();
            return InputKey(options);


        }
        public void WriteText()
        {

            WriteSelectMessage("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n6. 저장하기 & 기불러오기");
            WriteMessage("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n원하시는 행동을 입력해주세요.");
/*
            SoundPlayer.StopSound();
            SoundPlayer.Bgm("");
            Console.WriteLine();
            for (int i = 0; i <= 255; i += 8)
            {
                string s = "┌──────────────────────────────┐";
                Console.Write($"\u001b[38;2;255;{i};80m{s.Substring(i / 8, 1)}");      
            }
            Console.WriteLine();

            for (int i = 0; i <= 255; i += 8)
            {
                string s = "│  WELCOME TO SPARTA VILLAGE   │";
                Console.Write($"\u001b[38;2;255;{i};100m{s.Substring(i / 8, 1)}");
            }
            for (int i = 255; i >= 0; i -= 13)
            {
                string s = "■■■■■■■■■■■■■■■■■■■■■";
                Console.Write($"\u001b[38;2;0;{i};150m{s.Substring(i / 13, 1)}");
            }
            Console.WriteLine();
            for (int i = 0; i <= 255; i += 8)
            {
                string s = "└──────────────────────────────┘";
                Console.Write($"\u001b[38;2;255;{i};80m{s.Substring(i / 8, 1)}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();

            return "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다. \n\n1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n\n원하시는 행동을 입력해주세요.";
*/
        }

    }
    class StatusScene : Scene
    {
        public StatusScene() : base() { }
        public override int DrawScene()
        {
            options = new int[] { 0 };
            Console.Clear();
            DrawUI();
            WriteText();
            return InputKey(options);
        }

        public void WriteText()
        {
            WriteSelectMessage("0. 나가기");
            WriteLeftMessage($"{DungeonGame.Instance.player.PlayerInfo()}");
            WriteMessage("상태 보기\n캐릭터의 정보가 표시됩니다\n원하시는 행동을 입력해주세요.");
        }
    }
    class InventoryScene : Scene
    {   
        Inventory _inventory;
        public override int DrawScene()
        {
            _inventory = _player.Inventory;
            options = new int[] { 0, 1, 2, 3, 4, 5};
            Console.Clear();
            DrawUI();
            WriteText();

            switch (InputKey(options))
            {
                
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    return EquipmentInfo();
                case 2:
                    _inventory.SortInventory(SortingInventory.Name);
                    return (int)SceneType.InventoryScene;
                case 3:
                    _inventory.SortInventory(SortingInventory.Equipment);
                    return (int)SceneType.InventoryScene;
                case 4:
                    _inventory.SortInventory(SortingInventory.Attack);
                    return (int)SceneType.InventoryScene;
                case 5:
                    _inventory.SortInventory(SortingInventory.Defense);
                    return (int)SceneType.InventoryScene;
                default:
                    return (int)SceneType.StartScene;
            }
        }
        public int EquipmentInfo()
        {
            Console.Clear();
            WriteEquipText();

            // options를 아이템 Count갯수만큼 추가해야됨
            options = new int[_inventory.Count() + 1];
            options[0] = 0;
            for (int i = 1; i <= _inventory.Count(); i++)
            {
                options[i] = i;
            }

            int index = InputKey(options);
            switch (index)
            {
                case 0:
                    return (int)SceneType.InventoryScene;
                default:
                    // 해당 장비가 장착중 -> 장착 해제
                    // 장착 해제 -> 장착중으로 변경함
                    _player.EquipItem(index - 1);
                    // DungeonGame.Instance.PlayerSave();
                    return EquipmentInfo();
            }
        }

        public void WriteText()
        {
            WriteLeftMessage($"{_inventory.MakeItemList()}");
            WriteSelectMessage("1. 장착 관리\n2. 이름\n3. 장착순\n4. 공격력\n5. 방어력\n0. 나가기");
            WriteMessage("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n원하시는 행동을 입력해주세요.");
        }

        public void WriteEquipText()
        {
            WriteLeftMessage($"{_inventory.MakeEquipList()}");
            WriteSelectMessage("0. 나가기");
            WriteMessage("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n원하시는 행동을 입력해주세요.");
        }
    }
    class ShopScene : Scene
    {
        Shop _shop;
        public override int DrawScene()
        {
            _shop = DungeonGame.Instance.shop;
            options = new int[] { 0, 1, 2};
            Console.Clear();
            DrawUI();
            WriteText();

            switch (InputKey(options))
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    SoundPlayer.StopSound();
                    return BuyItem();
                case 2:
                    return SellItem();
                default:
                    return (int)SceneType.ShopScene;
            }
        }
        public int BuyItem()
        {
            Console.Clear();
            WriteBuyText();
            DungeonGame.Instance.PrintMessage();
            // options를 아이템 Count갯수만큼 추가해야됨
            options = new int[_shop.Count() + 1];
            options[0] = 0;
            for (int i = 1; i <= _shop.Count(); i++)
            {
                options[i] = i;
            }

            int index = InputKey(options);
            switch (index)
            {
                case 0:
                    return (int)SceneType.ShopScene;
                default:
                    Item? item = _shop.BuyItem(index - 1);
                    if (item != null)
                    {
                        //DungeonGame.Instance.PlayerSave();
                        //DungeonGame.Instance.ShopSave();
                        _player.Inventory.AddItem(item);
                    }

                    return BuyItem();
            }
        }
        public int SellItem()
        {
            Console.Clear();
            WriteSellText();
            DungeonGame.Instance.PrintMessage();

            // options를 아이템 Count갯수만큼 추가해야됨
            options = new int[_player.Inventory.Count() + 1];
            options[0] = 0;
            for (int i = 1; i <= _player.Inventory.Count(); i++)
            {
                options[i] = i;
            }

            int index = InputKey(options);
            switch (index)
            {
                case 0:
                    return (int)SceneType.ShopScene;
                default:
                    _player.SellItem(index - 1);
                    //DungeonGame.Instance.PlayerSave();
                    return SellItem();
            }
        }

        public void WriteText()
        {
            WriteLeftMessage($"{_shop.MakeItemList()}");
            WriteSelectMessage("1. 아이템 구매\n2. 아이템 판매\n0. 나가기");
            WriteMessage($"상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{_player.Gold}\n원하시는 행동을 입력해주세요.");
        }

        public void WriteBuyText()
        {
            WriteLeftMessage($"{_shop.MakeShopList()}");
            WriteSelectMessage("0. 나가기");
            WriteMessage($"상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{_player.Gold}\n원하시는 행동을 입력해주세요.");
        }

        public void WriteSellText()
        {
            WriteLeftMessage($"{_player.Inventory.MakeSellList()}");
            WriteSelectMessage("0. 나가기");
            WriteMessage($"상점 - 아이템 판매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{_player.Gold}\n원하시는 행동을 입력해주세요.");
        }

    }
    class DungeonEnterScene : Scene
    {
        public DungeonEnterScene() : base() { }
        public override int DrawScene()
        {
            options = new int[] { 0, 1};
            Console.Clear();
            DrawUI();
            WriteText();

            switch (InputKey(options))
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:                    
                    return (int)SceneType.PlayerPhaseScene;
                default:
                    return (int)SceneType.StartScene;
            }
        }
        public void WriteText()
        {
            WriteLeftMessage($"1. 쉬운 던전      | 방어력 5 이상 권장");
            WriteSelectMessage("0. 나가기");
            WriteMessage("던전입장\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n원하시는 행동을 입력해주세요");
        }

     
    }
    class RestScene : Scene
    {
        public RestScene() : base() { }
        public override int DrawScene()
        {
            options = new int[] { 0, 1 };
            Console.Clear();
            DrawUI();
            WriteText();
            DungeonGame.Instance.PrintMessage();
            //DungeonGame.Instance.PlayerSave();
            if (InputKey(options) == 1)
            {
                _player.Rest();
                return (int)SceneType.RestScene;
            }
            return (int)SceneType.StartScene;
        }

        public void WriteText()
        {
            WriteSelectMessage("1.휴식하기\n0.나가기");
            WriteMessage($"휴식하기\n500 G 를 내면 체력을 회복할 수 있습니다.\n보유 골드 : {_player.Gold} G\n원하시는 행동을 입력해주세요.");
        }


    }
    class PlayerPhaseScene : Scene 
    {
        Dungeon dungeon;
        public PlayerPhaseScene() : base() {  }
        public override int DrawScene()
        {
            dungeon = DungeonGame.Instance.dungeon;
            return PlayerPhase();
        }

        public int PlayerPhase()
        {
            Console.Clear();
            DrawUI();
            WriteText();
            DungeonGame.Instance.PrintMessage();

            options = new int[] { 1, 2 };
            switch (InputKey(options))
            {
                case 1:
                    return PlayerPhaseAttack();
                case 2:
                    return PlayerSelectSkill();
                default:
                    return (int)SceneType.PlayerPhaseScene;
            }

        }
       
        public int PlayerPhaseAttack()
        {
            //몬스터의 수 만큼 options를 추가
            // 플레이어가 몬스터 번호를 입력하면
            // 몬스터 리스트의 해당 인덱스에 있는 몬스터가
            // 플레이어가 주는 데미지를 받음

            Console.Clear();
            DrawUI();
            WriteBattleText();
            DungeonGame.Instance.PrintMessage();

            int key = InputKey(MakeOption(dungeon.Count()));
            switch (key)
            {
                case 0:
                    return (int)SceneType.PlayerPhaseScene;
                default:
                    Unit unit = dungeon.GetUnit(key - 1);
                    if (unit.IsDead == true)
                    {

                        DungeonGame.Instance.message.Append("이미 죽은 몬스터 입니다.\n");
                        return PlayerPhaseAttack();
                    }
                    _player.AttackUnit(unit);
                    return AttackResult();
                    // 몬스터 선택에 따른 결과 출력

            }
        }

        public int PlayerSelectSkill()
        {
            Console.Clear();
            DrawUI();
            WriteSkillText();
            DungeonGame.Instance.PrintMessage();

            int key = InputKey(MakeOption(_player.SkillCount));
            switch (key)
            {
                case 0:
                    return (int)SceneType.PlayerPhaseScene;
                default:
                    if (_player.MP < _player.skillList[key - 1].mp)
                    {
                        DungeonGame.Instance.message.Append("MP가 부족합니다!\n");
                        return PlayerSelectSkill();
                    }
                    if (_player.skillList[key-1].type == SkillType.Random)
                    {
                        foreach (Unit i in dungeon.GetUnitList())
                        {
                            if (!i.IsDead)
                            {
                                _player.targetList.Add(i);
                            }
                        }
                        _player.UseSkill(key - 1);
                        return AttackResult();
                    }
                    else
                    {
                        return SelectSkillTarget(key - 1, _player.skillList[key - 1].targetCount);
                    }
                    
            }
        }
    }

    class SaveRoadScene : Scene
    {
        int[] options = { 0, 1, 2 };

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());
            
            switch(InputKey(options)) 
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    return Save();
                case 2:
                    return Load();
                default:
                    return (int)SceneType.SaveRoadScene;

            }
        }

        public int Save()
        {
            Console.Clear();
            Console.WriteLine(MakeSaveText());
            DungeonGame.dataManager.PlayerSave();
            //DungeonGame.dataManager.InventorySave();

            return DrawScene();
        }

        public int Load()
        {
            Console.Clear();
            Console.WriteLine(MakeLoadText());
            DungeonGame.dataManager.PlayerLoad();
            //DungeonGame.dataManager.InventoryLoad();

            return DrawScene();
        }

        public string MakeText()
        {
            return "저장하거나 불러오시겠습니까? \n\n0. 나가기\n1. 저장하기\n2. 불러오기\n\n원하시는 행동을 입력해주세요.";
        }

        public string MakeSaveText()
        {
            return "저장하기";
        }

        public string MakeLoadText()
        {
            return "불러오기";
        }
    }

    class BattleScene : Scene 
    {
        public int SelectSkillTarget(int index, int count)
        {
            Console.Clear();
            DrawUI();
            WriteSkillTargetText();
            DungeonGame.Instance.PrintMessage();

            int key = InputKey(MakeOption(dungeon.Count()));
            switch (key)
            {
                case 0:
                    return PlayerSelectSkill();
                default:
                    Unit _unit = dungeon.GetUnit(key-1);
                    if (_player.targetList.Contains(_unit))
                    {
                        DungeonGame.Instance.message.Append("이미 선택한 대상입니다.\n");
                        return SelectSkillTarget(index, count);
                    }
                    else if (_unit.IsDead == true)
                    {
                        DungeonGame.Instance.message.Append("이미 죽은 몬스터 입니다.\n");
                        return PlayerPhaseAttack();
                    }

                    _player.targetList.Add(_unit);
                    if (--count > 0) return SelectSkillTarget(index, count);
                    _player.UseSkill(index);
                    // 스킬을 사용하기위해 내가 선택한 target들이 저장되야함
                    return AttackResult();
                    // 선택된 타겟을 저장하면서 count가 0이 될때까지 계속 선택
            }


        }
        public int AttackResult()
        { 
            
            options = new int[] { 0 };
            Console.Clear();
            DrawUI();
            WriteAttackResultText();



            switch (InputKey(options))
            {
                default:
                    return dungeon.DungeonClear() ? (int)SceneType.BattleResultScene : (int)SceneType.MonsterPhaseScene;
            }
        }

        public void WriteText()
        {
            WriteLeftMessage($"[내정보]\n\n{_player.PlayerInfo()}");
            WriteSelectMessage("1. 공격\n\n2. 스킬");
            WriteRightMessage($"{dungeon.MonsterListInfo()}");
            WriteMessage("Battle!!\n");
        }

        public void WriteAttackResultText()
        {
            WriteLeftMessage($"[내정보]\n\n{_player.PlayerInfo()}");
            WriteSelectMessage("0. 다음");
            WriteRightMessage($"{dungeon.MonsterSelectInfo()}");
            WriteMessage("Battle!!\n");
        }
        public void WriteBattleText()
        {
            WriteLeftMessage($"[내정보]\n\n{_player.PlayerInfo()}");
            WriteSelectMessage("0. 취소");
            WriteRightMessage($"{dungeon.MonsterSelectInfo()}");
            WriteMessage("Battle!!\n");
        }

        public void WriteSkillText()
        {
            WriteLeftMessage($"[내정보]\n\n{_player.PlayerInfo()}");
            WriteSelectMessage($"{_player.PlayerSkillInfo()}0.취소");
            WriteRightMessage($"{dungeon.MonsterListInfo()}");
            WriteMessage("Battle!!\n");
        }

        public void WriteSkillTargetText()
        {
            WriteLeftMessage($"[내정보]\n\n{_player.PlayerInfo()}");
            WriteSelectMessage($"0.취소");
            WriteRightMessage($"{dungeon.MonsterSelectInfo()}");
            WriteMessage("Battle!!\n");
        }

    }
    class MonsterPhaseScene : Scene
    {
        Dungeon dungeon;
        public MonsterPhaseScene() : base() { }

        public override int DrawScene()
        {
            dungeon = DungeonGame.Instance.dungeon;
            return MonsterPhase(0);
        }

        public int MonsterPhase(int i)
        {
            options = new int[] { 0 };

            if (dungeon.DungeonClear())
            {
                return (int)SceneType.BattleResultScene;
            }

            while (i < dungeon.Count() && dungeon.GetUnit(i).IsDead)
            {
                i++;
            }

            if(i < dungeon.Count()) 
            {
                Console.Clear();
                DrawUI();
                dungeon.GetUnit(i).AttackUnit(_player);
                WriteText();

                switch (InputKey(options))
                {
                    default:
                        if (_player.Health <= 0)
                        {
                            dungeon.Result = false;
                            return (int)SceneType.BattleResultScene;
                        }
                        return ++i < dungeon.Count() ? MonsterPhase(i) : (int)SceneType.PlayerPhaseScene;
                }
            }

            return (int)SceneType.PlayerPhaseScene;
            
        }

  
        public void WriteText()
        {
            WriteLeftMessage($"[내정보]\n\n{_player.PlayerInfo()}");
            WriteSelectMessage("0. 다음");
            WriteRightMessage($"{dungeon.MonsterListInfo()}");
            WriteMessage("Battle!!\n");
        }
    }
    class BattleResultScene : Scene
    { 

        Dungeon dungeon;
        public BattleResultScene() { }
        public override int DrawScene()
        {
            dungeon = DungeonGame.Instance.dungeon;
            return BattleResult(dungeon.Result);
        }

        public int BattleResult(bool _result)
        {
            options = new int[] { 0 };
            Console.Clear();

            if (_result)
            {
                WriteVictoryText();
                _player.GetExp(dungeon.Count() * 5);  // 몬스터 한 마리당 5의 경험치
                _player.Gold += 300;
            }
            else
            {
                WriteLoseText();
            }

            dungeon.Clear();
            dungeon.Init();
            switch (InputKey(options))
            {
                case 0:
                    return (int)SceneType.StartScene;
                default:
                    return (int)SceneType.StartScene;
            }

        }

        public void WriteVictoryText()
        {
            WriteLeftMessage($"[던전 결과]\n몬스터 {dungeon.Count()}마리를 잡았습니다! 경험치 {dungeon.Count() * 5} 증가!\n");
            WriteSelectMessage("0. 다음");
            WriteMessage("[보상 정산]\n골드 + 300 G\n원하시는 행동을 입력해주세요");

        }

        public void WriteLoseText()
        {
            WriteLeftMessage($"{_player.PlayerInfo()}");
            WriteSelectMessage("0. 다음");
            WriteRightMessage($"Baltte!! - Result\nLose");
            WriteMessage("[보상 정산]\n골드 + 300 G\n원하시는 행동을 입력해주세요");
        }
    }
}
