using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;
    [SerializeField] public bool isDead;


    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        if (currentHealth >= maxHealth) { currentHealth = maxHealth; }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void SetHealthTo(int amount)
    {
        currentHealth = amount;
        CheckHealth();
    }

    public void TakeDamage(int amount) { SetHealthTo(currentHealth - amount); }

    public void Heal(int amount) { SetHealthTo(currentHealth + amount); }

    public virtual void InitVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
    }

    public virtual void Die() 
    { 
        isDead = true; 
        // DIE mechanic
    }
}
