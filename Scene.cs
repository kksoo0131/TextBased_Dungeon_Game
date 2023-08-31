using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
        CreateCharacterScene,
        //마지막은 EndPoint여야함
        EndPoint,
    }
    class Scene
    {   
        protected Player _player;
        protected int[] options;
        protected string path = @"C:\Users\Kks\source\repos\TextBased_Dungeon_Game\.vscode";
        public virtual void SceneInit() 
        {
            _player = DungeonGame.Instance.player;
            Console.Clear();
            DrawUI();
        }

        public void SoundPlay()
        {
            Sound2.Play($"{path}\\Dungeon.mp3", true, false);
            Sound2.Play($"{path}\\Bgm.mp3", true, true);
        }
        public virtual int DrawScene() { return 0; }
        public virtual int InputKey(int[] options)
        {
            int input;
            int x = Console.GetCursorPosition().Left;
            int y = Console.GetCursorPosition().Top;

            while (!int.TryParse(Console.ReadLine(), out input) || !options.Contains(input))
            {
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine("잘못된 입력입니다");
                Console.SetCursorPosition(x, y + 1);
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
            Console.SetCursorPosition(0, 0);
            Console.Write(MakeVerticalLine());
            Console.SetCursorPosition(0, 0);
            Console.Write(HorizontalLine());
            Console.ResetColor();
        }
        public string MakeLogo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            sb.Append("\n");

            //for (int i = 0; i <= 255; i += 8)
            //{
            //    string s = "┌──────────────────────────────┐";
            //    sb.Append($"\u001b[38;2;255;{i};80m{s.Substring(i / 8, 1)}");
            //}
            sb.Append("\n");
            for (int i = 0; i <= 255; i += 7)
            {
                string s = "       ◆ WELCOME TO SPARTA VILLAGE\n    ";
                sb.Append($"\u001b[38;2;255;{i};100m{s.Substring(i / 7, 1)}");
            }         
            for (int i = 0; i <= 255; i += 11)
            {
                string s = " ◆ 스파르타 마을에 오신걸 환영합니다.   ";
             
                sb.Append($"\u001b[38;2;255;{i};80m{s.Substring(i / 11, 1)}");
            }
            sb.Append("\n");
            //for (int i = 255; i >= 0; i -= 8)
            //{
            //    string s = "   EGALLIV ATRAPS OT EMOCLEW  -  ";
            //    sb.Append($"\u001b[38;2;0;{i};150m{s.Substring(i / 8, 1)}");
            //}      
            sb.Append("\n");
            sb.Append("\n");

            return sb.ToString();
        }
        public string MakeVerticalLine()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Console.WindowHeight-2; i++)
            {
                int posX = 0;
                sb.Append($"\u001b[38;2;0;200;{255 / Console.WindowHeight * i};80m◆");
                while(posX++ < Console.WindowWidth / 2 - 30)
                {
                    sb.Append(' ');
                }    
                sb.Append($"\u001b[38;2;0;200;{255 / Console.WindowHeight * i};80m◆");
                while (posX++ < Console.WindowWidth-6)
                {
                    sb.Append(' ');
                }
                sb.Append($"\u001b[38;2;0;0;200;{20 + i / Console.WindowHeight * i };80m◆");
                sb.Append("\n");
            }

            return sb.ToString();
        }
        public string HorizontalLine()
        {
            StringBuilder sb = new StringBuilder();
            int posY = 0;
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                sb.Append($"\u001b[38;2;0;100;{255 / Console.WindowWidth * i};80m■");
            }
            while (posY++ < Console.WindowHeight - 18)
            {
                sb.Append("\n");
            }
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                sb.Append($"\u001b[38;2;0;100;{255 / Console.WindowWidth * i};80m■");
            }
            while (posY++ < Console.WindowHeight - 3)
            {
                sb.Append("\n");
            }
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                sb.Append($"\u001b[38;2;0;100;{255 / Console.WindowWidth * i};80m■");
            }

            return sb.ToString();
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
            int yPos = 0;

            for (int i = 0; i < strArray.Length; i++)
            {
                Console.SetCursorPosition(xPos, yPos += 2);
                Console.WriteLine(strArray[i]);
            }
        }

    }
    class CreateCharacterScene : Scene
    {
        public override int DrawScene()
        {
            SoundPlay();
            return SelectJob();
        }
        public int Start()
        {
            
            SceneInit();
            WriteText();
            _player.Name = Console.ReadLine();
            Prologue.PlayPrologue();
            return (int)SceneType.StartScene;
        }
        public int SelectJob()
        {
            SoundPlay();
            Console.Clear();
            DrawUI();
            WriteSelectJobText();

            int result = InputKey(MakeOption((int)PlayerJobs.EndPoint));
            switch (result)
            {
                case 0:
                    DungeonGame.Instance.message.Append("잘못된 입력 입니다");
                    return SelectJob();
                default:
                    PlayerJobs selectedJob = (PlayerJobs)(result-1);

                    string jobName = Enum.GetName(selectedJob);

                    Type type = Type.GetType($"TextBased_Dungeon_Game.{jobName}");

                    Player instance = (Player)Activator.CreateInstance(type);
                    // Activator로 객체를 생성하려면 default 생성자가 있어야함
                    DungeonGame.Instance.player = instance;
                    instance.PlayerInit();
                    return Start();

            }
           
        }
        public void WriteSelectJobText()
        {
            WriteRightMessage(MakeLogo());
            Console.ResetColor();
            WriteSelectMessage($"캐릭터 생성 중 ...\n1. 라우라\n2. 캐시");
            WriteMessage("스파르타 마을에 오신 여러분 환영합니다.\n원하시는 캐릭터를 선택해주세요.");

        }
        public void WriteText()
        {
            WriteRightMessage(MakeLogo());
            Console.ResetColor();
            WriteSelectMessage("캐릭터 생성 중 ...");
            WriteMessage("스파르타 마을에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.");

        }

        
    }
    class StartScene : Scene
    {
        public StartScene() : base() { }    
        public override int DrawScene()
        {
            return Start();
        }
        public int Start()
        {
            SceneInit();
            WriteText();
            return InputKey(MakeOption((int)SceneType.SaveRoadScene));
        }
        public void WriteText()
        {
            WriteRightMessage(MakeLogo());
            Console.ResetColor();
            WriteSelectMessage("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n6. 저장하기 & 기불러오기");
            WriteMessage("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n원하시는 행동을 입력해주세요.");

        }
    }
    class StatusScene : Scene
    {
        public StatusScene() : base() { }
        public override int DrawScene()
        {
            SceneInit();
            WriteText();
            return InputKey(MakeOption(0));
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
        PotionInventory _potionInventory;
        public override void SceneInit()
        {
            base.SceneInit();
            _inventory = _player.Inventory;
            _potionInventory = _player.potionInventory; 
        }
        public override int DrawScene()
        {
            SceneInit();
            WriteText();

            switch (InputKey(MakeOption(5)))
            {
                
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    return EquipmentInfo();
                case 2:
                    return PotionInventoryInfo();
                case 3:
                    _inventory.SortInventory(SortingInventory.Name);
                    return (int)SceneType.InventoryScene;
                    
                case 4:
                    _inventory.SortInventory(SortingInventory.Equipment);
                    return (int)SceneType.InventoryScene;
                    
                case 5:
                    _inventory.SortInventory(SortingInventory.Attack);
                    return (int)SceneType.InventoryScene;
                    
                case 6:
                    _inventory.SortInventory(SortingInventory.Defense);
                    return (int)SceneType.InventoryScene;
                default:
                    return (int)SceneType.StartScene;
            }
        }
        public int EquipmentInfo()
        {
            SceneInit();
            WriteEquipText();
            int index = InputKey(MakeOption(_inventory.Count()));

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
        public int PotionInventoryInfo()  // 포션 인벤토리
        {
            SceneInit();
            WritePotionText();

            // options를 포션 Count만큼 추가
            options = new int[_potionInventory.Count() + 1];
            options[0] = 0;
            for (int i = 1; i <= _potionInventory.Count(); i++)
            {
                options[i] = i;
            }

            int index = InputKey(options);
            switch (index)
            {
                case 0:
                    return (int)SceneType.InventoryScene;
                default:
                    _player.DrinkPotion(index - 1);
                    return PotionInventoryInfo();
            }
        }
        public void WriteText()
        {
            WriteRightMessage($"{_inventory.MakeItemList()}");
            WriteSelectMessage("1. 장착 관리\n2. 포션 인벤토리\n3. 이름\n4. 장착순\n5. 공격력\n6. 방어력");
            WriteMessage("[인벤토리]\n보유 중인 아이템을 관리할 수 있습니다.\n원하시는 행동을 입력해주세요.");
        }
        public void WriteEquipText()
        {
            WriteRightMessage($"{_inventory.MakeEquipList()}");
            WriteSelectMessage("0. 나가기");
            WriteMessage("[인벤토리 - 장착 관리]\n보유 중인 아이템을 관리할 수 있습니다.\n원하시는 행동을 입력해주세요.");
        }

        public void WritePotionText()
        {
            WriteRightMessage($"{_potionInventory.MakePotionList()}");
            WriteSelectMessage("1. 포션 사용\n0. 나가기");
            WriteMessage($"[포션 인벤토리]\n보유 중인 포션을 사용할 수 있습니다.\n원하시는 행동을 입력해주세요.");
        }  
    }
    class ShopScene : Scene
    {
        Shop _shop;
        public override void SceneInit()
        {
            base.SceneInit();
            _shop = DungeonGame.Instance.shop;
        }
        public override int DrawScene()
        {
            return StartShopScene();


        }
        public int StartShopScene()
        {
            SceneInit();
            WriteText();

            switch (InputKey(MakeOption(2)))
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:                 
                    return BuyItem();
                case 2:
                    return SellItem();
                default:
                    return (int)SceneType.ShopScene;
            }
        }
        public int BuyItem()
        {
            SceneInit();
            WriteBuyText();
            int index = InputKey(MakeOption(_shop.Count()));
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
            SceneInit();
            WriteSellText();
            int index = InputKey(MakeOption(_player.Inventory.Count()));
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
            WriteRightMessage($"{_shop.MakeItemList()}");
            WriteSelectMessage("1. 아이템 구매\n2. 아이템 판매\n0. 나가기");
            WriteMessage($"상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{_player.Gold}\n원하시는 행동을 입력해주세요.");
        }

        public void WriteBuyText()
        {
            WriteRightMessage($"{_shop.MakeShopList()}");
            WriteSelectMessage("0. 나가기");
            WriteMessage($"상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n[보유골드]\n{_player.Gold}\n원하시는 행동을 입력해주세요.");
        }

        public void WriteSellText()
        {
            WriteRightMessage($"{_player.Inventory.MakeSellList()}");
            WriteSelectMessage("0. 나가기");
            WriteMessage($"상점 - 아이템 판매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{_player.Gold}\n원하시는 행동을 입력해주세요.");
        }

    }
    class DungeonEnterScene : Scene
    {
        public new void SoundPlay()
        {
            Sound2.Play($"{path}\\Bgm.mp3", true, false);
            Sound2.Play($"{path}\\Dungeon.mp3", true, true);
        }
        public override int DrawScene()
        {
            SceneInit();
            WriteText();

            switch (InputKey(MakeOption(1)))
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    SoundPlay();
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
        public override int DrawScene()
        {
            SceneInit();
            WriteText();
            //DungeonGame.Instance.PlayerSave();
            if (InputKey(MakeOption(1)) == 1)
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
        public new void SoundPlay()
        {
            Sound2.Play($"{path}\\Bgm.mp3", true, false);
            Sound2.Play($"{path}\\Dungeon.mp3", true, true);
        }
        public override void SceneInit()
        {
            base.SceneInit();
            dungeon = DungeonGame.Instance.dungeon;
        }
        public override int DrawScene()
        {
            SceneInit();
            return PlayerPhase();
        }
        public int PlayerPhase()
        {
            SceneInit();
            WriteText();
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

            SceneInit();
            WriteBattleText();

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
            SceneInit();
            WriteSkillText();

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
        public int SelectSkillTarget(int index, int count)
        {
            SceneInit();
            WriteSkillTargetText();


            int key = InputKey(MakeOption(dungeon.Count()));
            switch (key)
            {
                case 0:
                    return PlayerSelectSkill();
                default:
                    Unit _unit = dungeon.GetUnit(key - 1);
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
            SoundPlay();
            SceneInit();
            WriteAttackResultText();
            Sound2.Play($"{path}\\Attack.mp3", false, true);

            switch (InputKey(MakeOption(0)))
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
    class SaveRoadScene : Scene
    {

        public override int DrawScene()
        {
            SceneInit();
            Console.WriteLine(MakeText());
            
            switch(InputKey(MakeOption(2))) 
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
            DungeonGame.Instance.dataManager.PlayerSave();
            //DungeonGame.dataManager.InventorySave();

            return DrawScene();
        }

        public int Load()
        {
            Console.Clear();
            Console.WriteLine(MakeLoadText());
            DungeonGame.Instance.dataManager.PlayerLoad();
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
    class MonsterPhaseScene : Scene
    {
        Dungeon dungeon;

        public new void SoundPlay()
        {
            Sound2.Play($"{path}\\Attack.mp3", false, true);
        }
        public override void SceneInit()
        {
            base.SceneInit();
            dungeon = DungeonGame.Instance.dungeon;
        }
        public override int DrawScene()
        {
            return MonsterPhase(0);
        }

        public int MonsterPhase(int i)
        {
            SoundPlay();
            SceneInit();
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
                switch (InputKey(MakeOption(0)))
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
        public new void SoundPlay()
        {
            Sound2.Play($"{path}\\Clear.mp3", false, true);
        }
        Dungeon dungeon;
        public override void SceneInit()
        {
            base.SceneInit();
            dungeon = DungeonGame.Instance.dungeon;
        }
        public override int DrawScene()
        {
            return BattleResult();
        }

        public int BattleResult()
        {
            SceneInit();
            if (dungeon.Result)
            {
                SoundPlay();
                WriteVictoryText();
                _player.GetExp(dungeon.Count() * 5);  // 몬스터 한 마리당 5의 경험치
                _player.Gold += 300;
                _player.potionInventory.GetPotion("체력 회복 포션", 1);
            }
            else
            {
                WriteLoseText();
            }

            dungeon.Clear();
            dungeon.Init();
            switch (InputKey(MakeOption(0)))
            {
                case 0:
                    base.SoundPlay();
                    return (int)SceneType.StartScene;
                default:
                    return (int)SceneType.StartScene;
            }

            

        }

        public void WriteVictoryText()
        {

            WriteLeftMessage($"[던전 결과]\n몬스터 {dungeon.Count()}마리를 잡았습니다! 경험치 {dungeon.Count() * 5} 증가!\n");
            WriteSelectMessage("0. 다음");
            WriteMessage("[보상 정산]\n골드 + 300 G\n체력 회복 포션 + 1개\n원하시는 행동을 입력해주세요");

        }

        public void WriteLoseText()
        {
            WriteLeftMessage($"{_player.PlayerInfo()}");
            WriteSelectMessage("0. 다음");
            WriteRightMessage($"Baltte!! - Result\nLose");
            WriteMessage("마을로 돌아갑니다.");
        }
    }
}
