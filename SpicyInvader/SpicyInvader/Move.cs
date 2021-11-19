/*
 *  Auteur : Thomas Rey
 *  Date : 15.10.2021
 *  Lieu : ETML
 *  Description : Code gerant les mouvements des ennemies
 */

using System;
using System.Timers;
using System.Collections.Generic;

namespace SpicyInvader
{
    /// <summary>
    /// Code managing movement of the enemies
    /// </summary>
    public class Move
    {
        private Ennemy[] _ennemies;
        
        public Move(Ennemy[] ennemies)
        {
            this._ennemies = ennemies;
            Timer ennemyMovement = new Timer(200);
            ennemyMovement.Elapsed += new ElapsedEventHandler(EnnemyControl);
            ennemyMovement.Enabled = true;
        }

        public void EnnemyControl(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i != _ennemies.Length; i++)
            {
                if (_ennemies[i] != null && _ennemies[i].ShipEnnemyLive == true)
                {
                    // If the lateral position is touching the left border of the window
                    if (_ennemies[i].EnnemyX == Console.WindowLeft)
                    {
                        Console.MoveBufferArea(_ennemies[i].EnnemyX, _ennemies[i].EnnemyY, 5, 1, _ennemies[i].EnnemyX, _ennemies[i].EnnemyY + 1);     // descend the ship from one floor
                        _ennemies[i].EnnemyY++;
                        _ennemies[i].EnnemyDirection = !_ennemies[i].EnnemyDirection;                                                     // Change the direction of the ship
                    }

                    // If the lateral position is touching the right border of the window
                    else if (_ennemies[i].EnnemyX + 5 == Console.WindowWidth)
                    {
                        Console.MoveBufferArea(_ennemies[i].EnnemyX, _ennemies[i].EnnemyY, 5, 1, _ennemies[i].EnnemyX, _ennemies[i].EnnemyY + 1);     // descend the ship from one floor
                        _ennemies[i].EnnemyY++;
                        _ennemies[i].EnnemyDirection = !_ennemies[i].EnnemyDirection;                                                     // Change the direction of the ship
                    }

                    // Else, move the ship to the left
                    if (_ennemies[i].EnnemyDirection == true && _ennemies[i].EnnemyX != Console.WindowLeft)
                    {
                        Console.MoveBufferArea(_ennemies[i].EnnemyX, _ennemies[i].EnnemyY, 5, 1, _ennemies[i].EnnemyX - 1, _ennemies[i].EnnemyY);
                        _ennemies[i].EnnemyX--;
                    }

                    // Else, move the ship to the right
                    else if (_ennemies[i].EnnemyDirection == false && _ennemies[i].EnnemyX + 5 != Console.WindowWidth)
                    {
                        Console.MoveBufferArea(_ennemies[i].EnnemyX, _ennemies[i].EnnemyY, 5, 1, _ennemies[i].EnnemyX + 1, _ennemies[i].EnnemyY);
                        _ennemies[i].EnnemyX++;
                    }
                    
                }
                if(_ennemies[i] != null)
                {
                    if (i != 19 && i != 0 &&  (_ennemies[i + 1] != null || _ennemies[i - 1] != null))
                    {
                        //if (_ennemies[i].EnnemyX == _ennemies[i + 1].EnnemyX || _ennemies[i].EnnemyX == _ennemies[i - 1].EnnemyX)
                        //{
                        //    _ennemies[i].Shoot = false;
                        //}
                        //else
                        //{
                        foreach(Ennemy ennemy in _ennemies)
                        {

                        }
                            if (i >= 15)
                            {
                                _ennemies[i].Shoot = true;
                            }
                            // If the ship forward is destroy, so the ship beward can shoot
                            else if (_ennemies[i + 5] == null)
                            {
                                _ennemies[i].Shoot = true;
                            }
                        //}
                    }
                }
            }
        }
    }
}