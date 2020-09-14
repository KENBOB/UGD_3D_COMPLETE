using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
        //ShowMainMenu("Hello User");
    }
    void ShowMainMenu()
    {
        var greeting = "Hello User";

        Terminal.ClearScreen();
        Terminal.WriteLine(greeting);
        Terminal.WriteLine("What would you like to hack into?\n");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("Enter your selection: ");
    }
    void OnUserInput(string input)
    {
        if(input == "menu")
        {
            ShowMainMenu();
        } else if (input == "1")
        {
            Terminal.WriteLine("You chose local library.");
        } else if(input == "2")
        {
            Terminal.WriteLine("You chose police station.");
        } else if(input == "3")
        {
            Terminal.WriteLine("You chose NASA.");
        } else
        {
            Terminal.WriteLine("Sorry does not compute");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
