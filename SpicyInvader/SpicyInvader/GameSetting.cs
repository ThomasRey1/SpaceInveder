/*
 *  Auteur : Thomas Rey
 *  Date : 27.08.2021
 *  Lieu : ETML
 *  Description : Code gerant les parametre du jeu
 */

using System;
using System.Collections.Generic;
using System.Timers;

namespace SpicyInvader
{
    /// <summary>
    /// code managing game parameters
    /// </summary>
    public class GameSetting
    {
        // Property
        private List<int> _posXBunker = new List<int>();
        private Ennemy[] _ennemies = new Ennemy[20];
        private PlayerShip player;

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
                    _posXBunker.Add(Console.WindowWidth / 4 * i - 23 + j);                                  // Save the position of the bunker
                }
            }

            player = new PlayerShip(Console.WindowWidth / 2 - 3, Console.WindowHeight - 2, SoundGame, _posXBunker, _ennemies);       // Create the player ship    

            int x = 0;
            for (int i = 0; i != 5; i++)
            {
                Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 2, SoundGame, true, true, _posXBunker, player);      // Create an ennemy
                _ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }

            for (int i = 0; i != 5; i++)
            {
                Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 4, SoundGame, true, true, _posXBunker, player);      // Create an ennemy
                _ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }

            for (int i = 0; i != 5; i++)
            {
                Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 6, SoundGame, true, true, _posXBunker, player);      // Create an ennemy
                _ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }

            for (int i = 0; i != 5; i++)
            {
                Ennemy ennemy = new Ennemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 8, SoundGame, true, true, _posXBunker, player);      // Create an ennemy
                _ennemies[x] = ennemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }

            Move move = new Move(_ennemies);   
            player.ShipAction();
        }
    }
}