# TextBased_Dungeon_Game

### 과제 개요

1. 던전을 떠나기전 마을에서 장비를 구하는 게임을 텍스트로 구현합니다. (C# - Console App)
2. 상점의 아이템 중에서 나만의 장비를 구성하는 부분이 포인트입니다.
3. 장비는 여러개의 데이터가 함께 있는 만큼 객체나 구조체를 활용하는 편이 효율적 입니다.
(이름, 가격, 효과, 설명 등…)
4. 관련된 여러 데이터를 다루는 부분은 배열이 도움이 됩니다.

<br/>
<br/>
<br/>

### 요구 사항 구현

<br/>

## 1. 게임 시작 화면

각 화면마다 Scene 클래스를 상속받아서 새로운 클래스를 정의했고 

Enum ScenType과 SceneManager 클래스를 이용해서 화면의 전환을 구현했습니다.

SceneManager은 List로 각 Scene을 관리하고 SceneType()을 통해서 해당 scene에 접근할 수 있습니다.

[DungeonGame](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/DungeonGame.cs)

[SceneManager](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/SceneManager.cs)

[Scene](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/Scene.cs)


<br/>

처음 게임이 시작될때 SceneManager의 SceneList에 Scene들이 추가되고각 화면에서 입력받는 입력 값에 따라서 

SceneManager에서 다른 Scene의 DrawScene()메서드를 호출해서 화면을 그려줍니다.

DrawScene()메서드는 Scene 클래스의 메소드로 각 Scene에서 오버라이드 해서 사용합니다.

<br/>
<br/>

## 2. 상태 보기 + 선택 8

캐릭터의 객체를 Warrior 클래스로 생성하고 Warrior 클래스의 PlayerInfo() 메서드로

캐릭터의 정보를 StringBuilder을 이용해 문자열로 만들어서 return 합니다

캐릭터의 상태를 보는 StatusScene에서는 캐릭터 정보 문자열을 받아서 화면에 출력할 String을 만들고 출력합니다.


### 선택8. 장착 개선

Warrior 클래스에 Itemtype에 따라서 객체를 하나씩 생성했고

EquipItem()메서드를 통해 아이템을 장착할때 이미 장착하고 있는 아이템이 있다면 해당 아이템을 해제하고 새로운 아이템을 장착하게 구현했고

장착하는 아이템의 능력치를 받을 프로퍼티를 만들어서 따로 저장했습니다.

[Warrior](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/Warrior.cs)
<br/>
<br/>

## 3. 인벤토리 + 선택2 + 선택5 

### 선택 2. 아이템 정보를 배열로 관리
아이템을 List로 관리하는 클래스 입니다.

### 선택 5. 인벤토리 크기 맞춤
MakeItemInfo() 메서드에서 StringBuilder를 사용해 각 칸의 길이를 일정하게 맞추어 String을 만들어 return 합니다.

<br/>
그리고 MakeItemList(), MakeEquipList(), MakeSellList() 등의 메서드로 해당되는 아이템 목록을 StringBuilder를 사용해서 string 형태로 return 할 때 MakeItemInfo()메서드가 활용되고,

InventoryScene, ShopScene등에서 해당 메서드들을 이용해 출력할 String을 만들고 출력합니다.

[Inventory](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/Inventory.cs)
<br/>
<br/>

## 아이템 클래스 선택 1 + 선택6

### 선택1. 아이템 정보를 클래스로 구현

아이템의 정보를 가진 Item 클래스를 만들었고

아이템의 타입에따라서 Item클래스를 상속한 Weapon클래스와 Armor클래스를 만들었습니다.

### 선택 6. 인벤토리 정렬하기

인벤토리에서는 List로 아이템을 관리하고 있기때문에 List.Sort()메서드를 이용해서 아이템을 정렬할 수 있는 기능이 있었고,

원하는 이름, 장착여부, 공격력, 방어력 순으로 정렬하기 위해서 Item 클래스가 IComparable<Item> 인터페이스를 상속받았고

입력받은 열거형 SortingInventory에 따라서 비교를 수행하는 CompareBySortingInventory()메서드를 만들었습니다

[Item](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/Item.cs)

<br/>
<br/>

## 선택 7. 상점 -아이템 구매, 판매

Scene을 상속받은 ShopScene클래스, Inventory를 상속받는 Shop 클래스를 만들어 주었습니다.

MakeItemInfo(), MakeItemList(), MakeShopList()등의 메서드들을 Shop의 기능에 맞게 오버라이딩 했고,

BuyItem() 메서드로 아이템 구매를 구현했습니다. 

<br/>

아이템 판매는 Warrior클래스에서 SellItem()메서드로 구현했습니다.

[Shop](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/Inventory.cs)

<br/>
<br/>

## 선택9. 던전입장, 휴식, 레벨업

던전 입장과 레벨업은 DungeonGame 클래스의 EnterDungeon()으로 구현했고

Scene클래스를 상속받은 DungeonEnterScene, DungeonClearScene, DungeonFailScene, RestScene을 만들어 주었습니다.

[DungeonGame](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/DungeonGame.cs)
<br/>
<br/>

## 선택 10. 게임저장

DungeonGame 클래스에 PlayerSave(), ShopSave(), Read() 메서드를 만들었고,

player, shop 클래스들을 모두 [Serializable] 를 붙힌 후 

직렬화 하여 파일로 저장하고 불러오는 기능을 구현했습니다.
[DungeonGame](https://github.com/kksoo0131/TextBased_Dungeon_Game/blob/main/DungeonGame.cs)
