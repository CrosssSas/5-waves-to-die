using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerStats : CharacterStats
{
    [SerializeField] private PlayerHUD hud;
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip hit1, hit2, hit3, hit4, hit5, hit6, hit7, hit8, hit9, heal;
    private System.Random rnd = new System.Random();

    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject winCanvas;
    private bool end, mainMenu, pause, win;

    public override void Die()
    {
        if(!isDead)
        {
            base.Die();
            SetDieCanvas(true);
        }
    }

    private void Start()
    {
        // hud = GetComponent<PlayerHUD>();
        
        InitVariables();
        SetDieCanvas(false);
        SetMainMenuCanvas(false);
        SetPauseCanvas(false);
        SetWinCanvas(false);
        SetHudCanvas(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!(end | mainMenu | win))
            {
                if(!pause)
                {
                    SetPauseCanvas(true);
                }
                else
                {
                    SetPauseCanvas(false);
                }
            }
        }
    }

    public override void CheckHealth()
    {
        base.CheckHealth();

        hud.UpdateHealth(currentHealth, maxHealth);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        switch(rnd.Next(1, 10))
        {
            case 1:
                source.PlayOneShot(hit1);
                break;
            case 2:
                source.PlayOneShot(hit2);
                break;
            case 3:
                source.PlayOneShot(hit3);
                break;
            case 4:
                source.PlayOneShot(hit4);
                break;
            case 5:
                source.PlayOneShot(hit5);
                break;
            case 6:
                source.PlayOneShot(hit6);
                break;
            case 7:
                source.PlayOneShot(hit7);
                break;
            case 8:
                source.PlayOneShot(hit8);
                break;
            case 9:
                source.PlayOneShot(hit9);
                break;
        }
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);

        source.PlayOneShot(heal);
    }


    public void SetHudCanvas(bool state)
    {
        hudCanvas.SetActive(state);
    }

    public void SetDieCanvas(bool state)
    {
        endCanvas.SetActive(state);
        SetHudCanvas(!state);
        end = state;
    }

    public void SetMainMenuCanvas(bool state)
    {
        mainMenuCanvas.SetActive(state);
        SetHudCanvas(!state);
        mainMenu = state;
    }

    public void SetPauseCanvas(bool state)
    {
        pauseCanvas.SetActive(state);
        pause = state;
        SetHudCanvas(!state);
    }

    public void SetWinCanvas(bool state)
    {
        winCanvas.SetActive(state);
    }

    public void Restart()
    {
        isDead = false;
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        SetDieCanvas(false);
        SetPauseCanvas(false);
        SetHudCanvas(false);
        SetWinCanvas(false);
        SetMainMenuCanvas(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("it's working");
    }
}
