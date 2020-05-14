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
        float h = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movementSpeed*h, rig.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(h));

        if(h >0){
            spr.flipX = false;
        }
        else if (h< 0){
            spr.flipX = true;
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, circleGroundArea, whatIsGround);

        if(Input.GetButtonDown("Jump") && grounded){
            rig.AddForce(Vector2.up * jumpForce);
        }

        anim.SetBool("jump", !grounded);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log("Attack");
            anim.SetTrigger("attack");
        }
    }
}
