//comment these out to introduce the random generator with no conflicting database logic
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    
    //Game conifuration array password data
    const string menuHint = ("Type 'menu' to return to main menu.");
    string[] level1Passwords = { "donkey", "pony", "zebra", "stallion", "stede" };
    string[] level2Passwords = { "elephant", "rhino", "hippo", "monkey", "giraffe" };
    string[] level3Passwords = { "bobcat", "lion", "bear", "wolf", "tiger" };
    //string[] leaveGame = {"quit", "close", "exit", "q", "c", "e"};
    
    //Game state
    int level;
    string password;
    enum Screen {MainMenu, Password, Win};
    Screen currentScreen;
    
    // Start is called before the first frame update
    void Start() {
        ShowMainMenu();
    }

    //check for confidence in the randomness of the random range function
    // void Update(){
    //     int index= Random.Range(0, level1Passwords.length);
    //     print(index);
    // }

    //Main Menu Screen on bootup
    void ShowMainMenu() {
        var greeting = "Hello User!";
        
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine(greeting);
        Terminal.WriteLine("What would you like to hack into?\n");
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("Enter your selection: ");
    }
    // handles the main menu screen
    void OnUserInput(string input) {
        if(input == "menu") {
            ShowMainMenu();

        } else if (input == "quit" || input == "close" || input == "exit" || input == "q" || input == "c" || input == "e"){
            Terminal.WriteLine("If on the web close the tab.");
            Application.Quit();

        } else if (currentScreen == Screen.MainMenu){
            RunMainMenu(input);

        } else if (currentScreen == Screen.Password){
            CheckPassword(input);

        } else if (currentScreen == Screen.Win){
            Terminal.WriteLine(menuHint);
        }
    }
    //handles the input and changes the screen to password screen
    void RunMainMenu(string input) {
        bool isVlaidLevelNumber = (input == "1" || input == "2" || input == "3");
        if(isVlaidLevelNumber){
            level = int.Parse(input);
            StartGame();
        } else {
            ShowMainMenu();
            Terminal.WriteLine("\nPlease choose a valid level.");
            //Terminal.WriteLine(menuHint);
            
        }

        /*
         if (input == "1") {
            level = 1;
            rtPassword = level1Passwords[2];
            StartGame();
            //Terminal.WriteLine("You chose local library.");
        } 
        else if(input == "2") {
            level = 2;
            rtPassword = level2Passwords[3];
            StartGame();
            //Terminal.WriteLine("You chose police station.");
        } 
        else if(input == "3") {
            level = 3;
            rtPassword = level3Passwords[4];
            StartGame();
            //Terminal.WriteLine("You chose NASA.");
        } 
        else {
            Terminal.WriteLine("Please choose a valid level.");
        } 
        */
    }

    //clear screen and show password login screen
    void StartGame() {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("\nYou have chosen level " + level);
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine("HINT: " + password.Anagram() + "\nPlease enter your password: ");
    }
    
    //generate random passwords from arrays for each level
    void SetRandomPassword(){
        switch (level)
        {
            //codes the random word from the array rt as we make it fresh for all of them
            //starting at random.range so no need for individual indexes
            case 1:
                //int index1 = Random.Range(0, level1Passwords.Length);
                //password = level1Passwords[index1];
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                //Terminal.WriteLine("HINT: 4 legged animals that nay.");
                break;
            case 2:
                //int index2 = Random.Range(0, level2Passwords.Length);
                //password = level2Passwords[index2];
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                //Terminal.WriteLine("HINT: vegetarian zoo animals.");
                break;
            case 3:
                //int index3 = Random.Range(0, level3Passwords.Length);
                //password = level3Passwords[index3];
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                //Terminal.WriteLine("HINT: predatory zoo animals.");
                break;
            default:
                //logs error to console
                Debug.LogError("Invalid level number.");
                break;
        }
    }

    //verify password is correct and call display win screen function or catch all fail state
    void CheckPassword(string input){
        if(input == password) {
            DisplayWinScreen();
        } else {
            StartGame();
            //Terminal.WriteLine("Sorry, please try again.");
        }
    }

    //Display win Screen function amd call level reward function
    void DisplayWinScreen(){
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    //Display level rewards
    void ShowLevelReward(){
        switch(level)
        {
            case 1: 
                Terminal.WriteLine("Congratulations! Here is your book.\n" + menuHint);
                Terminal.WriteLine(@"
        _______
       /      //
      /      //
     /______//
    (______(/
                ");
                break;

            case 2: 
                Terminal.WriteLine("Congratulations! We have signaled the \nbatman to hunt your down now.\n" + menuHint);
                Terminal.WriteLine(@"
     /(_M_)\  
    |       |  
     \/~V~\/ 
                ");
                break;

            case 3: 
                Terminal.WriteLine("Congratulations! Here is NASA's X-Wing plans.\n" + menuHint);
                Terminal.WriteLine(@"
     ||_||
   __[ 0 ]__
   |  \ /  |
   *  | |  * 
       V
                ");
                break;
            default:
                //logs error to console
                Terminal.WriteLine(menuHint);
                Debug.LogError("Invalid level reached.");
                break;
        }
    }

}
