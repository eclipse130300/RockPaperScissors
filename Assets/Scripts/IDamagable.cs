using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamagable : MonoBehaviour , ITakeDamage, IDestroyable
{
    [SerializeField] int maxHealth;
    public bool isPlayer;
    public bool isInvulnerable;
    [SerializeField] float deathDelay;
    private int currentHealth;

    UIGameController gameController;

    Action OnDeath = delegate { };

    private void Awake()
    {
        gameController = FindObjectOfType<UIGameController>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return;

        Debug.Log("I TAKE DAMAGE");
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Destroy(deathDelay);
        }
    }

    public void Destroy(float delay)
    {
        if (isPlayer)
        {
            gameController.ShowRestartButton();
        }

        //playaudio
        Destroy(gameObject, delay);


    }

    private void Update()
    {
        if(transform.position.y < -30)
        {
            TakeDamage(1);
        }
    }
}
