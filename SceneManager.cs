namespace TextBased_Dungeon_Game
{
    class SceneManager
    {
        List<Scene> _scenes = new List<Scene>();

        public SceneManager()
        {
            for (int i =0; i< (int)SceneType.EndPoint; i++)
            {
                SceneType selectedScene = (SceneType)i;

                string sceneName = Enum.GetName(selectedScene);

                Type type = Type.GetType($"TextBased_Dungeon_Game.{sceneName}");
                
                Scene instance = (Scene)Activator.CreateInstance(type);
                // Activator로 객체를 생성하려면 default 생성자가 있어야함
                _scenes.Add(instance);
                // Enum이름으로 Type을 구분해서 List에 추가
            }

        }

        public Scene SceneList(SceneType _type)
        {
            return _scenes[(int)_type];
        }

        
    }
    
}
