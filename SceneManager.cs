namespace TextBased_Dungeon_Game
{
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
    
}
