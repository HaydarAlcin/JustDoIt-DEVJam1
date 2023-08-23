using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    public float speed;
    public Animator enemyAnim;

    public bool move;

    public GameObject soul;

    Rigidbody rb;

    public AudioSource sound;
    public AudioClip deadSound;

    private void Start()
    {
        move = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveThePlayer();
        BackTheMove();


    }

    public void MoveThePlayer()
    {
        if (move==true)
        {
            transform.LookAt(player.position);
            Vector3 currentPos = transform.position;
            currentPos = transform.forward * speed * Time.deltaTime;
            transform.position += currentPos;

            float xDis = transform.position.x - player.position.x;
            float zDis= transform.position.z - player.position.z;

            if (Mathf.Abs(xDis)<=1&& Mathf.Abs(zDis) <= 1)
            {
                move = false;
                Attack();
            }
        }
    }


    

    


    public void Attack()
    {
        if (enemyAnim.GetBool("attack")==false)
        {
            move = false;
            enemyAnim.SetBool("attack", true);

            //BackTheMove();
        }
        
    }

    public void BackTheMove()
    {
        float xDis = transform.position.x - player.position.x;
        float zDis = transform.position.z - player.position.z;
        if (Mathf.Abs(xDis) >= .8 && Mathf.Abs(zDis) >= .8)
        {   
           enemyAnim.SetBool("attack", false);
            move = true;
        }

    }

    public void Death()
    {
        
        bool justOneMore = true;

        if (justOneMore==true)
        {
            Instantiate(soul, transform.position, Quaternion.identity);
            justOneMore = false;
        }
        gameObject.SetActive(false);

    }
}

