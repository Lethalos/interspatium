using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolHandler : MonoBehaviour
{
    [SerializeField] private GenericObjectPool<RangedEnemy> rangedEnemy;
    [SerializeField] private GenericObjectPool<MeleeEnemy> meleeEnemy;

    void Start()
    {
        // Initialize each pool with the appropriate prefab
        rangedEnemy.InitializePool();
        meleeEnemy.InitializePool();
    }

    public RangedEnemy GetRangedEnemy()
    {
        return rangedEnemy.GetPooledObject();
    }

    public MeleeEnemy GetMeleeEnemy()
    {
        return meleeEnemy.GetPooledObject();
    }

    public void ReturnToRangedEnemyPool(RangedEnemy enemy)
    {
        rangedEnemy.ReturnToPool(enemy);
    }

    public void ReturnToMeleeEnemyPool(MeleeEnemy enemy)
    {
        meleeEnemy.ReturnToPool(enemy);
    }
}

public enum EnemyType
{
    Ranged,
    Melee
}
