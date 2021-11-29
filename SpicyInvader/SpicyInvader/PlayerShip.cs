/*
 *  Auteur : Thomas Rey
 *  Date : 03.09.2021
 *  Lieu : ETML
 *  Description : Code gerant le vaisseau du joueur
 */

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing the player's ship
    /// </summary>
    public class PlayerShip
    {
        #region Properties
        //Properties
        private byte _shipLife = 3;             // The limit life of the player
        public string _shipForm = "├─┴─┤";      // The shap of the player's ship
        private bool _gamePause = false;        // Check if the game is in pause
        private const byte _shipSpeed = 1;      // The ship movement speed
        private Enemy[] _enemies;             // List of enemies
        private bool _over = false;             // Check if the game is over or not
        #endregion

        #region Getter - Setter
        //Getter - Setter
        /// <summary>
        /// ShipX property definition
        /// </summary>
        public int ShipX { get; set; }                     // The lateral position of the ship

        /// <summary>
        /// ShipY property definition
        /// </summary>
        public int ShipY { get; set; }                    // The vertical position of the ship

        /// <summary>
        /// SoundGame property definition
        /// </summary>
        public bool SoundGame { get; set; }                // The sound is ON or OFF

        /// <summary>
        /// PosXBunker property definition
        /// </summary>
        public List<int> PosXBunker { get; set; }                           // Postition of the bunker

        /// <summary>
        /// ShipLife property definition
        /// </summary>
        public byte ShipLife
        {
            get { return _shipLife; }

            set { _shipLife = value; }
        }
        #endregion

        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shipX">The lateral position of the ship</param>
        /// <param name="shipY">The vertical position of the ship</param>
        /// <param name="soundGame">The sound is ON or OFF</param>
        public PlayerShip(int shipX, int shipY, bool soundGame, List<int> posXBunker, Enemy[] enemies)
        {
            this.ShipX = shipX;
            this.ShipY = shipY;
            this.SoundGame = soundGame;
            this.PosXBunker = posXBunker;
            this._enemies = enemies;
            Console.SetCursorPosition(ShipX, ShipY);
            Console.Write(_shipForm);
        }

        [DllImport("User32.dll")]                       // Import the User32.dll
        static extern short GetAsyncKeyState(int key);  // keyboard key pressed

        /// <summary>
        /// Allows the player to move and shoot with the ship and to pause the game
        /// </summary>
        public void ShipAction(Move move)
        {
            Missile missilePlayer = new Missile(1, 1, _gamePause, false, PosXBunker, _enemies) ; // Create a missile
            short GetAsyncKeyStateResult = GetAsyncKeyState(32); // Result of the keyboard key pressed
            do
            {
                // If the result of the keyboard key pressed is positiv and he's equal to the left arrow key
                if ((GetAsyncKeyStateResult & 0x8000) > 0 && GetAsyncKeyStateResult == GetAsyncKeyState(37))
                {
                    // If the game is not in pause
                    if (_gamePause == false)
                    {
                        // If the lateral position is not touching the left border of the window
                        if (ShipX != Console.WindowLeft)
                        {
                            ShipX--;
                            Console.MoveBufferArea(ShipX + 1, ShipY, _shipForm.Length, 1, ShipX, ShipY); // Move the ship to the left
                        }
                        // Else do nothing
                        else { }
                        System.Threading.Thread.Sleep(_shipSpeed);
                    }
                }
                // Else if the result of the keyboard key pressed is positiv and he's equal to the right arrow key
                else if ((GetAsyncKeyStateResult & 0x8000) > 0 && GetAsyncKeyStateResult == GetAsyncKeyState(39))
                {
                    // If the game is not in pause
                    if (_gamePause == false)
                    {
                        // If the lateral position plus the lenght of the ship is not touching the right border of the window
                        if (ShipX + _shipForm.Length != Console.WindowWidth)
                        {
                            ShipX++;
                            Console.MoveBufferArea(ShipX - 1, ShipY, _shipForm.Length, 1, ShipX, ShipY); // Move the ship to the right
                        }
                        // Else do nothing
                        else { }
                        System.Threading.Thread.Sleep(_shipSpeed);
                    }
                }
                // If a key is pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true); // Read the key pressed
                    // If the key pressed is the left arrow
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        GetAsyncKeyStateResult = GetAsyncKeyState(37); // Initialize the GetAsynKeyState in left arrow
                    }
                    // If the key pressed is the right arrow
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        GetAsyncKeyStateResult = GetAsyncKeyState(39); // Initialize the GetAsynKeyState in right arrow
                    }
                    // If the key pressed is the space bar
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Check if a missile is already launched
                        if (missilePlayer.MissileLive == false)
                        {
                            Sound.SoundShipShoot(SoundGame); // Play the shoot sound
                            missilePlayer.MissileLive = true;
                            // Reposition the location of the missile
                            missilePlayer.MissileY = ShipY - 1;
                            missilePlayer.MissileX = ShipX + (_shipForm.Length / 2);
                            missilePlayer.MissilePlayerCreate();
                        }
                    }
                    // If the key pressed is the P
                    else if (key.Key == ConsoleKey.P)
                    {
                        // The game is in pause
                        _gamePause = !_gamePause;
                        missilePlayer.MissilePause = _gamePause;
                        move.StopMove();
                        foreach(Enemy x in _enemies)
                        {
                            if(x != null)
                            {
                                x.StopShoot();
                            }
                        }
                    }
                }
                if(_shipLife == 0)
                {
                    _over = true;
                }
                byte i = 0;
                foreach(Enemy x in _enemies)
                {
                    if (x != null)
                    {
                        i++;
                    }
                }
                if(i == 0)
                {
                    _over = true;
                }
                i = 0;
            }
            while (_over == false);
            missilePlayer.StopShoot();
        }
        #endregion
    }
}