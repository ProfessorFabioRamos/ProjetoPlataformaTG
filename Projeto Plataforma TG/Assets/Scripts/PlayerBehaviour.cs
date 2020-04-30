using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float movementSpeed = 5;
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

    }

}
