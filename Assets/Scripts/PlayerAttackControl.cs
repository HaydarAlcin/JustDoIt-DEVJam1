using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerAttackControl : MonoBehaviour
{
    public Animator animator; // Karakterin Animator bileþeni
    public PlayerController player;
    private AudioSource sound;

    float time=1.2f;

    private float lastClickTime = 0f;
    public float clickInterval = 0.7f;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        sound = GetComponent<AudioSource>();
    }


    private void Update()
    {
        Attack();
        
    }
    private void FixedUpdate()
    {
        AttackChange();
    }


    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (Time.time - lastClickTime > clickInterval)
            {
                player.isAttack = true;
                if (time < 1f)
                {
                    if (time < 0 || time == 1.2f)
                    {
                        time = 1.2f;
                        animator.SetInteger("attack", 1);
                        StartCoroutine(SoundEffect());
                    }

                    else if (animator.GetInteger("attack") == 2 && time > 0)
                    {
                        animator.SetInteger("attack", 3);
                        time = 1.2f;
                        StartCoroutine(SoundEffect());
                    }

                    else if (time > 0)
                    {
                        animator.SetInteger("attack", 2);
                        time = 1.2f;
                        StartCoroutine(SoundEffect());
                    }
                }
            }


        }
    }

    public void AttackChange()
    {
        
        time -= Time.deltaTime;
        if (time<0)
        {
            animator.SetInteger("attack", 0);
            player.isAttack = false;
        }

    }


    IEnumerator SoundEffect()
    {
        yield return new WaitForSeconds(0.5f);
        sound.Play();
    }
}
