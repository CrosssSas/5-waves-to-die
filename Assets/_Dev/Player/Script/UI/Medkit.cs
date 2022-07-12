using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] public CharacterStats player;
    [SerializeField] public int amount = 50;

    private void Start()
    {
        player.Heal(amount);
    }

    public void HealPlayer()
    {
        player.Heal(amount);
    } 
}
