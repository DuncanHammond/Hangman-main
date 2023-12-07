using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HangmanConsole
{
    internal class Program
    {
        static bool gameOver = false;
        static bool guessedSuccessfully = false;
        static bool keepPlaying = false;
        static int totalNumberOfGuesses = 6; // After testing is done change this back from 3 to 6 lives. That is the standard for Hangman.

        // I plan on making word bank something that selects a difficulty level that then sets itself to a certain dictionary
        static Dictionary<string, List<string>> wordBank = new Dictionary<string, List<string>> {
            { "Food", new List<string>() { "banana", "cherry", "grapes", "cake", "cereal", "mushrooms", "steak", "milk", "honey", "smoothie", "cookie", "brownie", "candy", "pie", "truffle", "water", "strawberry", "tapioca", "malt", "rice", "bagel", "kiwi", "ribs", "shrimp" } },
            { "Animals", new List<string>() { "lion", "tiger", "bear", "human", "alligator", "pelican", "newt", "axlotl", "turkey", "shrew", "toad", "bull", "camel", "hippo", "elephant", "chicken", "beetle", "spider", "squid", "worm", "owl", "tragopan" } },
            { "Sports", new List<string>() { "archery", "skiing", "boxing", "swimming", "rugby", "cricket", "fencing", "sailing", "mogul", "fishing", "gymnastics", "taekwondo", "polo", "pool", "darts", "golf", "running", "badminton", "tennis", "karate" } },
            { "Colors", new List<string>() { "perrywinkle", "brown", "lilac", "indigo", "ruby", "goldenrod", "white", "black", "teal", "cyan", "magenta", "orange", "silver", "copper", "maroon", "green", "pink", "grey", "gray", "blue", "rose" } },
        };
        /*static Dictionary<string, List<string>> wordBankLevel1 = new Dictionary<string, List<string>> {
            { "Food", new List<string>() { "banana", "cherry", "grapes", "cake", "cereal", "mushrooms", "steak", "milk", "honey", "smoothie", "cookie", "brownie", "candy", "pie", "truffle", "water", "strawberry", "tapioca", "malt", "rice", "bagel", "kiwi", "ribs", "shrimp" } },
            { "Animals", new List<string>() { "lion", "tiger", "bear", "human", "alligator", "pelican", "newt", "axlotl", "turkey", "shrew", "toad", "bull", "camel", "hippo", "elephant", "chicken", "beetle", "spider", "squid", "worm", "owl", "tragopan" } },
            { "Sports", new List<string>() { "archery", "skiing", "boxing", "swimming", "rugby", "cricket", "fencing", "sailing", "mogul", "fishing", "gymnastics", "taekwondo", "polo", "pool", "darts", "golf", "running", "badminton", "tennis", "karate" } },
            { "Colors", new List<string>() { "perrywinkle", "brown", "lilac", "indigo", "ruby", "goldenrod", "white", "black", "teal", "cyan", "magenta", "orange", "silver", "copper", "maroon", "green", "pink", "grey", "blue", "rose" } },
        };
        static Dictionary<string, List<string>> wordBankLevel2 = new Dictionary<string, List<string>> {
            { "Food", new List<string>() { "banana", "cherry", "grapes", "cake", "cereal", "mushrooms", "steak", "milk", "honey", "smoothie", "cookie", "brownie", "candy", "pie", "truffle", "water", "strawberry", "tapioca", "malt", "rice", "bagel", "kiwi", "ribs", "shrimp" } },
            { "Animals", new List<string>() { "lion", "tiger", "bear", "human", "alligator", "pelican", "newt", "axlotl", "turkey", "shrew", "toad", "bull", "camel", "hippo", "elephant", "chicken", "beetle", "spider", "squid", "worm", "owl", "tragopan" } },
            { "Sports", new List<string>() { "archery", "skiing", "boxing", "swimming", "rugby", "cricket", "fencing", "sailing", "mogul", "fishing", "gymnastics", "taekwondo", "polo", "pool", "darts", "golf", "running", "badminton", "tennis", "karate" } },
            { "Colors", new List<string>() { "perrywinkle", "brown", "lilac", "indigo", "ruby", "goldenrod", "white", "black", "teal", "cyan", "magenta", "orange", "silver", "copper", "maroon", "green", "pink", "grey", "blue", "rose" } },
        };
        static Dictionary<string, List<string>> wordBankLevel2 = new Dictionary<string, List<string>> {
            { "Food", new List<string>()    { "banana", "cherry", "grapes", "cake", "cereal", "mushrooms", "steak", "milk", "honey", "smoothie", "cookie", "brownie", "candy", "pie", "truffle", "water", "strawberry", "tapioca", "malt", "rice", "bagel", "kiwi", "ribs", "shrimp" } },
            { "Animals", new List<string>() { "lion", "tiger", "bear", "human", "alligator", "pelican", "newt", "axlotl", "turkey", "shrew", "toad", "bull", "camel", "hippo", "elephant", "chicken", "beetle", "spider", "squid", "worm", "owl", "tragopan" } },
            { "Sports", new List<string>()  { "archery", "skiing", "boxing", "swimming", "rugby", "cricket", "fencing", "sailing", "mogul", "fishing", "gymnastics", "taekwondo", "polo", "pool", "darts", "golf", "running", "badminton", "tennis", "karate" } },
            { "Colors", new List<string>()  { "perrywinkle", "brown", "lilac", "indigo", "ruby", "goldenrod", "white", "black", "teal", "cyan", "magenta", "orange", "silver", "copper", "maroon", "green", "pink", "grey", "blue", "rose" } },
        };
        static Dictionary<string, List<string>> wordBankLevel2 = new Dictionary<string, List<string>> {
            { "Food", new List<string>()    { "banana", "cherry", "grapes", "cereal", "mushrooms", "steak", "milk", "honey", "smoothie", "cookie", "brownie", "candy", "pie", "truffle", "water", "strawberry", "tapioca", "malt", "rice", "bagel", "kiwi", "ribs", "shrimp" } },
            { "Animals", new List<string>() { "alligator", "pelican", "newt", "axlotl", "turkey", "shrew", "camel", "hippo", "elephant", "chicken", "beetle", "squid", "worm", "tragopan" } },
            { "Sports", new List<string>()  { "archery", "skiing", "boxing", "swimming", "rugby", "cricket", "fencing", "sailing", "mogul", "fishing", "gymnastics", "taekwondo", "polo", "pool", "darts", "running", "badminton",  "karate" } },
            { "Colors", new List<string>()  { "perrywinkle", "lilac", "indigo", "ruby", "goldenrod", "cyan", "magenta", "silver", "copper", "maroon", "rose" } },
        };*/

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Hangman! Choose a category by number: ");

                ShowCategoriesForSelection();
                string selectedCategory = wordBank.Keys.ElementAt(GetUserSelectedCategory()) ;
                string hangmanWord = GetRandomWordFromSelectedCategory(selectedCategory);
                PlayHangmanWithWord(hangmanWord, selectedCategory);
                
                keepPlaying = false;
                string playAgain = "";
                do{
                    Console.WriteLine("Would you like to play again? y/n: ");
                    playAgain = Console.ReadKey().KeyChar.ToString();
                    Console.WriteLine("");
                    if (playAgain.ToLower() == "y")
                    {
                        keepPlaying = true;
                        Console.Clear();
                        gameOver = false;
                        guessedSuccessfully = false;
                    }
                } while(playAgain.ToLower() != "n" && playAgain.ToLower() != "y");

            } while (keepPlaying);
        }

        private static void ShowCategoriesForSelection()
        {
            int keyIndex = 0;
            foreach (string key in wordBank.Keys)
            {
                Console.WriteLine((keyIndex + 1) + ": " + key);
                keyIndex++;
            }
        }

        /*private static void GetUserDifficultyLevel(string wordbank)
        {
            int selectedDifficultyLevel = 0;
            
            Console.WriteLine("Please choose a difficulty level (1-4))";
            // set wordBank equal whatever wordBankLevel is chosen

            int selectedValue = -1;

            while (selectedValue < 0 || selectedValue > wordBank.Count - 1)
            {
                Console.WriteLine(Environment.NewLine + "Please choose a valid difficulty by number: ");
                char selectedCategoryIndex = Console.ReadKey().KeyChar;
                selectedValue = (int)Char.GetNumericValue(selectedCategoryIndex) - 1; 
                Console.WriteLine("");
            }
            return selectedValue;


            return selectedDifficultyLevel;
        }*/

        private static int GetUserSelectedCategory()
        {
            int selectedValue = -1;

            while (selectedValue < 0 || selectedValue > wordBank.Count - 1)
            {
                Console.WriteLine(Environment.NewLine + "Please choose a valid category by number: ");
                char selectedCategoryIndex = Console.ReadKey().KeyChar;
                selectedValue = (int)Char.GetNumericValue(selectedCategoryIndex) - 1; 
                Console.WriteLine("");
            }
            return selectedValue;
        } 

        private static string GetRandomWordFromSelectedCategory(string selectedCategory)
        {
            Random rnd = new Random();
            int randNumber = rnd.Next(wordBank[selectedCategory].Count);
            // Console.WriteLine("This is the random number: " + randNumber); // used for testing purposes.
            string selectedWord = wordBank[selectedCategory].ElementAt(randNumber); // Figure out if I need to edit this to include the condition for which difficulty level is selected    // Edit to include spaces and special characters such as - and ' automatically into the blank spaces
            
            return selectedWord;
        }

        private static void PlayHangmanWithWord(string selectedWord, string selectedCategory)
        {
            List<string> invalidEntries = new List<string>();
            List<string> validEntries = new List<string>();
            while (!gameOver)
            {
                string maskedWord = GetMaskedWord(selectedWord, validEntries, invalidEntries); 
                if (maskedWord.IndexOf("_") == -1)
                {
                    guessedSuccessfully = true;
                    break;
                }
                Console.WriteLine("\n\nCategory: " + selectedCategory);
                Console.WriteLine(Environment.NewLine + maskedWord);
                Console.WriteLine("Incorrect guesses: " + string.Join(", ", invalidEntries));
                Console.WriteLine("Guesses left: " + (totalNumberOfGuesses - invalidEntries.Count));
                Console.WriteLine("Guess another letter: ");

                string selectedCharacter = Console.ReadKey().KeyChar.ToString();
                if (Regex.IsMatch(selectedCharacter, @"^[a-zA-Z]+$")) // using a regex to specify which characters are valid and invalid
                {
                    if (selectedWord.ToLower().IndexOf(selectedCharacter.ToLower()) != -1)
                    {
                        if (validEntries.FirstOrDefault(a => a.ToLower() == selectedCharacter.ToLower()) == null)
                            validEntries.Add(selectedCharacter);
                    }
                    else
                    {
                        if (invalidEntries.FirstOrDefault(a => a.ToLower() == selectedCharacter.ToLower()) == null)
                            invalidEntries.Add(selectedCharacter);
                    }
                    gameOver = (totalNumberOfGuesses - invalidEntries.Count) == 0;
                    Console.WriteLine(Environment.NewLine + "______________________________________________________________________________");
                }                
            }
            Console.WriteLine(Environment.NewLine + (guessedSuccessfully ? "Congratulations! You guessed the word! It was: " + selectedWord : "Sorry. Better luck next time!"));
        }

        private static string GetMaskedWord(string selectedWord, List<string> validCharacters, List<string> invalidCharacters) 
        {
            StringBuilder maskedString = new StringBuilder();
            string[] characterList = selectedWord.ToCharArray().Select(c => c.ToString()).ToArray();
            foreach (string character in characterList)
            {
                maskedString.Append(validCharacters.FirstOrDefault(a => a.ToLower() == character.ToLower()) != null ? character : "_");                 
            }
            return maskedString.ToString();
        }
    }
}