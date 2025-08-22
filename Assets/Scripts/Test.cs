using System;
using RobotBattle.Enemy;
using RobotBattle.Robot;
using UnityEngine;

namespace RobotBattle
{
    public class Test : MonoBehaviour
    {
        public PlayerSettings playerSettings;
        public PlayerController player;
        [Space] 
        public EnemySettings enemySettings;
        public SentryEnemy enemy;

        private void Start()
        {
            player.Init(playerSettings);
            enemy.Init(enemySettings);
        }
    }
}
