using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    private PlayerHUD hud;

    private void Start()
    {
        hud = GetComponent<PlayerHUD>();
        InitVariables();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();

        hud.UpdateHealth(currentHealth, maxHealth);
    }
}
