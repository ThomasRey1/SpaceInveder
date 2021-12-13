/*
 *  Auteur : Thomas Rey
 *  Date : 27.08.2021
 *  Lieu : ETML
 *  Description : Code gerant les parametre du jeu
 */

using System;
using System.Collections.Generic;
using System.Threading;

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
        private Move move;                                  // Move the enemies ship
        private int score = 0;                              // The score of the game
        private int stage = 0;                              // The current stage whre is the player
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
            Console.SetCursorPosition(Console.WindowLeft + 10, Console.WindowHeight - 3);
            Console.Write("Vie : ♥ ♥ ♥");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight - 3);
            Console.Write("Score : {0}", score);
            Console.SetCursorPosition(Console.WindowWidth - 20, Console.WindowHeight - 3);
            Console.Write("Stage : {0}", stage);

            move = new Move(_enemies);
            player.ShipAction(move);

            bool redo = true;
            while (redo)
            {
                // When the player is dead
                Thread.Sleep(50);
                if (player.ShipLife == 0)
                {
                    Console.Clear();
                    move.StopMove(true);
                    move = null;
                    for (byte i = 0; i < _enemies.Length; i++)
                    {
                        if (_enemies[i] != null)
                        {
                            _enemies[i].Dead();
                        }
                        _enemies[i] = null;
                    }
                    score = player.Score;
                    player = null;
                    redo = false;
                }
                else
                {
                    move.StopMove(true);
                    move = null;
                    for (byte i = 0; i < _enemies.Length; i++)
                    {
                        if (_enemies[i] != null)
                        {
                            _enemies[i].Dead();
                        }
                        _enemies[i] = null;
                    }

                    Console.MoveBufferArea(player.ShipX, player.ShipY, player.ShipForm.Length, 1, Console.WindowWidth / 2 - 3, Console.WindowHeight - 7);
                    player.ShipX = Console.WindowWidth / 2 - 3;
                    Console.SetCursorPosition(player.ShipX, player.ShipY);
                    Console.Write(player.ShipForm);
                    stage++;
                    Console.SetCursorPosition(Console.WindowWidth - 12, Console.WindowHeight - 3);
                    Console.Write(stage);

                    CreatEnemy();
                    move = new Move(_enemies);
                    player.ShipAction(move);
                }
            }
        }

        /// <summary>
        /// Create the enemies and add them in the table
        /// </summary>
        public void CreatEnemy()
        {
            int x = 0;
            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5, Console.WindowTop + 2 * i + 2, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                Thread.Sleep(100);
            }

            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5 * 2 + 1, Console.WindowTop + 2 * i + 2, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                Thread.Sleep(100);
            }

            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5 * 3 + 2, Console.WindowTop + 2 * i + 2, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                System.Threading.Thread.Sleep(100);
            }

            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5 * 4 + 3, Console.WindowTop + 2 * i + 2, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                Thread.Sleep(100);
            }

            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5 * 5 + 4, Console.WindowTop + 2 * i + 2, SoundGame, true, true, _posXBunker, player);      // Create an enemy
                _enemies[x] = enemy;
                x++;
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Get the score of the game
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return score;
        }
        #endregion
    }
}