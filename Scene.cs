﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    public enum SceneType
    {
        StartScene,
        StatusScene,
        InventoryScene,
        ShopScene,
        EquipmentScene,
        BuyScene,
        EndPoint,
    }
    class Scene
    {   // 인터페이스에서는 모든 멤버가 암시적으로 public이고,
        // 클래스나 메서드에서는 모든 멤버가 암시적으로 internal이다.

        //Type 리플렉션을 사용할때 인터페이스로는 형변환이 불가능하기 때문에
        //인스턴스를 생성할수 있는 클래스로 변경하고 메서드를 가상메서드로 선언
        string WrongInputText = "잘못된 입력입니다";
        public Scene(SceneType _type) { Type = _type; }
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
        public StartScene(SceneType _type) : base(_type) { }
        int[] options = { 1, 2, 3};

        string GameStartText = "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다. \n\n1. 상태 보기\n2. 인벤토리\n3. 상점\n\n원하시는 행동을 입력해주세요.";
        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(GameStartText);
            return InputKey(options);
        }

    }
    class StatusScene : Scene
    {
        public StatusScene(SceneType _type) : base(_type) { }
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
        public InventoryScene(SceneType _type) : base(_type) { }

        int[] options = { 0, 1, 2, 3, 4, 5};

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());

            switch (InputKey(options))
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    return (int)SceneType.EquipmentScene;
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

        public string MakeText()
        {
            return $"인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n\n{DungeonGame.player.Inventory.MakeItemList()}\n\n1. 장착 관리\n2. 이름\n3. 장착순\n4. 공격력\n5. 방어력\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }
    }
    class EquipmentScene : Scene
    {
        public EquipmentScene(SceneType _type) : base(_type) { }

        int[] options = { 0 };

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());

            // options를 아이템 Count갯수만큼 추가해야됨
            options = new int[DungeonGame.player.Inventory.Count() + 1];
            options[0] = 0;
            for (int i =1; i<= DungeonGame.player.Inventory.Count(); i++)
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
                    return (int)SceneType.EquipmentScene;   
            }
        }

        public string MakeText()
        {
            return $"인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n\n{DungeonGame.player.Inventory.MakeEquipList()}\n\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }
    }
    class ShopScene : Scene
    {
        public ShopScene(SceneType _type) : base(_type) { }
        int[] options = { 0, 1};

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());

            switch (InputKey(options))
            {
                case 0:
                    return (int)SceneType.StartScene;
                case 1:
                    return (int)SceneType.BuyScene;
                default:
                    return (int)SceneType.ShopScene;
            }
        }

        public string MakeText()
        {
            return $"상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{DungeonGame.player.Gold} G\n\n{DungeonGame.shop.MakeItemList()}\n\n1. 아이템 구매\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }

    }
    class BuyScene : Scene
    {
        public BuyScene(SceneType _type) : base(_type) { }

        int[] options = { 0 };

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeText());

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
                    return (int)SceneType.InventoryScene;
                default:
                    Item? item = DungeonGame.shop.BuyItem(index - 1);
                    if (item != null)
                    {
                        DungeonGame.player.Inventory.AddItem(item);
                    }
                    
                    return (int)SceneType.BuyScene;
            }
        }

        public string MakeText()
        {
            return $"상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]\n{DungeonGame.player.Gold} G\n\n{DungeonGame.shop.MakeShopList()}\n\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
        }
    }
}
