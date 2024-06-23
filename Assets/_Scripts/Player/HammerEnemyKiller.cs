using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerEnemyKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<EnemyBehavior>(out EnemyBehavior enemyBehavior))
            {
                enemyBehavior.KillEnemy();
            }
            else if (other.TryGetComponent<SerpmareBehaviour>(out SerpmareBehaviour serpmareBehaviour))
            {
                serpmareBehaviour.KillEnemy();
            }
        }
    }
}
