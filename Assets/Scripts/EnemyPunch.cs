using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    public Animator enemyParent;
    public PlayerManager player;

    private AudioSource sound;
    public AudioClip attack;

    private void Start()
    {
        sound = GetComponent<AudioSource>();    
    }

    public bool oneAttack=true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyParent.GetBool("attack")==true && oneAttack)
            {
                sound.PlayOneShot(attack);
                oneAttack = false;
                player.TakingDamage();
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oneAttack = true;
        }
    }
}
