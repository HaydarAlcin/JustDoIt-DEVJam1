using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController character;
    //public Rigidbody rb;

    public bool isAttack;

    public Animator anim;
    public Animation leftRight;

    public float speed;



    void Start()
    {
        character = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        Move();
    }



    public void Move()
    {
        if (!isAttack)
        {
            Vector3 moveInputs = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;

            Vector3 moveVelocity = moveInputs * Time.deltaTime * speed;
            character.Move(moveVelocity);

            PlayerAnimation();
        }
    }

    public void PlayerAnimation()
    {
        if (Input.GetAxis("Vertical") == 0)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isMove", false);
            anim.SetBool("back", false);

            //gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            //gameObject.transform.GetChild(0).transform.localPosition = new Vector3(0, -1.12f, 0);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                speed = 3.5f;
                anim.SetFloat("AnimationSpeed", 1f);
                anim.SetBool("back", true);
                anim.SetBool("isMove", false);
                anim.SetBool("isIdle", false);


            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                speed = 5f;
                anim.SetBool("back", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isMove", true);
            }

        }
        

    }
}
