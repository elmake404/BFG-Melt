using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public PlayerBehaviour playerBehaviour;
    public List<EnemyBehaviour> enemyRobotBehaviours;

    private void Start()
    {
        SetPlayerLinksToEnemies();
    }

    private void SetPlayerLinksToEnemies()
    {
        for (int i = 0; i < enemyRobotBehaviours.Count; i++)
        {
            enemyRobotBehaviours[i].SetPlayerBehaviour(playerBehaviour);
        }
    }
}
