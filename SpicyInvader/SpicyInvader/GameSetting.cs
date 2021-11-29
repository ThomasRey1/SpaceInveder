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
        #region Properties
        // Properties
        private List<int> _posXBunker = new List<int>();    // List of bunker positions
        private Enemy[] _enemies = new Enemy[20];           // List of enemies
        private PlayerShip player;                          // The player ship
        #endregion

        #region Getter - Setter
        //Getter - Setter
        /// <summary>
        /// Difficulty property definition
        /// </summary>
        public bool Difficulty { get; set; }            // The difficulty level

        /// <summary>
        /// SoundGame property definition
        /// </summary>
        public bool SoundGame { get; set; }             // The sound is ON or OFF
        #endregion

        #region Method
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
        /// Create the enemies and the player and give the controle to the player
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

            player = new PlayerShip(Console.WindowWidth / 2 - 3, Console.WindowHeight - 7, SoundGame, _posXBunker, _enemies);       // Create the player ship    

            CreatEnemy();
            Console.SetCursorPosition(Console.WindowLeft, Console.WindowHeight - 6);
            for(int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("─");
            }
            Console.SetCursorPosition(Console.WindowLeft, Console.WindowHeight - 3);
            Console.Write("Vie : ♥ ♥ ♥");

            Move move = new Move(_enemies);   
            player.ShipAction(move);

            if(player.ShipLife == 0)
            {
                Console.Clear();
                for (byte i = 0; i < _enemies.Length; i++)
                {
                    if(_enemies[i] != null)
                    {
                        _enemies[i].StopShoot();
                    }
                    _enemies[i] = null;
                }
                player = null;
                move.StopMove();
                move = null;
            }
            else
            {
                for (byte i = 0; i < _enemies.Length; i++)
                {
                    if (_enemies[i] != null)
                    {
                        _enemies[i].StopShoot();
                    }
                    _enemies[i] = null;
                }
                move.StopMove();
                move = null;
            }
        }


        public void CreatEnemy()
        {
            int x = 0;
            for (int i = 0; i != 5; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 2, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }

            for (int i = 0; i != 5; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 4, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }

            for (int i = 0; i != 5; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 6, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }

            for (int i = 0; i != 5; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + (6 * i), Console.WindowTop + 8, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                System.Threading.Thread.Sleep(10);
            }
        }
        #endregion
    }
}