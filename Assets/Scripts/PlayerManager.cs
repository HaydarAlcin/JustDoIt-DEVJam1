using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health;
    public int power;

    public GameManager gm;

    public Slider healthBar,powerBar;

    private void Start()
    {
        power = 0;
        health = 100f;
     
        healthBar.maxValue = 100f;
        healthBar.minValue = 0f;
        healthBar.value = 100f;

        powerBar.maxValue = 15;
        powerBar.minValue = 0;
        powerBar.value = 0;
    }

    public void TakingDamage()
    {
        health -= 20f;
        healthBar.value = health;
        if (health<=0)
        {
            gm.GameOverPlayer();
            
        }

    }

    public void TakeThePower()
    {
        power += 1;
        powerBar.value = power;
        if (power>=15)
        {
            gm.WinGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Power"))
        {
            other.transform.parent.gameObject.SetActive(false);
            TakeThePower();
        }
    }


}
