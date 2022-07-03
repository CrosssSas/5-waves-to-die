using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagment : MonoBehaviour
{
    private int max_health = 100;
    private int current_health = 0;


    public AudioSource source;
    public AudioClip hit, heal;


    void Start()
    {
        current_health = 100;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            source.PlayOneShot(hit);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            source.PlayOneShot(heal);
        }
    }

    public int getHealth() { return current_health; }

    public void TakeDamage(int amount)
    {
        current_health -= amount;

        if (current_health <= 0)
        {
            current_health = 0;
            Die();
        }
    }

    public void Die()
    {
        // TODO: Die mechanic
    }

    public void TakeHeal(int amount)
    {
        current_health += amount;

        if (current_health >= max_health)
        {
            current_health = max_health;
        }
    }
}
