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
    class Move
    {
        private Ennemy[] ennemies;
        
        public Move(Ennemy[] ennemies)
        {
            this.ennemies = ennemies;
            Timer ennemyMovement = new Timer(200);
            ennemyMovement.Elapsed += new ElapsedEventHandler(EnnemyControl);
            ennemyMovement.Enabled = true;
        }

        public void EnnemyControl(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i != ennemies.Length; i++)
            {
                if (ennemies[i] != null && ennemies[i].ShipEnnemyLive == true)
                {
                    // If the lateral position is touching the left border of the window
                    if (ennemies[i].EnnemyX == Console.WindowLeft)
                    {
                        Console.MoveBufferArea(ennemies[i].EnnemyX, ennemies[i].EnnemyY, 5, 1, ennemies[i].EnnemyX, ennemies[i].EnnemyY + 1);     // descend the ship from one floor
                        ennemies[i].EnnemyY++;
                        ennemies[i].EnnemyDirection = !ennemies[i].EnnemyDirection;                                                     // Change the direction of the ship
                    }

                    // If the lateral position is touching the right border of the window
                    else if (ennemies[i].EnnemyX + 5 == Console.WindowWidth)
                    {
                        Console.MoveBufferArea(ennemies[i].EnnemyX, ennemies[i].EnnemyY, 5, 1, ennemies[i].EnnemyX, ennemies[i].EnnemyY + 1);     // descend the ship from one floor
                        ennemies[i].EnnemyY++;
                        ennemies[i].EnnemyDirection = !ennemies[i].EnnemyDirection;                                                     // Change the direction of the ship
                    }

                    // Else, move the ship to the left
                    if (ennemies[i].EnnemyDirection == true && ennemies[i].EnnemyX != Console.WindowLeft)
                    {
                        Console.MoveBufferArea(ennemies[i].EnnemyX, ennemies[i].EnnemyY, 5, 1, ennemies[i].EnnemyX - 1, ennemies[i].EnnemyY);
                        ennemies[i].EnnemyX--;
                    }

                    // Else, move the ship to the right
                    else if (ennemies[i].EnnemyDirection == false && ennemies[i].EnnemyX + 5 != Console.WindowWidth)
                    {
                        Console.MoveBufferArea(ennemies[i].EnnemyX, ennemies[i].EnnemyY, 5, 1, ennemies[i].EnnemyX + 1, ennemies[i].EnnemyY);
                        ennemies[i].EnnemyX++;
                    }
                }
                else
                {
                    ennemies[i] = null;
                }
                if(ennemies[i] != null)
                {
                    if (i >= 15)
                    {
                        ennemies[i].Shoot = true;
                    }
                    // If the ship forward is destroy, so the ship beward can shoot
                    else if (ennemies[i + 5] == null)
                    {
                        ennemies[i].Shoot = true;
                    }
                }
            }
        }
    }
}