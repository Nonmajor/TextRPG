// 이해력이 낮아 까먹지 않기 위해 작성하면서 주석을 좀 많이 썼습니다

namespace TextRPG
{
    internal class Program
    {
        // 정적 클래스 변수 = 리스트
        //여러 함수/메서드에서 접근해야 하므로 static
        static Character player = new Character("Chad", "전사", 10, 5, 100, 10000); //새로운 Chracter 객체 생성 후 생성자 호출
        static ItemList shopItems = new ItemList(); //새로운 별도의(static) 빈 객체 ItemList를 생성하고 shopItems에 저장함. 컬렉션
        static ItemList inventory = new ItemList();
        static ItemList equippedItems = new ItemList();
        // shopItems, inventorym equippedItems 는 전부 static과 new를 사용하여 별도의 객체를 생성하였기 때문에 서로 데이터를 공유하지 않는다
        // = 각자 100의 크기를 가진 객체가 생성이 된다

        static void Main()
        {
            InitializeShop();
            ShowMainMenu();
        }

        static void InitializeShop() //함수
        {

            shopItems.Add(new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000));
            // shopItems는 ItemList 타입이기 때문에 Add 메서드를 호출 가능하다
            // new로 새로운 Item 타입 객체 생성 후 생성자를 호출하여 괄호 안 값들을 생성자에 전달함.
            // 만들어진 Item 타입 객체를 Add() 메서드를 통해 shopItems에 추가함

