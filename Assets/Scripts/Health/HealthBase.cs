using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startLife = 10;
    public bool destroyOnDeath = false;
    public float delayToDeath = 0f;

    private int currentLife;

    private bool isDead = false;

    void Awake()
    {
        Init();
    }
    private void Init()
    {
        isDead = false;
        currentLife = startLife;
    }

    public void Damage(int damage)
    {
        if (isDead) return;

        currentLife -= damage;
        if (currentLife <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;

        if (destroyOnDeath)
        {
            Destroy(gameObject, delayToDeath);
        }
    }
}
