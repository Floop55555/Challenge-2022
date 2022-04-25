using System;
using System.IO;
using System.Diagnostics;

namespace Console_Crossing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            int[] date = new int[6];
            string[] pocket = new string[32];
            string[] characterData = new string[7];
            int bells = 0;
            //0: Placeholder
            //1: Player Name
            //2: he/she/they
            //3: him/her/them
            //4: his/her/their
            //5: Town Name
            ObtainCharacterData(characterData);
            // System.Console.WriteLine(characterData[0]);
            // System.Console.WriteLine(characterData[1]);
            // System.Console.WriteLine(characterData[2]);
            // System.Console.WriteLine(characterData[3]);
            // System.Console.WriteLine(characterData[4]);
            // System.Console.WriteLine(characterData[5]);
            // System.Console.WriteLine(characterData[6]);
            if (characterData[0] != "-1")
            {
                CreateNewCharacter();
                ObtainCharacterData(characterData);
            }
            GetDate(date);
            GetPocket(pocket, ref bells);
            GreetPlayer(date, characterData);
            GameRoute(date, characterData, pocket, ref bells);
            SavePocket(pocket, ref bells);
        }
        //Start Game
        static void ObtainCharacterData(string[] characterData)
        {
            int count = 0;
            StreamReader inFile = new StreamReader("player.txt");
            string line = inFile.ReadLine();
            while (line != null)
            {
                characterData[count] = line;
                count++;
                line = inFile.ReadLine();
            }
            inFile.Close();
        }
        static void CreateNewCharacter()
        {
            Console.WriteLine("What is your name?");
            string characterName = Console.ReadLine();
            System.Console.WriteLine("What is your gender?");
            System.Console.WriteLine("Male, Female, or Non-Binary?");
            string characterGender = Console.ReadLine().ToLower();
            while (characterGender.ToLower() != "male" && characterGender.ToLower() != "female" && characterGender.ToLower() != "Non-Binary")
            {
                System.Console.WriteLine("Please select an option");
                characterGender = Console.ReadLine();
            }
            string characterPronouns1;
            string characterPronouns2;
            string characterPronouns3;
            if (characterGender == "male")
            {
                characterPronouns1 = "he";
                characterPronouns2 = "him";
                characterPronouns3 = "his";
            }
            else if (characterGender == "female")
            {
                characterPronouns1 = "she";
                characterPronouns2 = "her";
                characterPronouns3 = "hers";
            }
            else
            {
                characterPronouns1 = "they";
                characterPronouns2 = "them";
                characterPronouns3 = "theirs";
            }
            System.Console.WriteLine($"So, {characterName} what is the name of the cozy little village you will be calling home?");
            string townName = Console.ReadLine();
            Console.Clear();
            StreamWriter character = new StreamWriter("player.txt");

            character.WriteLine("-1");
            character.WriteLine(characterName);
            character.WriteLine(characterPronouns1);
            character.WriteLine(characterPronouns2);
            character.WriteLine(characterPronouns3);
            character.WriteLine(townName);
            character.Close();
        }

        static void GreetPlayer(int[] date, string[] characterData)
        {
            System.Console.WriteLine($"Welcome, {characterData[1]}");
            System.Console.WriteLine($"Today is {DayOfWeek(date)} the {date[2]} of {MonthOfYear(date)}, {date[0]}");
            CurrentTime(date);
            System.Console.WriteLine($"Enjoy your day in {characterData[5]}.");
            System.Console.WriteLine("Press any key to get started!");
            Console.ReadKey();
            Console.Clear();
        }
        static void GetPocket(string[] pocket, ref int bells)
        {
            StreamReader inFile = new StreamReader("pocket.txt");
            string line = inFile.ReadLine();
            for (int i = 0; i < 32; i++)
            {
                pocket[i] = line;
                line = inFile.ReadLine();
            }
            bells = int.Parse(line);
            inFile.Close();
        }
        //Save Game
        static void SavePocket(string[] pocket, ref int bells)
        {
            StreamWriter inFile = new StreamWriter("pocket.txt");
            string line = pocket[0];
            for (int i = 0; i < 32; i++)
            {
                inFile.WriteLine(pocket[i]);
            }
            inFile.Write(bells);
            inFile.Close();
        }
        //Game Route
        static void GameRoute(int[] date, string[] character, string[] pocket, ref int bells)
        {
            string userInput = "";
            while (userInput != "5")
            {
                GetDate(date);
                CurrentTime(date);
                RouteMenu();
                userInput = Console.ReadLine();
                Console.Clear();
                switch (userInput)
                {
                    case "1":
                        //if (date[4] < 22 && date[4] > 8)
                        //{
                        Market(date, character, pocket, ref bells);
                        //}
                        //else
                        // {
                        //     System.Console.WriteLine("Hmmm, what is this?");
                        //     System.Console.WriteLine("Dear Valued Customers: We are closed for the day. Come visit us when we");
                        //     System.Console.WriteLine("re-open tommorrow at 8 a.m. Thank You!");
                        //     System.Console.WriteLine();
                        //     System.Console.WriteLine("That's right, they are only open from 8 a.m. to 10 p.m...");
                        //     System.Console.WriteLine("(Press any button to return to town)");
                        //     Console.ReadKey();
                        // }
                        break;
                    case "2":
                        FishingRoute(date, character, pocket);
                        break;
                    case "3":
                        FruitTrees(date, character, pocket);
                        break;
                    case "4":
                        CheckPockets(pocket, bells);
                        break;
                    case "5":
                        break;
                    default:
                        System.Console.WriteLine("Oh dear, I'm not quite sure what I'm trying to do...");
                        break;
                }
            }
        }
        static void RouteMenu()
        {
            System.Console.WriteLine("1. I should visit the market");
            System.Console.WriteLine("2. Let's see what's swimming");
            System.Console.WriteLine("3. The orchard could use some attention");
            System.Console.WriteLine("4. Let's see what's in my pockets");
            System.Console.WriteLine("5. I think I'm done for the day");
            System.Console.WriteLine();
        }
        //Nooklings
        static void Market(int[] date, string[] character, string[] pocket, ref int bells)
        {
            DateTime localDate = DateTime.Now;
            string userInput = "";
            if (date[3] == 0)
            {
                while (userInput != "2")
                {
                    System.Console.WriteLine("Oh? A mystertious tent! Should I visit?");
                    System.Console.WriteLine("1. Yes");
                    System.Console.WriteLine("2. No");
                    userInput = Console.ReadLine();
                    Console.Clear();
                    switch (userInput)
                    {
                        case "1":
                            System.Console.WriteLine("Okay... here goes nothing!");
                            ReddShop(character, pocket, bells);
                            break;
                        case "2":
                            System.Console.WriteLine("Okay, I'll just go to the market then!");
                            break;
                        default:
                            System.Console.WriteLine("What was that?");
                            break;
                    }
                }
            }
            string[] items = new string[100];
            int[] nookSell = new int[100];
            int[] nookBuy = new int[100];
            LoadItems(items, nookBuy, nookSell);
            Console.WriteLine(nookSell[0]);
            Console.WriteLine(nookBuy[0]);
            System.Console.WriteLine($"Welcome, {character[1]}!");
            userInput = "";
            while (userInput != "3")
            {
                MarketMenu();
                userInput = Console.ReadLine();
                Console.Clear();
                switch (userInput)
                {
                    case "1":
                        BuyItem(pocket, items, nookSell, ref bells);
                        break;
                    case "2":
                        SellItem(pocket, items, nookBuy, ref bells);
                        break;
                    case "3":
                        System.Console.WriteLine("Come back soon!");
                        break;
                    default:
                        System.Console.WriteLine("Sorry, I'm not sure I understand...");
                        break;
                }
            }
        }
        static void LoadItems(string[] items, int[] nookBuy, int[] nookSell)
        {
            StreamReader inFile = new StreamReader("items.txt");
            string line = inFile.ReadLine();
            int count = 0;
            while (line != null)
            {
                string[] temp = line.Split("#");
                items[count] = temp[0];
                nookBuy[count] = int.Parse(temp[1]);
                nookSell[count] = int.Parse(temp[2]);
                line = inFile.ReadLine();
                count++;
            }
            inFile.Close();
        }
        static void MarketMenu()
        {
            System.Console.WriteLine("What can we do for you?");
            System.Console.WriteLine("1. What are you selling?");
            System.Console.WriteLine("2. Would you buy these?");
            System.Console.WriteLine("3. I'm good, thank you!");
        }
        static void SellItem(string[] pocket, string[] items, int[] nookBuy, ref int bells)
        {

            int verify = 0;
            while (verify != -1)
            {
                System.Console.WriteLine("What are you looking to sell?");
                WritePockets(pocket);
                System.Console.WriteLine("... or would you like to STOP?");
                string item = Console.ReadLine().ToUpper();
                int pocketPosition = SearchPocket(pocket, item);
                if (pocketPosition != -1)
                {
                    pocket[pocketPosition] = null;
                    int sellValue = GiveBells(items, nookBuy, ref bells, item);
                    System.Console.WriteLine($"Thank you. Here's {sellValue} bells!");
                    verify = KeepShopMenuing("sell");
                }
                else if (item == "STOP")
                {
                    System.Console.WriteLine("No problem!");
                    verify = -1;
                }
                else
                {
                    System.Console.WriteLine("Hmm, I don't think I have that...");
                }
            }
        }
        static void BuyItem(string[] pocket, string[] items, int[] nookSell, ref int bells)
        {
            string boughtItem = "";
            int verify = 0;
            while (boughtItem.ToUpper() != "STOP" && verify != -1)
            {
                System.Console.WriteLine("What can I get for you?");
                int check = ItemsForSale(items, nookSell);
                System.Console.WriteLine("...Or would you like to STOP?");
                boughtItem = Console.ReadLine();
                Console.Clear();
                if (boughtItem.ToUpper() != "STOP")
                {
                    int foundItem = TakeBells(items, nookSell, ref bells, boughtItem, check);
                    if (foundItem == 0)
                    {
                        AddInventory(pocket, boughtItem);
                        verify = KeepShopMenuing("buy");
                    }
                    else if (foundItem == 1)
                    {
                        System.Console.WriteLine("I'm sorry, you don't have enough bells...");
                    }
                    else
                    {
                        System.Console.WriteLine("Sorry, what was that?");
                    }
                }
            }
        }
        static int ItemsForSale(string[] items, int[] price)
        {
            int i = 0;
            while (price[i] != 0)
            {
                System.Console.WriteLine($"{items[i]} \t {price[i]}");
                i++;
            }
            return i;
        }
        static int KeepShopMenuing(string action)
        {
            int verify = 1;
            while (verify > 0)
            {
                System.Console.WriteLine($"Do you have more to {action}?");
                System.Console.WriteLine("1. Yes");
                System.Console.WriteLine("2. No");
                string userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    verify = 0;
                }
                else if (userInput == "2")
                {
                    verify = -1;
                }
                else
                {

                    System.Console.WriteLine("I'm sorry, what was that?");

                }
            }
            return verify;
        }
        //Redd Shop
        static void ReddShop(string[] character, string[] pocket, int bells)
        {
            string userInput = "";
            System.Console.WriteLine($"Welcome, cuz! Hmmm? {character[1]} is your name? \nOh, we don't worry about that. Everyone is a cousin here, cuz.\n Welcome to Redd's Shop of Magic and Mystery!!! What can I do for ya'?");
            while (userInput != "4")
            {
                ReddMenu();
                userInput = Console.ReadLine();
                Console.Clear();
                switch (userInput)
                {
                    case "1":
                        break;
                    case "2":
                        System.Console.WriteLine("...Why, of course we can play a little game!");
                        ReddOffer(pocket, bells);
                        break;
                    case "3":
                        System.Console.WriteLine("A tree you say!");
                        ReddTreesConfirm(ref bells);
                        break;
                    case "4":
                        System.Console.WriteLine($"Well, I'm sorry to see you go, but feel free to return... \nfor as long as I'm still in {character[5]}.");
                        break;
                    default:
                        System.Console.WriteLine("Sorry, what was that, cuz?");
                        break;
                }
            }
        }
        static void ReddMenu()
        {
            System.Console.WriteLine("1. What is this place?");
            System.Console.WriteLine("2. Care for a game?");
            System.Console.WriteLine("3. Can you help me with a tree?");
            System.Console.WriteLine("4. I think that I'll go...");
        }
        static void ReddOffer(string[] pocket, int bells)
        {
            System.Console.WriteLine("Let's play... Higer or Lower! The most popular game this side of the Great Sea! \nHere's the rules, cuz. I'll flip a card over in front of ya' and you'll\n say whether you think the next card in my deck here is higher or lower\n and don't worry, I'll make sure to shuffle real nice, heh. All good games have some stakes though,\n How's about we wager something from ya' pockets! I'll buy it for double it's value if\n you win! Else, it's all mine, cuz!");
            System.Console.WriteLine("Press any key to let me know you're ready!");
            Console.ReadKey();
            Console.Clear();
            Random r = new Random();
            int verify = 0;
            string[] items = new string[100];
            int[] reddBuy = new int[100];
            LoadRedd(items, reddBuy);
            while (verify != -1)
            {
                System.Console.WriteLine("Let's see, what are ya offering?");
                WritePockets(pocket);
                System.Console.WriteLine("... or would you like to STOP?");
                string item = Console.ReadLine().ToUpper();
                int pocketPosition = SearchPocket(pocket, item);
                if (pocketPosition != -1)
                {
                    pocket[pocketPosition] = null;
                    bool win = ReddGame();
                    if (win == true)
                    {
                        GiveBells(items, reddBuy, ref bells, item);
                    }
                    verify = -1;
                }
                else if (item == "STOP")
                {
                    System.Console.WriteLine("No problem!");
                    verify = -1;
                }
                else
                {
                    System.Console.WriteLine("Hmm, I don't think I have that...");
                }
            }
        }
        static void ReddExplain()
        {
            System.Console.WriteLine("This is Redd's Shop of Magic and Mystery, cuz!\nI tend to drift into town about every Sunday or\nso to provide my services to my favorite town of... whatever this place is!\nSo, take a look around, don't be shy and remember my motto\n'Satisfaction guaranteed, so there's no need for a refund!'");
        }
        static void ReddTreesConfirm(ref int bells)
        {
            System.Console.WriteLine("I should be able to help, cuz. For a reasonable fee of course. Should cost ya, say, 20,000 Bells. What do ya think?");
            System.Console.WriteLine("1. Yes");
            System.Console.WriteLine("2. No");
            string userInput = Console.ReadLine();
            Console.Clear();
            if (bells < 20000)
            {
                System.Console.WriteLine("Hmmm, I think there's a misundertanding, cuz. I would do anything for ya, but you've got to\nhelp me out here! Come back when you have the Bells!");
            }
            while (userInput != "2" && bells >= 20000)
            {
                switch (userInput)
                {
                    case "1":
                        ReddTrees(ref bells);
                        userInput = "2";
                        break;
                    case "2":
                        break;
                    default:
                        System.Console.WriteLine("Come again, cuz?");
                        System.Console.WriteLine("1. Yes");
                        System.Console.WriteLine("2. No");
                        userInput = Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
        static void ReddTrees(ref int bells)
        {
            FruitTrees[] plantedTrees = new FruitTrees[32];
            LoadFruit(plantedTrees);
            int space = SearchSpace(plantedTrees);
            if (space != -1)
            {
                string[] types = new string[10];
                int count = GetAllFish(types, "fruittrees.txt");
                System.Console.WriteLine("What type of tree do you want?");
                for (int i = 0; i < count; i++)
                {
                    System.Console.WriteLine(types[i]);
                }
                string treeType = Console.ReadLine();
                Console.Clear();
                CheckTreeSelection(types, ref treeType, count);
                int[] temp = new int[6];
                GetDate(temp);
                FruitTrees f = new FruitTrees();
                f.SetTreeType(treeType);
                f.SetDay(temp[2]);
                f.SetMonth(temp[1]);
                f.SetYear(temp[0]);
                f.SetExists(true);
                plantedTrees[space] = f;
                System.Console.WriteLine($"There you go cuz, you should be getting a {plantedTrees[space].GetTreeType()} tree. No refunds!");
                bells = bells - 20000;
                SaveFruit(plantedTrees);
            }
            else
            {
                System.Console.WriteLine("Sorry, cuz. I'm not sure you have the space for another tree.");
            }
        }
        static void CheckTreeSelection(string[] types, ref string treeType, int count)
        {
            bool verify = false;
            while (verify == false)
            {
                for (int i = 0; i < count; i++)
                {
                    if (types[i].ToLower() == treeType.ToLower())
                    {
                        verify = true;
                    }
                }
                if (verify == false)
                {
                    System.Console.WriteLine("Sorry, what type of tree do you want?");
                    for (int i = 0; i < count; i++)
                    {
                        System.Console.WriteLine(types[i]);
                    }
                    treeType = Console.ReadLine();
                    Console.Clear();
                }
            }
        }
        static void LoadRedd(string[] items, int[] reddBuy)
        {
            StreamReader inFile = new StreamReader("items.txt");
            string line = inFile.ReadLine();
            int count = 0;
            while (line != null)
            {
                string[] temp = line.Split("#");
                items[count] = temp[0];
                reddBuy[count] = int.Parse(temp[1]) * 2;
                line = inFile.ReadLine();
                count++;
            }
            inFile.Close();
        }
        static bool ReddGame()
        {
            string[] deck = new string[52];
            int[] deckValues = new int[52];
            int m = 0;
            bool win = true;
            MakeDeck(deck, deckValues);
            Shuffle(deck, deckValues);
            ReddGameMenu(deck, m);
            string userInput = Console.ReadLine();
            Console.Clear();
            while (userInput != "1" && userInput != "2")
            {
                System.Console.WriteLine("Sorry cuz, ya gotta make a choice.");
                ReddGameMenu(deck, m);
                userInput = Console.ReadLine();
                Console.Clear();
            }
            if (deckValues[m] < deckValues[m + 1] && userInput == "1")
            {
                win = true;
                System.Console.WriteLine("Hmph. Well done, I guess.");
                System.Console.WriteLine($"The next card is a {deck[m + 1]}.");
                System.Console.WriteLine("(Press any key to continue)");
                Console.ReadKey();
                Console.Clear();
            }
            else if (deckValues[m] > deckValues[m + 1] && userInput == "2")
            {
                win = true;
                System.Console.WriteLine("A lucky guess...");
                System.Console.WriteLine($"The next card is a {deck[m + 1]}.");
                System.Console.WriteLine("(Press any key to continue)");
                Console.ReadKey();
                Console.Clear();
            }
            else if (deckValues[m] == deckValues[m + 1])
            {
                win = false;
                System.Console.WriteLine("Well, that's just bad luck, cuz!");
                System.Console.WriteLine($"The next card is a {deck[m + 1]}.");
                System.Console.WriteLine("(Press any key to continue)");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                System.Console.WriteLine("Shoot! That's a real shame, cuz! I hate to see it");
                System.Console.WriteLine($"The next card is a {deck[m + 1]}.");
                System.Console.WriteLine("(Press any key to continue)");
                Console.ReadKey();
                Console.Clear();
            }
            return win;
        }
        static void MakeDeck(string[] deck, int[] deckValues)
        {
            string[] ranks = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            string[] suits = new string[] { " of Spades", " of Clubs", " of Diamonds", " of Hearts" };
            int deckTracker = 0;
            for (int rankCount = 0; rankCount < 13; rankCount++)
            {
                for (int suitsCount = 0; suitsCount < 4; suitsCount++)
                {
                    deck[deckTracker] = ranks[rankCount] + suits[suitsCount];
                    deckValues[deckTracker] = rankCount + 1;
                    deckTracker++;
                }
            }
        }
        static void Shuffle(string[] deck, int[] deckValues)
        {
            Random r = new Random();
            for (int m = 0; m < 52; m++)
            {
                int randomSpot = r.Next(0, 52);
                string holdDeck = deck[m];
                int holdValue = deckValues[m];
                deck[m] = deck[randomSpot];
                deck[randomSpot] = holdDeck;
                deckValues[m] = deckValues[randomSpot];
                deckValues[randomSpot] = holdValue;
            }
        }
        static void ReddGameMenu(string[] deck, int m)
        {
            System.Console.WriteLine($"The face up card is {deck[m]}. Do you think the next card is higher or lower?");
            System.Console.WriteLine("1. Higher");
            System.Console.WriteLine("2. Lower");
        }
        //Fishing
        static void FishingRoute(int[] date, string[] character, string[] pocket)
        {
            string userInput = "";
            string fishingLocation = "";
            while (userInput != "3")
            {
                System.Console.WriteLine("Where should I go fishing?");
                System.Console.WriteLine("1. The River");
                System.Console.WriteLine("2. The Sea");
                System.Console.WriteLine("3. Quit");
                userInput = Console.ReadLine();
                Console.Clear();
                switch (userInput)
                {
                    case "1":
                        fishingLocation = "riverfish.txt";
                        FishingMinigame(pocket, fishingLocation);
                        break;
                    case "2":
                        fishingLocation = "seafish.txt";
                        FishingMinigame(pocket, fishingLocation);
                        break;
                    case "3":
                        System.Console.WriteLine("I think I'll head back");
                        break;
                    default:
                        System.Console.WriteLine("Hmm, I'm not quite sure...");
                        break;

                }
            }
        }
        static void FishingMinigame(string[] pocket, string fishingLocation)
        {
            Random rnd = new Random();
            int findTime = rnd.Next(3000, 12000);
            int fishTime = rnd.Next(2000, 10000);
            string[] fish = new string[100];
            string pickedFish = PickFish(fish, fishingLocation);
            System.Console.WriteLine("Alright! Let's see what I can find.");
            TimeSpan acceptedReactionTime = new TimeSpan(0, 0, 0, 1, 0);
            System.Threading.Thread.Sleep(fishTime);
            Console.Clear();
            System.Console.WriteLine("Oh look! I think I see a fish!");
            System.Console.WriteLine("Press any Button to cast my line!");
            Console.ReadKey();
            Console.Clear();
            System.Threading.Thread.Sleep(fishTime);
            System.Console.WriteLine("A Bite!");
            var stopwatch = Stopwatch.StartNew();
            // char thing;
            // thing = Console.ReadKey();
            stopwatch.Start();
            Console.ReadKey();
            // if(thing!= null);
            stopwatch.Stop();
            Console.Clear();
            TimeSpan reaction = stopwatch.Elapsed;
            System.Console.WriteLine(reaction);
            if (reaction < acceptedReactionTime)
            {
                System.Console.WriteLine("Caught it!");
                System.Console.WriteLine($"I caught a {pickedFish}!");
                AddInventory(pocket, pickedFish);
            }
            else
            {
                System.Console.WriteLine("It got away...");
            }
        }
        static string PickFish(string[] fish, string fishType)
        {
            int count = GetAllFish(fish, fishType);
            Random rnd = new Random();
            int rndNum = rnd.Next(0, count);
            string pickedFish = fish[rndNum];
            return pickedFish;
        }
        static int GetAllFish(string[] fish, string fishType)
        {
            int count = 0;
            StreamReader inFile = new StreamReader($"{fishType}");
            string line = inFile.ReadLine();
            while (line != null)
            {
                fish[count] = line;
                count++;
                line = inFile.ReadLine();
            }
            inFile.Close();
            return count;
        }
        //Fruit Trees
        static void FruitTrees(int[] date, string[] character, string[] pockets)
        {
            FruitTrees[] plantedTrees = new FruitTrees[32];
            LoadFruit(plantedTrees);
            System.Console.WriteLine("Let's see how the orchard is doing!");
            FruitMenu();
            string userInput = Console.ReadLine();
            Console.Clear();
            while (userInput != "4")
            {
                switch (userInput)
                {
                    case "1":
                        CheckTrees(pockets, plantedTrees);
                        FruitMenu();
                        userInput = Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        PlantTrees(plantedTrees);
                        FruitMenu();
                        userInput = Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        DigTrees(plantedTrees);
                        FruitMenu();
                        userInput = Console.ReadLine();
                        Console.Clear();
                        break;
                    case "4":
                        break;
                    default:
                        System.Console.WriteLine("I'm not sure what I'm trying to do...");
                        FruitMenu();
                        userInput = Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
            SaveFruit(plantedTrees);
        }
        static void FruitMenu()
        {
            System.Console.WriteLine("What should I do?");
            System.Console.WriteLine("1. Check the trees");
            System.Console.WriteLine("2. Plant some new trees");
            System.Console.WriteLine("3. Dig up some old trees");
            System.Console.WriteLine("4. Return to town");
        }
        static void LoadFruit(FruitTrees[] plantedTrees)
        {
            StreamReader inFile = new StreamReader("orchard.txt");
            string line = inFile.ReadLine();
            int count = 0;
            for (int i = 0; i < plantedTrees.Length; i++)
            {
                string[] temp = line.Split("#");
                FruitTrees current = new FruitTrees();
                current.SetTreeType(temp[0]);
                current.SetYear(int.Parse(temp[1]));
                current.SetMonth(int.Parse(temp[2]));
                current.SetDay(int.Parse(temp[3]));
                current.SetExists(bool.Parse(temp[4]));
                plantedTrees[i] = current;
                line = inFile.ReadLine();
                count++;
            }
            inFile.Close();
        }
        static void SaveFruit(FruitTrees[] plantedTrees)
        {
            StreamWriter outFile = new StreamWriter("orchard.txt");
            FruitTrees temp = new FruitTrees();
            for (int i = 0; i < 32; i++)
            {
                temp = plantedTrees[i];
                if (temp != null)
                {
                    outFile.WriteLine($"{temp.GetTreeType()}#{temp.GetYear()}#{temp.GetMonth()}#{temp.GetDay()}#{temp.GetExists()}");
                }
            }
            outFile.Close();
        }
        static void CheckTrees(string[] pockets, FruitTrees[] plantedTrees)
        {
            string menuChoice = "0";
            while (menuChoice != "3")
            {
                CheckFruitTreesMenu();
                menuChoice = Console.ReadLine();
                Console.Clear();
                switch (menuChoice)
                {
                    case "1":
                        System.Console.WriteLine("What tree should I check?");
                        WriteOrchard(plantedTrees);
                        System.Console.WriteLine("... or should I stop?");
                        string userInput = Console.ReadLine();
                        Console.Clear();
                        if (userInput.ToLower() != "stop")
                        {
                            int input = int.Parse(userInput);
                            if (plantedTrees[input].GetExists() == true)
                            {
                                if (plantedTrees[input].GetTreeType() != "nothing")
                                {
                                    bool verify = plantedTrees[input].CheckFruitTrees();
                                    if (verify == true)
                                    {
                                        PickFruit(pockets, plantedTrees[input]);
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("This tree is looking great! It's not quite ready to pick though...");
                                    }
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("There's no tree here!");
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Okay! I'll head back.");
                            menuChoice = "3";
                        }
                        break;
                    case "2":
                        System.Console.WriteLine("Here's what's growing!");
                        WriteOrchard(plantedTrees);
                        System.Console.WriteLine("(Press any button when done)");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        System.Console.WriteLine("Alrighty, I'll head back!");
                        break;
                    default:
                        System.Console.WriteLine("Hmmm, I'm not sure what I'm trying to do...");
                        break;
                }
            }
        }
        static void CheckFruitTreesMenu()
        {
            System.Console.WriteLine("What should I do?");
            System.Console.WriteLine("1. Check trees For fruit");
            System.Console.WriteLine("2. Check the plots");
            System.Console.WriteLine("3. Go back");
        }
        static void PickFruit(string[] pockets, FruitTrees tree)
        {
            Random r = new Random();
            int fruit = r.Next(1, 3);
            string temp = tree.GetTreeType();
            for (int i = 0; i < fruit; i++)
            {
                AddInventory(pockets, temp);
            }
        }
        static void PlantTrees(FruitTrees[] plantedTrees)
        {
            int space = SearchSpace(plantedTrees);
            if (space != -1)
            {
                string[] types = new string[10];
                int count = GetAllFish(types, "fruittrees.txt");
                // for (int i = 0; i < count; i++)
                // {
                //     System.Console.WriteLine(types[i]);
                // }
                Random r = new Random();
                int treeType = r.Next(0, 11);
                int[] temp = new int[6];
                GetDate(temp);
                FruitTrees f = new FruitTrees();
                f.SetTreeType(types[treeType]);
                f.SetDay(temp[2]);
                f.SetMonth(temp[1]);
                f.SetYear(temp[0]);
                f.SetExists(true);
                plantedTrees[space] = f;
                System.Console.WriteLine($"Yes! I planted my sapling. I think it will become a {plantedTrees[space].GetTreeType()} tree!");
            }
            else
            {
                System.Console.WriteLine("Oops, I don't have any more room in the orchard. Cut down some trees if you want to make space");
            }
        }
        static void DigTrees(FruitTrees[] plantedTrees)
        {
            int verify = 0;
            while (verify != -1)
            {
                System.Console.WriteLine("Here are the trees I have. What plot should I get dig up?");
                WriteOrchard(plantedTrees);
                System.Console.WriteLine("... or should I STOP?");
                int plot = int.Parse(Console.ReadLine());
                string searchVal = plantedTrees[plot].GetTreeType();
                int pocketPosition = SearchPocket(plantedTrees, searchVal);
                if (pocketPosition != -1)
                {
                    plantedTrees[pocketPosition].SetExists(false);
                    verify = -1;
                }
                else if (searchVal.ToLower() == "stop")
                {
                    System.Console.WriteLine($"I think I'll leave things as they are.");
                    verify = -1;
                }
                else
                {
                    System.Console.WriteLine("Hmm, what tree am I trying to dig?");

                }
            }
        }
        static void WriteOrchard(FruitTrees[] plantedTrees)
        {
            int j = 0;
            for (int i = 0; i < 32; i++)
            {
                if (plantedTrees[i].GetExists() == true)
                {
                    if (plantedTrees[i].GetTreeType().Length <= 6)
                    {
                        System.Console.Write((i + 1) + ". " + plantedTrees[i].GetTreeType() + " \t \t");
                    }
                    else
                    {
                        System.Console.Write(plantedTrees[i].GetTreeType() + " \t");
                    }
                    j++;
                }
                if (j == 4)
                {
                    System.Console.WriteLine();
                    j = 0;
                }
            }
        }
        //Pockets
        static void CheckPockets(string[] pocket, int bells)
        {
            System.Console.WriteLine("Let's see here...");
            WritePockets(pocket);
            System.Console.WriteLine($"I currently have {bells} bells.");
            System.Console.WriteLine("(Press Any Key To Continue)");
            Console.ReadKey();
        }
        static void WritePockets(string[] pocket)
        {
            int j = 0;
            for (int i = 0; i < 32; i++)
            {
                if (pocket[i] != null)
                {
                    if (pocket[i].Length <= 6)
                    {
                        System.Console.Write(pocket[i] + " \t \t");
                    }
                    else
                    {
                        System.Console.Write(pocket[i] + " \t");
                    }
                    j++;
                }
                if (j == 4)
                {
                    System.Console.WriteLine();
                    j = 0;
                }
            }
        }
        static void AddInventory(string[] pocket, string item)
        {
            int pocketPosition = SearchSpace(pocket);
            if (pocketPosition != -1)
            {
                pocket[pocketPosition] = item;
                System.Console.WriteLine("I added it to my pocket!");
            }
            else
            {
                System.Console.WriteLine("I don't have any more space...");
                System.Console.WriteLine("Should I get rid of something?");
                System.Console.WriteLine("1. Yes");
                System.Console.WriteLine("2. No");
                string userInput = Console.ReadLine();
                Console.Clear();
                switch (userInput)
                {
                    case "1":
                        TossItem(pocket, item);
                        break;
                    case "2":
                        System.Console.WriteLine("Okay, well I'll just get rid of this then...");
                        break;
                    default:
                        System.Console.WriteLine("I'm not sure what I'm trying to do...");
                        break;
                }
            }
        }
        static int SearchSpace(string[] pocket)
        {
            int pocketPosition = -1;
            for (int i = 0; i < 32; i++)
            {
                if (pocket[i] == null)
                {
                    pocketPosition = i;
                    return pocketPosition;
                }
            }
            return pocketPosition;
        }
        static int SearchSpace(FruitTrees[] pocket)
        {
            int pocketPosition = -1;
            for (int i = 0; i < 32; i++)
            {
                if (pocket[i].GetExists() == false)
                {
                    pocketPosition = i;
                    return pocketPosition;
                }
            }
            return pocketPosition;
        }
        static int SearchPocket(string[] pocket, string searchVal)
        {
            int pocketPosition = -1;
            for (int i = 0; i < 32; i++)
            {
                if (pocket[i].ToLower() == searchVal.ToLower())
                {
                    pocketPosition = i;
                    return pocketPosition;
                }
            }
            return pocketPosition;
        }
        static int SearchPocket(FruitTrees[] pocket, string searchVal)
        {
            int pocketPosition = -1;
            for (int i = 0; i < 32; i++)
            {
                if (pocket[i].GetTreeType().ToLower() == searchVal.ToLower())
                {
                    pocketPosition = i;
                    return pocketPosition;
                }
            }
            return pocketPosition;
        }
        static void TossItem(string[] pocket, string item)
        {
            int verify = 0;
            while (verify != -1)
            {
                System.Console.WriteLine("What should I get rid of?");
                WritePockets(pocket);
                System.Console.WriteLine("... or should I STOP?");
                string searchVal = Console.ReadLine();
                Console.Clear();
                int pocketPosition = SearchPocket(pocket, searchVal);
                if (pocketPosition != -1)
                {
                    pocket[pocketPosition] = item;
                    verify = -1;
                    System.Console.WriteLine($"Okay! I added {item} to my pockets!");
                }
                else if (searchVal.ToLower() == "stop")
                {
                    System.Console.WriteLine($"Okay, I'll just get rid of {item} then!");
                    verify = -1;
                }
                else
                {
                    System.Console.WriteLine("Hmm, I don't think I have that...");

                }
            }
        }

        //General Functions
        static void GetDate(int[] date)
        {
            DateTime localDate = DateTime.Now;
            date[0] = localDate.Year;
            date[1] = localDate.Month;
            date[2] = localDate.Day;
            date[3] = (int)localDate.DayOfWeek;
            date[4] = localDate.Hour;
            date[5] = localDate.Minute;
        }
        static string DayOfWeek(int[] date)
        {
            string weekDay = "";
            if (date[3] == 1)
            {
                weekDay = "Monday";
            }
            else if (date[3] == 2)
            {
                weekDay = "Tuesday";
            }
            else if (date[3] == 3)
            {
                weekDay = "Wednesday";
            }
            else if (date[3] == 4)
            {
                weekDay = "Thursday";
            }
            else if (date[3] == 5)
            {
                weekDay = "Friday";
            }
            else if (date[3] == 6)
            {
                weekDay = "Saturday";
            }
            else
            {
                weekDay = "Sunday";
            }
            return weekDay;
        }
        static string MonthOfYear(int[] date)
        {
            string month = "";
            switch (date[1])
            {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;
            }
            return month;
        }
        static void CurrentTime(int[] date)
        {
            if (date[5] < 10)
            {
                System.Console.WriteLine($"The current time is {date[4]}:0{date[5]}");
            }
            else
            {
                System.Console.WriteLine($"The current time is {date[4]}:{date[5]}");
            }
        }
        static int GiveBells(string[] items, int[] nookBuy, ref int bells, string item)
        {
            for (int i = 0; i < 54; i++)
            {
                if (items[i].ToLower() == item.ToLower())
                {
                    bells = bells + nookBuy[i];
                    return nookBuy[i];
                }
            }
            return bells;
        }
        static int TakeBells(string[] items, int[] nookSell, ref int bells, string item, int check)
        {
            int verify = -1;
            for (int i = 0; i < check; i++)
            {
                if (items[i] == item && bells > nookSell[i])
                {
                    bells = bells - nookSell[i];
                    verify = 0;
                }
                else if (items[i] == item)
                {
                    verify = 1;
                }
            }
            return verify;
        }
    }
}