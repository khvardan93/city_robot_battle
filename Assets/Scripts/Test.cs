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
        [Space] 
        public EnemySettings spiderSettings;
        public OrangeSpiderEnemy spiderEnemy;

        private void Start()
        {
            var poolModel = System.Instance.GetModel<PoolModel>();

            player.Init(playerSettings, poolModel);
            enemy.Init(enemySettings, poolModel);
            spiderEnemy.Init(spiderSettings, poolModel);
        }
    }
}
