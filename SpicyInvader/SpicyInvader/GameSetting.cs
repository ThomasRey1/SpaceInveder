/*
 *  Auteur : Thomas Rey
 *  Date : 27.08.2021
 *  Lieu : ETML
 *  Description : Code gerant les parametre du jeu
 */

using System;
using System.Collections.Generic;

namespace SpicyInvader
{
    /// <summary>
    /// code managing game parameters
    /// </summary>
    public class GameSetting
    {
        // Property
        public List<int> posXBunker = new List<int>();

        //Getter - Setter
        /// <summary>
        /// Difficulty property definition
        /// </summary>
        public bool Difficulty { get; set; }            // The difficulty level
        /// <summary>
        /// SoundGame property definition
        /// </summary>
        public bool SoundGame { get; set; }             // The sound is ON or OFF


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="difficulty"></param>
        public GameSetting(bool difficulty, bool soundGame)
        {
            this.Difficulty = difficulty;
            this.SoundGame = soundGame;
        }

        /// <summary>
        /// Create the ennemies and the player and give the controle to the player
        /// </summary>
        public void GameStarted()
        {
            for (int i = 1; i != 5; i++)
            {
                Bunker bunker = new Bunker(i);                                                              // Create a bunker

                for (int j = 0; j != 18; j++)
                {
                    posXBunker.Add(Console.WindowWidth / 4 * i - 23 + j);
                }
            }
            Ennemy[] ennemies = new Ennemy[20];
            int x = 0;
            for (int i = 0; i != 5; i++)
            {
                Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 2, false, true, true, posXBunker);      // Create an ennemy
                ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
            for (int i = 0; i != 5; i++)
            {
                Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 4, false, true, true, posXBunker);      // Create an ennemy
                ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
            for (int i = 0; i != 5; i++)
            {
                Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 6, false, true, true, posXBunker);      // Create an ennemy
                ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
            for (int i = 0; i != 5; i++)
            {
                Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 8, false, true, true, posXBunker);      // Create an ennemy
                ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
            Move move = new Move(ennemies);

            PlayerShip player = new PlayerShip(Console.WindowWidth / 2 - 3, Console.WindowHeight - 2, SoundGame, posXBunker);       // Create the player ship
            player.ShipAction();
        }
    }
}