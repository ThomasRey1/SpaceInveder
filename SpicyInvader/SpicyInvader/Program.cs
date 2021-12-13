/*
 *  Auteur : Thomas Rey
 *  Date : 27.08.2021
 *  Lieu : ETML
 *  Description : Code principal du jeu Space invader
 */

using System;
using System.Collections.Generic;

namespace SpicyInvader
{
    /// <summary>
    /// Main code of the space invader game
    /// </summary>
    class Program
    {
        #region Properties
        // Properties
        static byte choice = 1;                                 // The choice option of the player
        static bool redo = true;                                // Redo a while loop until the player choose an option
        static bool soundGame = true;                           // The sound is ON or OFF
        static bool difficulty = false;                         // The difficulty level
        static List<int> score = new List<int>();               // The highscore of the game
        static List<string> scoreName = new List<string>();     // The name of the player than make the highscore
        static string middle = "";                              // Margin the contents
        #endregion

        #region Method
        /// <summary>
        /// Execute the different choice of the player
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //test test = new test();
            //Console.Read();

            Console.CursorVisible = false;
            Sound.SoundMenu(soundGame);
            Console.WindowWidth = 41;
            Console.WindowHeight = 20;

            for (int i = 0; i != Console.WindowWidth / 3; i++)
            {
                middle += " ";
            }

            // Execute the choice of the user
            do
            {
                Show();
                if (choice == 1)
                {
                    Console.WindowWidth = 120;
                    Console.WindowHeight = 36;
                    GameSetting NewGame = new GameSetting(difficulty, soundGame);
                    NewGame.GameStarted();
                    Console.Clear();

                    // Enter the name of the player and him score
                    Console.WindowWidth = 41;
                    Console.WindowHeight = 20;
                    score.Add(NewGame.GetScore());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Entrez votre speudo : ");
                    scoreName.Add(Console.ReadLine());

                    NewGame = null;

                    Console.Clear();
                }
                else if (choice == 2)
                {
                    Configure();
                }
                else if (choice == 3)
                {
                    ShowHighscore();
                }
                else if (choice == 4)
                {
                    Pertinent();
                }
                else if (choice == 5)
                {
                    Environment.Exit(0);
                }
            }
            while (redo);
        }

