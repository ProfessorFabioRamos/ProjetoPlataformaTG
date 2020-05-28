using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 2;
    public float direction = -1;// 1 ou -1
    Rigidbody2D rig;
    SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = new Vector2(movementSpeed*direction, rig.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Limiter"){
            direction *= -1;
            if(direction == 1){
                spr.flipX = true;
            }else{
                spr.flipX = false;
            }
        }
    }
}
