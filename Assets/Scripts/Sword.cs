using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public PlayerController player;
    private EnemyController enemyController;
    public GameObject enemy;

    bool justOne=true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            Animator otherAnim = other.transform.GetChild(0).GetComponent<Animator>();
            enemyController = other.gameObject.GetComponent<EnemyController>();
            if (player.isAttack==true && justOne==true )
            {
                otherAnim.SetBool("attack", false);
                otherAnim.SetTrigger("death");
                enemyController.sound.PlayOneShot(enemyController.deadSound);
                Invoke("EnemyDead", 1.2f);
                justOne = false;
            }
            
        }
    }

    private void EnemyDead()
    {
        justOne = true;
        enemyController.Death();
    }
}
