/*
 *  Auteur : Thomas Rey
 *  Date : 27.08.2021
 *  Lieu : ETML
 *  Description : Code principal du jeu Space invader
 */

using System;

namespace SpicyInvader
{
    /// <summary>
    /// Main code of the space invader game
    /// </summary>
    class Program
    {
        /// <summary>
        /// Execute the different choice of the player
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            test test = new test();
            Console.Read();
            //// Properties
            //byte choice = 1;                // The choice option of the player
            //bool redo = true;               // Redo a while loop until the player choose an option
            //bool soundGame = true;          // The sound is ON or OFF
            //bool difficulty = false;        // The difficulty level

            //Sound.SoundMenu(soundGame);

            //// Execute the choice of the user
            //do
            //{
            //    Console.CursorVisible = false;
            //    Show(ref choice);
            //    Console.Clear();
            //    if (choice == 1)
            //    {
            //        GameSetting NewGame = new GameSetting(difficulty, soundGame);
            //        NewGame.GameStarted();

            //    }
            //    else if (choice == 2)
            //    {
            //        Configure(ref soundGame, ref difficulty);
            //    }
            //    else if (choice == 3)
            //    {

            //    }
            //    else if (choice == 4)
            //    {

            //    }
            //    else if (choice == 5)
            //    {
            //        Environment.Exit(0);
            //    }
            //    Console.Clear();
            //}
            //while (redo);
        }

        /// <summary>
        /// Show the main menu
        /// </summary>
        /// <param name="S_option">The choice option of the player</param>
        /// <returns></returns>
        static byte Show(ref byte S_option)
        {
            bool main = true;           // While loop for choose the option
            ConsoleKeyInfo keyInfo;     // Check the key than the player touch
            string middle = "";         // Margin the contents

            for (int i = 0; i != 44; i++)
            {
                middle += " ";
            }

            // Show the main menu with their option
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(middle + "=========================");
                Console.WriteLine(middle + "=     Space Invader     =");
                Console.WriteLine(middle + "=========================\n");


                if (S_option == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(middle + "> Jouer <\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(middle + "  Jouer\n");
                }
                if (S_option == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(middle + "> Options <\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(middle + "  Options\n");
                }
                if (S_option == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(middle + "> Highscore <\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(middle + "  Highscore\n");
                }
                if (S_option == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(middle + "> A propos <\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(middle + "  A propos\n");
                }
                if (S_option == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(middle + "> Quitter <");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(middle + "  Quitter");
                }

                // Check the user's movement
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    S_option++;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    S_option--;
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Enter)
                {
                    main = false;
                }

                if (S_option > 5)
                    S_option = 1;
                else if (S_option < 1)
                    S_option = 5;
                Console.Clear();
            }
            while (main);
            return S_option; // Return the number of the choice
        }

        /// <summary>
        /// Set up the configuration
        /// </summary>
        /// <param name="C_soundGame">activated or deactivated the sound</param>
        /// <param name="C_difficulty">Change the difficult mode</param>
        static void Configure(ref bool C_soundGame, ref bool C_difficulty)
        {
            bool main = true;           // While loop for choose the option
            ConsoleKeyInfo keyInfo;     // Check the key than the player touch
            string middle = "";         // Margin the contents
            byte option = 1;            // The choice option of the player

            for (int i = 0; i != 44; i++)
            {
                middle += " ";
            }
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(middle + "=========================");
                Console.WriteLine(middle + "=     Space Invader     =");
                Console.WriteLine(middle + "=========================\n");


                if (option == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(middle + "> Son : ");
                    if (C_soundGame == true)
                    {
                        Console.WriteLine("ON\n");
                    }
                    else
                    {
                        Console.WriteLine("OFF\n");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(middle + "  Son : ");
                    if (C_soundGame == true)
                    {
                        Console.WriteLine("ON\n");
                    }
                    else
                    {
                        Console.WriteLine("OFF\n");
                    }
                }
                if (option == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(middle + "> Difficulté :  ");
                    if (C_difficulty == false)
                    {
                        Console.WriteLine("FACILE\n");
                    }
                    else
                    {
                        Console.WriteLine("DIFFICILE\n");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(middle + "  Difficulté : ");
                    if (C_difficulty == false)
                    {
                        Console.WriteLine("FACILE\n");
                    }
                    else
                    {
                        Console.WriteLine("DIFFICILE\n");
                    }
                }
                if (option == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(middle + "> Retour \n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(middle + "  Retour\n");
                }

                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    option++;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    option--;
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Enter)
                {
                    if (option == 1)
                    {
                        C_soundGame = !C_soundGame;
                        Sound.SoundMenu(C_soundGame);
                    }
                    else if (option == 2)
                    {
                        C_difficulty = !C_difficulty;
                    }
                    else
                    {
                        main = false;
                    }
                }

                if (option > 3)
                    option = 1;
                else if (option < 1)
                    option = 3;
                Console.Clear();
            }
            while (main);
        }
    }
}
