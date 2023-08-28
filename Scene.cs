using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    public enum SceneType
    {
        
        StartScene,

        // 스타트 씬 메뉴 4개
        StatusScene,
        InventoryScene,
        ShopScene,
        DungeonEnterScene,
        RestScene,
        //나머지
        DungeonClearScene,
        DungeonFailScene,
        BattleScene,

        //마지막은 EndPoint여야함
        EndPoint,
    }
    class Scene
    {   // 인터페이스에서는 모든 멤버가 암시적으로 public이고,
        // 클래스나 메서드에서는 모든 멤버가 암시적으로 internal이다.

        //Type 리플렉션을 사용할때 인터페이스로는 형변환이 불가능하기 때문에
        //인스턴스를 생성할수 있는 클래스로 변경하고 메서드를 가상메서드로 선언
        string WrongInputText = "잘못된 입력입니다";
        SceneType Type { get; set; }
        public virtual int DrawScene() { return 0; }
        public virtual int InputKey(int[] options)
        {
            int input;

            while (!int.TryParse(Console.ReadLine(), out input) || !options.Contains(input))
            {
                Console.WriteLine(WrongInputText);
            }

            return input;
        }
        static void WriteLine(string str)
        {
            Console.SetCursorPosition(50, 20);
        }

    }
    class StartScene : Scene
    {
        int[] options = { 1, 2, 3, 4, 5 };
        
        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());
            return InputKey(options);
        }

        public string MakeText()
        {
            return "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다. \n\n1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n\n원하시는 행동을 입력해주세요.";
        }

    }
    class StatusScene : Scene
    {
        int[] options = { 0 };

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());
            return InputKey(options);
        }

        public string MakeText()
        {
           
            return $"상태 보기\n캐릭터의 정보가 표시됩니다.\n\n{DungeonGame.player.PlayerInfo()}0. 나가기\n\n원하시는 행동을 입력해주세요";
            // 버그 해결
            // 괄호 안에 DungeonGame.player.PlayerInfo를 넣으면 델리게이트에 메서드 이름을 추가할때의 형식으로 메서드 이름이 들어가게됨
            // DungeonGame.player.PlayerInfo() 는 메서드의 리턴값이 들어가게된다.
        }
    }
    class InventoryScene : Scene
    {

        int[] options;

        public override int DrawScene()
        {
            options = new int[] { 0, 1, 2, 3, 4, 5};
            Console.Clear();
            Console.WriteLine(MakeText());

            switch (InputKey(options))
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    return EquipmentInfo();
                case 2:
                    DungeonGame.player.Inventory.SortInventory(SortingInventory.Name);
                    return (int)SceneType.InventoryScene;
                case 3:
                    DungeonGame.player.Inventory.SortInventory(SortingInventory.Equipment);
                    return (int)SceneType.InventoryScene;
                case 4:
                    DungeonGame.player.Inventory.SortInventory(SortingInventory.Attack);
                    return (int)SceneType.InventoryScene;
                case 5:
                    DungeonGame.player.Inventory.SortInventory(SortingInventory.Defense);
                    return (int)SceneType.InventoryScene;
                default:
                    return (int)SceneType.StartScene;
            }
        }
        public int EquipmentInfo()
        {
            Console.Clear();
            Console.WriteLine(MakeEquipText());

            // options를 아이템 Count갯수만큼 추가해야됨
            options = new int[DungeonGame.player.Inventory.Count() + 1];
            options[0] = 0;
            for (int i = 1; i <= DungeonGame.player.Inventory.Count(); i++)
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
                    DungeonGame.player.EquipItem(index - 1);
                    DungeonGame.PlayerSave();
                    return EquipmentInfo();
            }
        }

        public string MakeText()
        {
            return $"인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n\n{DungeonGame.player.Inventory.MakeItemList()}\n\n1. 장착 관리\n2. 이름\n3. 장착순\n4. 공격력\n5. 방어력\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }

        public string MakeEquipText()
        {
            return $"인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n\n{DungeonGame.player.Inventory.MakeEquipList()}\n\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }
    }
    class ShopScene : Scene
    {
        int[] options;

        public override int DrawScene()
        {
            options = new int[] { 0, 1, 2};
            Console.Clear();
            Console.WriteLine(MakeText());

            switch (InputKey(options))
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
            Console.Clear();
            Console.WriteLine(MakeBuyText());
            DungeonGame.PrintMessage();
            // options를 아이템 Count갯수만큼 추가해야됨
            options = new int[DungeonGame.shop.Count() + 1];
            options[0] = 0;
            for (int i = 1; i <= DungeonGame.shop.Count(); i++)
            {
                options[i] = i;
            }

            int index = InputKey(options);
            switch (index)
            {
                case 0:
                    return (int)SceneType.ShopScene;
                default:
                    Item? item = DungeonGame.shop.BuyItem(index - 1);
                    if (item != null)
                    {
                        DungeonGame.PlayerSave();
                        DungeonGame.ShopSave();
                        DungeonGame.player.Inventory.AddItem(item);
                    }

                    return BuyItem();
            }
        }
        public int SellItem()
        {
            Console.Clear();
            Console.WriteLine(MakeSellText());
            DungeonGame.PrintMessage();

            // options를 아이템 Count갯수만큼 추가해야됨
            options = new int[DungeonGame.player.Inventory.Count() + 1];
            options[0] = 0;
            for (int i = 1; i <= DungeonGame.player.Inventory.Count(); i++)
            {
                options[i] = i;
            }

            int index = InputKey(options);
            switch (index)
            {
                case 0:
                    return (int)SceneType.ShopScene;
                default:
                    DungeonGame.player.SellItem(index - 1);
                    DungeonGame.PlayerSave();
                    return SellItem();
            }
        }
        public string MakeText()
        {
            return $"상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{DungeonGame.player.Gold} G\n\n{DungeonGame.shop.MakeItemList()}\n\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }
        public string MakeBuyText()
        {
            return $"상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{DungeonGame.player.Gold} G\n\n{DungeonGame.shop.MakeShopList()}\n\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }
        public string MakeSellText()
        {
            return $"상점 - 아이템 판매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{DungeonGame.player.Gold} G\n\n{DungeonGame.player.Inventory.MakeSellList()}\n\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }
    }
    class DungeonEnterScene : Scene
    {
        int[] options = { 0, 1, 2, 3, 4};

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());

            switch (InputKey(options))
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    return DungeonGame.Instance.EnterDungeon(1);
                case 2:
                    return DungeonGame.Instance.EnterDungeon(2);
                case 3:
                    return DungeonGame.Instance.EnterDungeon(3);
                case 4:
                    return (int)SceneType.BattleScene;
                default:
                    return (int)SceneType.StartScene;
            }
        }

        public string MakeText()
        {
            return "던전입장\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n1. 쉬운 던전      | 방어력 5 이상 권장\n2. 일반 던전      | 방어력 11 이상 권장\n3. 어려운 던전     | 방어력 17 이상 권장\n0. 나가기\n\n원하시는 행동을 입력해주세요";

        }
    }
    class DungeonClearScene : Scene
    {
        int[] options = { 0 };

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());
            DungeonGame.PlayerSave();
            return InputKey(options);
        }

        public string MakeText()
        {
            return $"던전 클리어\n축하합니다!!\n쉬운 던전을 클리어 하였습니다.\n\n[탐험 결과]\n체력 {DungeonGame.player.PrevHealth} -> {DungeonGame.player.Health}\nGold {DungeonGame.player.PrevGold} G -> {DungeonGame.player.Gold} G\n\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }

    }
    class DungeonFailScene : Scene
    {
        int[] options = { 0 };

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());
            DungeonGame.PlayerSave();
            return InputKey(options);
        }

        public string MakeText()
        {
            return $"던전 실패\n방어력이 부족해 던전에서 실패했습니다.\n방어력을 높혀 다시 도전해 주세요.\n\n[탐험 결과]\n체력 {DungeonGame.player.PrevHealth} -> {DungeonGame.player.Health}\nGold {DungeonGame.player.PrevGold} G -> {DungeonGame.player.Gold} G\n\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }

    }
    class RestScene : Scene
    {
        int[] options = { 0, 1 };

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());
            DungeonGame.PrintMessage();
            DungeonGame.PlayerSave();
            if (InputKey(options) == 1)
            {
                DungeonGame.player.Rest();
                return (int)SceneType.RestScene;
            }
            return (int)SceneType.StartScene;
        }

        public string MakeText()
        {
            return $"휴식하기\n500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {DungeonGame.player.Gold} G\n\n1. 휴식하기\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }
    }
    class BattleScene : Scene 
    {
        int[] options;

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());
            DungeonGame.PrintMessage();

            options = new int[] { 1 };
            switch (InputKey(options))
            {
                case 1:
                    return StartBattle();
                default:
                    return (int)SceneType.BattleScene;
            }

        }

        public int StartBattle()
        {
            //몬스터의 수 만큼 options를 추가
            options = new int[DungeonGame.shop.Count() + 1];
            options[0] = 0;
            for (int i = 1; i <= DungeonGame.shop.Count(); i++)
            {
                options[i] = i;
            }
            Console.Clear();
            Console.WriteLine(MakeText());
            DungeonGame.PrintMessage();

            int key = InputKey(options);
            switch (key)
            {
                case 0:
                    return (int)SceneType.BattleScene;
                default:
                    DungeonGame.player.AttackMonstser(DungeonGame.dungeon.monsterList[key - 1]);
                    return AttackResult();
                    // 몬스터 선택에 따른 결과 출력

            }
        }

        public int AttackResult()
        { 
            // 플레이어가 몬스터 번호를 입력하면
            // 몬스터 리스트의 해당 인덱스에 있는 몬스터가
            // 플레이어가 주는 데미지를 받음
            options = new int[] { 0 };
            Console.Clear();
            Console.WriteLine($"Battle!!\n\n");
            DungeonGame.PrintMessage();
            Console.WriteLine("\n\n0. 다음");

            switch (InputKey(options))
            {
                default:
                    return EnemyPhase();
            }
        }

        public int EnemyPhase()
        {
            //앞 인덱스의 몬스터부터 공격함 마지막 인덱스의 몬스터가 공격하면 플레이어의 차례가됨
        }

        public string MakeText()
        {
            return $"Battle!!\n\n{DungeonGame.dungeon.MonsterListInfo()}\n\n[내정보]\n\n{DungeonGame.player.PlayerInfo()}\n\n1. 공격\n\n원하시는 행동을 입력해주세요.";
        }

        public string MakeBattleText()
        {
            return $"Battle!!\n\n{DungeonGame.dungeon.MonsterSelectInfo()}\n\n[내정보]\n\n{DungeonGame.player.PlayerInfo()}\n\n0. 취소\n\n원하시는 행동을 입력해주세요.";
        }
    }
}