        /// <summary>
        /// Show the main menu
        /// </summary>
        /// <returns></returns>
        static byte Show()
        {
            #region Properties
            bool main = true;           // While loop for choose the option
            ConsoleKeyInfo keyInfo;     // Check the key than the player touch
            byte cursorY = 4;           // The position vertical of the cursor
            char cursor = '>';          // The forme of the cursor
            #endregion

            choice = 1;
            Console.ForegroundColor = ConsoleColor.White;

            // Show the main menu with its option
            Console.WriteLine("=========================================");
            Console.WriteLine("=             Space Invader             =");
            Console.WriteLine("=========================================\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}{1}", middle, cursor);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Jouer\n"); 
            Console.WriteLine("{0} Options\n", middle);
            Console.WriteLine("{0} Highscore\n", middle);
            Console.WriteLine("{0} A propos\n", middle);
            Console.WriteLine("{0} Quitter", middle);

            // Check the user's movement
            do
            {
                keyInfo = Console.ReadKey(true);
                // Move down the option
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    choice++;
                    if (choice > 5)
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, 4);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 9, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, 4, 9, 1, middle.Length + 2, 4);
                        choice = 1;
                        cursorY = 4;
                    }
                    else
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, cursorY + 2);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 9, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, cursorY + 2, 9, 1, middle.Length + 2, cursorY + 2);
                        cursorY += 2;
                    }
                }
                // Move up the option
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    choice--;
                    if (choice < 1)
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, 12);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 9, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, 12, 9, 1, middle.Length + 2, 12);
                        choice = 5;
                        cursorY = 12;
                    }
                    else
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, cursorY - 2);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 9, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, cursorY - 2, 9, 1, middle.Length + 2, cursorY - 2);
                        cursorY -= 2;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Enter)
                {
                    main = false;
                }
            }
            while (main);
            Console.Clear();
            return choice; // Return the number of the choice
        }

        /// <summary>
        /// Show the configuration
        /// </summary>
        static void Configure()
        {
            #region Properties
            bool main = true;           // While loop for choose the option
            ConsoleKeyInfo keyInfo;     // Check the key than the player touch
            byte option = 1;            // The choice option of the player
            byte cursorY = 4;           // The position vertical of the cursor
            char cursor = '>';          // The forme of the cursor
            #endregion

            // Write the menu option
            Console.WriteLine("=========================================");
            Console.WriteLine("=                 Option                =");
            Console.WriteLine("=========================================\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}{1}", middle, cursor);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Son : ");
            if (soundGame)
            {
                Console.WriteLine("ON \n");
            }
            else
            {
                Console.WriteLine("OFF\n");
            }
            Console.Write("{0} Difficulté : ", middle);
            if (difficulty)
            {
                Console.WriteLine("DIFFICILE\n");
            }
            else
            {
                Console.WriteLine("FACILE   \n");
            }
            Console.WriteLine("{0} Retour\n", middle);

            // Check the user's mouvement
            do
            {
                keyInfo = Console.ReadKey(true);
                // Move up the option
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    option++;
                    if (option > 3)
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, 4);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 23, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, 4, 23, 1, middle.Length + 2, 4);
                        option = 1;
                        cursorY = 4;
                    }
                    else
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, cursorY + 2);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 23, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, cursorY + 2, 23, 1, middle.Length + 2, cursorY + 2);
                        cursorY += 2;
                    }
                }
                // Move down the option
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    option--;
                    if (option < 1)
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, 8);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 23, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, 8, 23, 1, middle.Length + 2, 8);
                        option = 3;
                        cursorY = 8;
                    }
                    else
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, cursorY - 2);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 23, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, cursorY - 2, 23, 1, middle.Length + 2, cursorY - 2);
                        cursorY -= 2;
                    }
                }
                // If the player presses the spacebar or the enter, then activate or deactivate the option in question
                else if (keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Enter)
                {
                    if (option == 1)
                    {
                        soundGame = !soundGame;
                        Sound.SoundMenu(soundGame);
                        Console.SetCursorPosition(middle.Length + 8, cursorY);
                        if (soundGame)
                        {
                            Console.Write("ON ");
                        }
                        else
                        {
                            Console.Write("OFF");
                        }
                    }
                    else if (option == 2)
                    {
                        difficulty = !difficulty;
                        Console.SetCursorPosition(middle.Length + 15, cursorY);
                        if (difficulty)
                        {
                            Console.Write("DIFFICILE");
                        }
                        else
                        {
                            Console.Write("FACILE   ");
                        }
                    }
                    else
                    {
                        main = false;
                    }
                }
            }
            while (main);
            Console.Clear();
        }

        /// <summary>
        /// Show to the player the score board
        /// </summary>
        static void ShowHighscore()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("=               Highscore               =");
            Console.WriteLine("=========================================\n");

            // Write the list of player and their score
            Console.ForegroundColor = ConsoleColor.Green;
            if (scoreName.Count != 0)
            {
                for (int i = 0; i < scoreName.Count; i++)
                {
                    Console.WriteLine("{0} {1} : {2}\n", middle, scoreName[i], score[i]);
                }
            }
            else
            {
                Console.WriteLine("{0} -",middle);
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Show the commande to the player
        /// </summary>
        static void Pertinent()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("=                À propos               =");
            Console.WriteLine("=========================================\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} Fléche directionnel ", middle);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":\n{0}      Se déplacer\n", middle);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} Espace ", middle);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":\n{0}      Tirer\n", middle);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} P/esc ", middle);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":\n{0}      Pause\n", middle);

            Console.ReadKey(true);
            Console.Clear();
        }
        #endregion
    }
}
