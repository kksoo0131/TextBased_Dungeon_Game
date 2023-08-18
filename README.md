# TextBased_Dungeon_Game

1. DungeonGame 클래스

DungeonGame객체는 프로그램의 메인문에서 생성되어 Manager들을 이용해서 게임을 진행시킨다.

2. SceneManager 클래스

화면을 그리는 부분을 담당하는 클래스로 DungeonGame객체에서 선언되어 화면을 전환할때 호출된다.

3. Scene 클래스

필요한 화면들 별로 객체를 생성해서 SceneManager에서 관리한다.

4. Warrior 클래스

캐릭터의 정보를 관리하는 클래스, 인벤토리 객체를 들고 있다.

5. Inventory 클래스

캐릭터가 가지고 있는 아이템 리스트를 관리한다. 아이템을 추가, 제거 그리고 아이템 리스트를 string으로 출력한다.

6. Item 클래스

각각 아이템의 정보를 저장할 클래스, 아이템에 대한 정보를 string으로 출력한다.
