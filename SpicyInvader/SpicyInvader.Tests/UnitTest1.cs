using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SpicyInvader;
using System.Collections.Generic;

namespace SpicyInvader.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Collision_entre_2_missiles()
        {
            // Arrange
            List<int> posXBunker = new List<int>();
            Enemy[] enemies = new Enemy[20];
            PlayerShip player = new PlayerShip(1, 20, false, posXBunker, enemies);
            Enemy enemy = new Enemy(1, 10, false, true, posXBunker, player, false);


            // Act
            enemy.MissileEnemy.MissileLive = true;
            player.MissilePlayer.MissileLive = true;
            while (player.MissilePlayer.MissileY != 0)
            {
                // Check if the missile touch a another player's missile
                if (enemy.MissileEnemy.MissileX == player.MissilePlayer.MissileX && enemy.MissileEnemy.MissileY >= player.MissilePlayer.MissileY)
                {
                    enemy.MissileEnemy.MissileLive = false;
                    player.MissilePlayer.MissileLive = false;
                    break;
                }
                player.MissilePlayer.MissileY--;
            }
            

            //Assert
            Assert.AreEqual(false, enemy.MissileEnemy.MissileLive, "le missile doit être mort");
            Assert.AreEqual(false, player.MissilePlayer.MissileLive, "le missile doit être mort");
        }

        [TestMethod]
        public void Collision_entre_1_missile_et_1_ennemie()
        {
            // Arrange
            List<int> posXBunker = new List<int>();
            Enemy[] enemies = new Enemy[20];
            PlayerShip player = new PlayerShip(2, 20, false, posXBunker, enemies);
            Enemy enemy = new Enemy(1, 10, false, true, posXBunker, player, false);


            // Act
            while (player.MissilePlayer.MissileY != 0)
            {
                if (player.MissilePlayer.MissileY == enemy.EnemyY)
                {
                    if (player.MissilePlayer.MissileX >= enemy.EnemyX && player.MissilePlayer.MissileX <= enemy.EnemyX + enemy.ShipForm.Length - 1)
                    {
                        enemy.MissileEnemy = null;
                        //Assert
                        Assert.AreEqual(null, enemy.MissileEnemy, "le missile doit être null");
                        //Act
                        enemy = null;
                        player.MissilePlayer.MissileY = 0;
                        player.Score += 100;
                        break;
                    }
                }
                player.MissilePlayer.MissileY--;
            }


            //Assert
            Assert.AreEqual(null, enemy, "l'ennemie doit être null");
        }

        [TestMethod]
        public void Collision_entre_1_missile_et_1_joueur()
        {
            // Arrange
            List<int> posXBunker = new List<int>();
            Enemy[] enemies = new Enemy[20];
            PlayerShip player = new PlayerShip(1, 20, false, posXBunker, enemies);
            Enemy enemy = new Enemy(1, 10, false, true, posXBunker, player, false);
            player.ShipLife = 3;

            // Act
            while (enemy.MissileEnemy.MissileY < 28)
            {
                // Check if the missile touch a pixel of the player
                if (enemy.MissileEnemy.MissileY == player.ShipY - 1)
                {
                    if (enemy.MissileEnemy.MissileX >= player.ShipX && enemy.MissileEnemy.MissileX <= player.ShipX + player.ShipForm.Length - 1)
                    {
                        enemy.MissileEnemy.MissileY = 28;
                        player.ShipLife -= 1;
                    }
                }
                enemy.MissileEnemy.MissileY++;
            }


            //Assert
            Assert.AreEqual(2, player.ShipLife, "La vie du joueur doit être 2");
        }
    }
}