            shopItems.Add(new Item("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000));
            shopItems.Add(new Item("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
            shopItems.Add(new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600));
            shopItems.Add(new Item("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
            shopItems.Add(new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1000));
            shopItems.Add(new Item("그냥 검", 1000, 1000, "ㅈㄴ 쎄다~.", 10000));
            //여기까지 실행되었을 경우 shopItems 리스트에 6가지 변수가 추가됨
        }

        static void ShowMainMenu() //함수
        {
            while (true) //반복문
            {
                Console.Clear(); //콘솔창 텍스트 초기화
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ShowStatus(); //1번 입력 시 ShowStatus 호출
                        break;
                    case "2":
                        ShowInventory(); //2번 입력 시 ShowInventory 호출
                        break;
                    case "3":
                        ShowShop(); //3번 입력 시 ShowShop 호출
                        break;
                    case "0":
                        return; //0번 입력 시 while문을 빠져나가며 프로그램 종료
                    default: //그 외 입력 시
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        Console.ReadKey();//아무 키 입력 시 진행
                        break; //switch 문을 빠져가나서 다시 while문으로
                }
            }
        }

        static void ShowStatus() //캐릭터 상태창 함수
        {
            Console.Clear(); //콘솔창 텍스트 초기화
            int totalAtk = player.BaseAtk; //기본 공격력
            int totalDef = player.BaseDef; //기본 방어력

            for (int i = 0; i < equippedItems.Count; i++)
            //equippedItems의 수만큼 반복
            // public int Count => count; 코드를 통해 .Count로 equippedItems의 ItemList 컬렉션 내부의 count 변수에 접근이 가능하다
            // 접근을 하면 count 변수의 현재 값이 반환된다.

            {
                totalAtk += equippedItems[i].Atk; //totalAtk에 합산
                totalDef += equippedItems[i].Def; //totalDef에 합산
            }

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine("Lv. 01");
            Console.WriteLine($"{player.Name} ( {player.Job} )"); //문자열 보간 활용. player의 Name, Job 속성 출력
            Console.WriteLine($"공격력 : {totalAtk}");
            Console.WriteLine($"방어력 : {totalDef}");
            Console.WriteLine($"체  력 : {player.Hp}");//문자열 보간 활용. player의 Hp 속성 출력
            Console.WriteLine($"Gold : {player.Gold} G\n");//문자열 보간 활용. player의 Gold 속성 출력 + 한 줄 비우기
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine(); //입력값 받고 input에 저장
            if (input == "0") return; //입력값이 0이면 ShowStatus를 끝내고 호출한 곳으로 돌아감
        }

        static void ShowInventory() //인벤토리창 함수
        {
            while (true) //반복문
            {
                Console.Clear(); //콘솔창 텍스트 초기화
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                if (inventory.Count == 0) //inventory 변수의 수가 0일 경우
                {
                    Console.WriteLine("(보유 중인 아이템이 없습니다.)");
                }
                else //inventory 변수의 수가 0이 아닐 경우
                {
                    for (int i = 0; i < inventory.Count; i++) //inventory 변수의 수보다 작은만큼 반복
                    {
                        Item item = inventory[i];
                        // inventory[i]; : inventory 변수에 저장된 ItemList 객체에 정의된 인덱서 호출 -> public Item this[int index] => items[index]; 코드 작동 -> index 매개변수에 i 값 전달
                        // 전달받은 i 값으로 private Item[] items 배열에 접근하여 i 번째 위치에 저장되어있는 값을 Inventory[i] 로 반환함
                        // Item 타입의 변수 item에 inventory 컬렉션의 i 번째 요소를 할당함
                        // 지역 변수이므로 ShowInventory 함수 내에서만 유효함


                        string equippedMark = equippedItems.Contains(item) ? "[E]" : "";
                        // 3항연산자. item 객체를 Contains메서드에 넘겨서 equippedItems의 배열과 비교를 한다
                        // true 를 반환할 경우 ? 코드를 실행 -> equippedMark에 "[E]" 를 저장
                        // false를 반환할 경우 : 코드를 실행 -> equippedMark에 " " 공백 을 저장


                        Console.WriteLine($"- {i + 1} {equippedMark}{item.Name} | 공격력 +{item.Atk} | 방어력 +{item.Def} | {item.Desc}");
                        // - 아이템순서 equippedMark 불러온아이템의Name속성 | 공격력 +불러온아이템의Atk속성 | 방어력 +불러온아이템의Def속성 | 불러온아이템의Desc속성
                    }
                }

                Console.WriteLine("\n1. 장착관리"); //줄바꿈 후 출력
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();
                if (input == "1") //입력값이 1일 경우 ManageEquipment 호출
                {
                    ManageEquipment();
                }
                else if (input == "0") //입력값이 0일 경우 함수 종료 후 반환
                {
                    return;
                }
            }
        }

        static void ManageEquipment() //장착관리창 함수
        {
            while (true) //반복문
            {
                Console.Clear(); //콘솔창 텍스트 초기화
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++) //inventory 변수의 수보다 작은만큼 반복
                {
                    Item item = inventory[i];
                    // Item 타입의 변수 item에 inventory 컬렉션의 i 번째 요소를 가져옴
                    // 지역 변수이므로 ManageEquipment 함수 내에서만 유효함

                    string equippedMark = equippedItems.Contains(item) ? "[E]" : "";
                    //3항연산자. 불러온 아이템이 장착 중이라면(equippedItems의 컬렉션에 있다면) equippedMark에 [E] 저장, 아니라면 빈칸 저장

                    Console.WriteLine($"- {i + 1} {equippedMark}{item.Name} | 공격력 +{item.Atk} | 방어력 +{item.Def} | {item.Desc}");
                    // - 아이템순서 equippedMark 불러온아이템의Name속성 | 공격력 +불러온아이템의Atk속성 | 방어력 +불러온아이템의Def속성 | 불러온아이템의Desc속성 
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0") return; //0 입력 시 인벤토리창으로 돌아감


                int index; //정수형 변수 index

                //입력된 값(문자열)을 정수로 변환하여 index에 저장함. 성공 시 true, 실패 시 false 반환
                if (int.TryParse(input, out index))
                {
                    index--;
                    //배열은 0부터 시작하기 때문에 1을 고정으로 빼주어 0에도 접근 가능하게 해줌
                    // 1 입력 시 -1이 고정이라 0으로 입력됨 -> 배열 0에 접근 가능

                    if (index >= 0 && index < inventory.Count)
                    // index가 0보다 크거나같음 + inventory변수의 수보다 작은지 확인

                    {
                        Item selected = inventory[index];
                        // Item 타입의 변수 selected에 inventory 컬렉션의 index 번째 요소를 가져옴
                        // Add와 Remove 메서드로 배열에 저장된 값의 순서가 계속 바뀌는 equippedItems 변수와는 다르게 inventory 변수에 저장된 값은 순서가 바뀌는 일이 없기 때문에 상점에서 구입만 한다면 고정된 값을 계속 불러올 수 있음

                        if (equippedItems.Contains(selected)) // equippedItems 컬렉션에 선택된 아이템(selected = inventory의 i번째 요소)이 포함되어있는지 확인
                        {
                            equippedItems.Remove(selected);
                            // true일 경우 equippedItems 컬렉션에서 selected를 제거
                            // 정확하게는 Remove() 메서드를 호출하고 selected 변수의 값을 넘겨준다
                            // 해당 변수의 컬렉션에 값을 추가하는 Add() 메서드와 반대로 Remove()는 해당 변수의 컬렉션에서 값을 제거한다
                        }
                        else
                        {
                            equippedItems.Add(selected); // false일 경우 equippedItems 컬렉션에 추가함
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey(); // 대기
                    }
                }
            }
        }

        static void ShowShop() //상점 함수
        {
            while (true) //반복문
            {
                Console.Clear(); //콘솔창 텍스트 초기화
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G\n"); //player 변수의 Gold 속성 출력

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < shopItems.Count; i++) // shopItems 변수의 수보다 작은만큼 반복
                {
                    Item item = shopItems[i];
                    // Item 타입의 변수 item에 shopItems 컬렉션의 i 번째 요소를 가져옴
                    // 지역 변수이므로 ShowShop 함수 내에서만 유효함

                    string priceOrBought = item.IsPurchased ? "구매완료" : item.Price + " G";
                    // string형 변수 priceOrBought를 선언
                    // Item 타입 변수 item의 속성인 IsPurchased가 true일 경우 "구매완료" 를, false일 경우 Price속성과 "G" 를 문자열로 저장함

                    Console.WriteLine($"- {item.Name} | 공격력 +{item.Atk} | 방어력 +{item.Def} | {item.Desc} | {priceOrBought}");
                    // - 불러온아이템의Name속성 | 공격력 +불러온아이템의Atk속성 | 방어력 +불러온아이템의Def속성 | 불러온아이템의Desc속성
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine(); //입력값을 받아 input에 넘김
                if (input == "1") BuyItems(); // 1 입력 시 BuyItems() 함수 호출
                else if (input == "0") return;
            }
        }

        static void BuyItems() //함수
        {
            while (true) //반복문
            {
                Console.Clear();
                Console.WriteLine("아이템 구매");
                Console.WriteLine("구매할 아이템의 번호를 선택해주세요.\n");

                for (int i = 0; i < shopItems.Count; i++) // shopItems 변수의 수만큼 반복
                {
                    Item item = shopItems[i];
                    // Item 타입의 변수 item에 shopItems 컬렉션의 i 번째 요소를 가져옴
                    // 지역 변수이므로 BuyItems() 함수 내에서만 유효함

                    string priceOrBought = item.IsPurchased ? "구매완료" : item.Price + " G";
                    // string형 변수 priceOrBought를 선언
                    // Item 타입 변수 item의 속성인 IsPurchased가 true일 경우 "구매완료" 를, false일 경우 Price속성과 "G" 를 문자열로 저장함

                    Console.WriteLine($"- {i + 1} {item.Name} | 공격력 +{item.Atk} | 방어력 +{item.Def} | {item.Desc} | {priceOrBought}");
                    // - 아이템순서 불러온아이템의Name속성 | 공격력 +불러온아이템의Atk속성 | 방어력 +불러온아이템의Def속성 | 불러온아이템의Desc속성
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine(); // string형 변수 input에 입력값을 받음

                if (input == "0") return; // 입력값이 0일 경우 Shop으로 돌아감

                int index; // 정수형 지역변수 index
                if (int.TryParse(input, out index))
                //입력된 값(문자열)을 정수로 변환하여 index에 저장함. 성공 시 true, 실패 시 false 반환

                {
                    index--;
                    // ManageEquipment와 동일

                    if (index >= 0 && index < shopItems.Count)
                    // index가 0보다 크거나같음 + shopItems 변수의 수보다 작은지 확인
                    {
                        Item item = shopItems[index];
                        // Item 타입의 변수 item에 shopItems 컬렉션의 i 번째 요소를 가져옴
                        // 지역 변수이므로 BuyItems() 함수 내에서만 유효함

                        if (item.IsPurchased)
                        // item의 IsPurchased 속성이 true일 경우 (기본값은 false)

                        {
                            Console.WriteLine("이미 구매한 아이템입니다.");
                            Console.ReadKey(); // 대기
                        }
                        else if (player.Gold < item.Price)
                        // player의 Gold 속성이 item의 Price 속성보다 작을 경우

                        {
                            Console.WriteLine("Gold가 부족합니다.");
                            Console.ReadKey(); // 대기
                        }
                        else // if와 else if 둘 다 false일 경우
                        {
                            player.Gold -= item.Price; //player의 Gold 속성에서 item의 Price 값 만큼 차감 후 저장
                            item.IsPurchased = true; // 해당 item의 IsPurchased 속성을 true로 저장
                            inventory.Add(item); // inventory 변수로 Add() 함수를 호출 후 item을 넘겨줌 -> inventory 변수의 내부 배열에 해당 아이템 (item) 이 추가됨
                            Console.WriteLine("구매를 완료했습니다.");
                            Console.ReadKey(); // 대기
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                    }
                }
            }
        }
    }

    class Character //Character 클래스 정의 (생성자)
    {
        //속성
        public string Name; //문자열
        public string Job; //문자열
        public int BaseAtk, BaseDef, Hp, Gold; //정수

        //생성자
        public Character(string name, string job, int atk, int def, int hp, int gold) //접근 제한자, 클래스명, 매개변수
        // Chad / 전사 / 10 / 5 / 100 / 10000
        {
            //인수를 받아서 새로 만들어지는 객체의 속성을 초기화
            Name = name;
            Job = job;
            BaseAtk = atk;
            BaseDef = def;
            Hp = hp;
            Gold = gold;
        }
    }

    class Item //Item 클래스 정의
    {
        //속성
        public string Name;
        public int Atk, Def, Price;
        public string Desc;
        public bool IsPurchased = false;

        //생성자
        public Item(string name, int atk, int def, string desc, int price) //접근 제한자, 클래스명, 매개변수
        {
            //인수를 받아서 새로 만들어지는 객체의 속성을 초기화
            Name = name;
            Atk = atk;
            Def = def;
            Desc = desc;
            Price = price;
        }
    }

    class ItemList //ItemList 클래스 정의
    {
        private Item[] items = new Item[100];
        // private로 클래스 내부에서만 접근 가능.
        // Item[] 을 사용하여 Item 타입의 객체만 저장 가능하게 만듬.
        // Item 타입의 items 변수 선언 후 새로운 Item 객체를 만들어서 할당함
        // new 키워드를 사용하여 크기가 100인 Item 배열(객체) 생성

        private int count = 0;
        // 정수형 변수 count를 선언 후 0 할당
        // ItemList에 몇 개의 아이템이 저장되었는지를 나타냄

        public void Add(Item item) //메서드 매개변수
        // 컬렉션의 배열에 특정 객체를 추가하는 기능
        // 외부에서 호출 가능한 반환되지 않는 메서드 Add를 생성
        // (Item item) 은 Item 타입의 객체 item을 메서드를 호출할 때 객체 하나를 인자로 전달함(=내부 배열에 추가함)을 의미함
        // 여기서 item은 Add( ) 괄호 안에 들어가는 객체임 (예시 : shopItems.Add(new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600) 에서 괄호 안 부분이 item 이다) )
        // Add 호출 시 괄호 () 안에는 Item 타입 변수만 들어가야 함 (예시 :  equippedItems.Add(selected) 에서 selected를 index 변수로 바꾸면 컴파일 오류가 생김)

        {
            if (count < items.Length) // count 변수가 items 배열의 크기(100)보다 작다면
            {
                items[count++] = item;
                // items 배열의 count 위치에 접근하고 item 객체를 해당 위치에 저장한다. 그리고 count에 1을 추가함.
                // 첫 추가 시 count가 0이므로 배열의 0번째 인덱스에 item을 저장한다. 그리고 count의 값을 1 증가시켜 다음에는 1번째 인덱스에 저장 가능하게 한다.
            }
        }

        public int Count => count;
        // 외부에서 접근 가능한 정수형 변수 Count (대소문자 구분)를 선언한다
        // 람다식 => 를 통해 외부에서 접근 불가한 private int count의 값을 읽어올 수 있도록 한다
        // .Count를 통해 호출한 변수의 count 값을 반환함 = 현재 저장된 아이템의 개수를 알려주는 역할을 함

        public Item this[int index] => items[index];
        // ItemList 객체를 배열처럼 접근하기 위해 인덱서를 사용함
        // public으로 외부에서 접근 가능하게 하게 하고 Item 타입의 객체를 반환함
        // this[int index] : 인덱서 선언. this 는 현재 클래스의 인스턴스를 의미함. int index 는 정수형 매개변수 index를 [ ] 안에 받음.
        // items[index] : 람다식으로 표현한 읽기 전용 인덱서. items[index]는 private Item[] items = new Item[100]으로 크기 100의 배열을 할당받은 상태이다. 입력한 index에 해당하는 배열의 요소를 반환함
        // 위 기능으로 Item item = inventory[i]; 같은 코드로 ItemList 의 특정 위치에 있는 배열에 접근 가능함

        // 요약
        // 1. 인덱서 선언으로 private Item[] items 에 접근 가능하게 해줌 + 어디서나 [ ] 를 통해 인덱서 호출 가능
        // 2. 인덱서를 호출받았을 경우 index에 정수형으로 값이 저장됨 (예시로 inventory[i]일 경우 this=inventory int index = i)
        // 3. 람다식 표기법 => items[index] 로 인덱서 호출 시 items 의 index 번째 배열에 접근하여 값을 반환함


        public bool Contains(Item target)
        // bool : Contains 메서드가 true나 false를 반환하도록 함
        // Item target : Item item과 마찬가지로 Item 타입 객체 target을 인자로 전달받음


        {
            for (int i = 0; i < count; i++) // count 변수에 저장된 값보다 작은만큼 반복
            {
                if (items[i] == target) return true; // 전달받은 값 target이 items 배열의 i 번째 인덱스와 일치하면 true를 반환하고 종료함
            }
            return false; // 일치하지 않으면 false를 반환하고 종료함
        }

        public void Remove(Item target)
        // 컬렉션에서 특정 객체를 찾아 제거하는 기능 (덮어씌우고 공간을 하나 줄이는 방식)
        // Item 타입 변수 target을 넘겨받음


        {
            int index = -1; // 지역변수 index 를 선언하고 -1을 저장함. 아이템을 찾지 못한 상태

            for (int i = 0; i < count; i++) // count 변수에 저장된 값보다 작은만큼 반복
            {
                if (items[i] == target) // Remove() 메서드를 호출한 변수의 ItemList 컬렉션의 items 변수의 i 번째 배열에 저장된 값이 target 와 같을 경우
                {
                    index = i; // index 에 i 값을 저장 후 종료 (불필요한 추가 탐색 중지. 한번에 하나의 아이템만 제거)
                    break;
                }
            }
            if (index >= 0)
            // 인덱스가 0보다 크거나 같으면 = 아이템을 찾았으면

            {
                for (int i = index; i < count - 1; i++)
                // 새로운 지역변수 i 에 index의 값을 저장. i 가 count에서 -1을 뺀 값보다 작으면 반복 후 i 값 1 증가
                // index가 -1일 경우 실행되지 않음
                // 제거할 아이템부터 마지막 유효 items 배열의 끝까지 반복

                {
                    items[i] = items[i + 1]; //현재 선택한 위치에 다음 위치의 값을 덮어씌움 (기존 값은 삭제)
                }
                count--; // count에서 1을 감소시켜 실제 저장된 값을 감소시키고 빈 공간을 만듬
            }
        }

        // Add() 메서드와 Remove() 메서드 보충 설명

        // Add() 메서드로 추가 시 추가한 순서에 맞게 0, 1, 2, 3 .... 순으로 저장됨 (A B C D 를 추가한다고 가정하고 순서대로 A B C D 를 추가함)
        // 이제 Remove() 메서드로 1번째 배열과 3번째 배열인 1과 3을 제거함 -> Remove() 메서드는 뒤에있는 값을 현재 선택한 값에 덮어씌우고 빈 공간을 제거하는 식으로 작동함
        // 1번째와 3번째 배열의 값을 제거 시 A B C D 에서 A C 가 남음
        // 다시 Add로 추가할 경우 A C B D 또는 A C D B 가 됨 (추가하는 순서에 따라)
        // 이때 배열의 순서는 Remove()로 제거하면서 당겨졌으므로 A가 0, C가 1, B가 2, D가 3이 됨

        // 위 예제와 마찬가지로 상점에서 수련자, 무쇠, 스파르타, 낡은 검 순서대로 산 다음 (샀을 경우 inventory에 바로 추가) 산 순서대로 장착 시 equippedItems에 수련자, 무쇠, 스파르타, 낡은 검 순서대로 배열에 저장됨
        // 무쇠, 낡은 검을 장착 해제 시 inventory 배열에는 변화가 없지만 equippedItems 배열에서는 수련자, 스파르타만 0번째와 1번쨰 배열에 저장되고 나머지 배열에 저장된 값들은 제거되어 빈칸이 됨
        // 다시 Add() 매서드를 통해 equippedItems에 무쇠, 낡은 검을 추가 시 수련자, 스파르타, 무쇠, 낡은 검 순으로 0, 1, 2, 3 번째 배열에 저장됨



    }
}
