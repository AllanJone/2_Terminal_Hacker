using System;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Game congiguration data
    const string menuHint = "You may type menu at any time.";
    string[] level1Passwords = { "books", "aisle", "self", "password", "font", "borrow" };
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] level3Passwords = { "starfield", "telescope", "environment", "exploration", "astronauts" };

    //Game State
    int level;
    string password;

    enum Screen { MainScreen, Password, Win };
    Screen currentScreen = Screen.MainScreen;

    // Use this for initialization
    void Start()
    {
        ShowMainMenu("Hello Allan!");
    }

    void ShowMainMenu(string greeting)
    {
        currentScreen = Screen.MainScreen;
        Terminal.ClearScreen();
        Terminal.WriteLine(greeting);
        Terminal.WriteLine("What would you like to hack into ?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 for the Local Library");
        Terminal.WriteLine("Press 2 for the Police Station");
        Terminal.WriteLine("Press 3 for NASA!");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your Selection");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")// we can always go to the main screen
        {
            ShowMainMenu("Hello Allan!");
        }
        else if (input == "exit" || input == "quit" || input == "close")
        {
            Terminal.WriteLine("Close the tab");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainScreen)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else
        {
            Terminal.WriteLine("Please select a valid level");
            Terminal.WriteLine(menuHint);
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[UnityEngine.Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[UnityEngine.Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[UnityEngine.Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.Log("Invalid Level Numner");
                break;
        }
    }

    private void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a Book...");
                Terminal.WriteLine(@"
    _______
   /      //
  /      //
 /______//
(______(/
");
                break;
            case 2:
                Terminal.WriteLine("You got the Prison Key...");
                Terminal.WriteLine("Play again for a greater challenge");
                Terminal.WriteLine(@"
 __
/0 \_______
\__/-=' = '   
");
                break;
            case 3:
                Terminal.WriteLine("NASA");
                Terminal.WriteLine(@"
 _ __   __ _ ___  __ _
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___)\__,_|
");
                Terminal.WriteLine("Welcome to NASA's internal system....");
                break;
            default:
                Debug.Log("Invalid level reached");
                break;
        }
    }
}
