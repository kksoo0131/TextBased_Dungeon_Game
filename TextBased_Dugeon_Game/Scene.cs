namespace TextBased_Dugeon_Game
{
    class Warrior
    {
        public int Level { get; set; }
        public string Chad { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
    }
    public enum SceneType
    {
        StartScene,
        StatusScene,
        InventoryScene,
        EndPoint,
    }
    class SceneManager
    {
        List<Scene> _scenes = new List<Scene>();

        public SceneManager()
        {
            for (int i =0; i< (int)SceneType.EndPoint; i++)
            {
                // Enum이름으로 Type을 구분해서 List에 추가
                // Reflection 사용법 익히기
            }
            _scenes.Add(new StartScene(SceneType.StartScene));
            _scenes.Add(new StatusScene(SceneType.StatusScene));
            _scenes.Add(new InventoryScene(SceneType.InventoryScene));

        }

        public Scene SceneList(SceneType _type)
        {
            return _scenes[(int)_type];
        }

        
    }
    class Scene
    {   // 인터페이스에서는 모든 멤버가 암시적으로 public이고,
        // 클래스나 메서드에서는 모든 멤버가 암시적으로 internal이다.

        //Type 리플렉션을 사용할때 인터페이스로는 형변환이 불가능하기 때문에
        //인스턴스를 생성할수 있는 클래스로 변경하고 메서드를 가상메서드로 선언
        string WrongInputText = "잘못된 입력입니다";
        public Scene(SceneType _type) { Type = _type; }
        SceneType Type { get;  set; }
        public virtual int DrawScene() { return -1; }
        public virtual int InputKey(int[] options) 
        {
            int input;

            while (!int.TryParse(Console.ReadLine(), out input) || !options.Contains(input))
            {
                Console.WriteLine(WrongInputText);
            }

            return input;
        }

    }
    class StartScene : Scene
    {
        public StartScene(SceneType _type) : base(_type) {}
        int[] options = { 1, 2 };

        string GameStartText = "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다. \n\n1. 상태 보기\n2. 인벤토리\n\n원하시는 행동을 입력해주세요.";
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
        string WrongInputText = "잘못된 입력입니다";
        int[] options = {0};

        public override int DrawScene()
        {
            Warrior player = new Warrior() { Level = 1, Chad = "전사", Attack = 10, Defense = 5, Health = 100, Gold = 1500 };
            Console.Clear();
            Console.WriteLine(MakeStatusText(player));
            return InputKey(options);
        }

        public string MakeStatusText(Warrior player)
        {
            string status = $"Lv. { player.Level}\n\nChad({ player.Chad})\n\n공격력: { player.Attack}\n\n방어력: { player.Defense}\n\n체력: { player.Health}\n\nGold: { player.Gold} G\n\n";
            return $"상태 보기\n캐릭터의 정보가 표시됩니다.\n\n{status}0. 나가기\n\n원하시는 행동을 입력해주세요";
        }
    }
    class InventoryScene : Scene
    {
        public InventoryScene(SceneType _type) : base(_type) { }

        int[] options = { 0, 1 };

        public override int DrawScene()
        {
            Console.Clear();
            Console.WriteLine(MakeInventoryText());
            return InputKey(options);
        }

        public string MakeInventoryText()
        {
            string itemlist = " ";// 아이템 리스트를 뽑아옴
            return $"인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]\n\n{itemlist}\n\n0. 나가기\n\n원하시는 행동을 입력해주세요.";
                
        }
    }
}
