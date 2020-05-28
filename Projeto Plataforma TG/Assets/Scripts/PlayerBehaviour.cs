using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float movementSpeed = 5;
    public float jumpForce = 500;

    public bool grounded;
    public float circleGroundArea = 0.5f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isJumping;
    public float jumpTime = 0.5f;
    private float jumpTimeCounter;

    Rigidbody2D rig;
    Animator anim;
    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = SimpleInput.GetAxis("Horizontal");

        rig.velocity = new Vector2(movementSpeed*h, rig.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(h));

        if(h >0){
            spr.flipX = false;
        }
        else if (h< 0){
            spr.flipX = true;
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, circleGroundArea, whatIsGround);

        //Input.GetButtonDown
        if(SimpleInput.GetAxis("Vertical") > 0 && grounded){
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rig.velocity = Vector2.up * jumpForce;
        }

        //Input.GetButton
        if(SimpleInput.GetAxis("Vertical") > 0 && isJumping){
            if(jumpTimeCounter > 0){
                rig.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else{
                isJumping = false;
            }
        }

        //Input.GetButtonUp
        if(SimpleInput.GetAxis("Vertical") == 0){
            isJumping = false;
        }

        anim.SetBool("jump", !grounded);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log("Attack");
            anim.SetTrigger("attack");
        }
    }

    public void AttackAnimation(){
        anim.SetTrigger("attack");
    }
}
