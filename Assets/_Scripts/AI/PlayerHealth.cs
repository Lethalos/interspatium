using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public static event Action OnPlayerDeath;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}