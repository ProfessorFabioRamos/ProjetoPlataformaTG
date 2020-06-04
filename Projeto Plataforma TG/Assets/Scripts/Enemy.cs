using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 2;
    public float direction = -1;// 1 ou -1
    Rigidbody2D rig;
    SpriteRenderer spr;
    Animator anim;
    public GameObject bullet;
    private Transform playerPos;
    public float attackDistance = 3;
    public Transform shootingPosLeft;
    public Transform shootingPosRight;
    private bool canShoot = true;
    public float bulletSpeed = 50;
    public float coolDownTime = 1;
    private float cooldowntimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = new Vector2(movementSpeed*direction, rig.velocity.y);
    }

    void Update(){
        float distance = Vector2.Distance(transform.position, playerPos.position);

        if(distance < attackDistance && transform.position.x > playerPos.position.x){
            direction = -1;
        }
        else if(distance < attackDistance && transform.position.x < playerPos.position.x){
            direction = 1;
        }

        if(distance < attackDistance && canShoot){
            anim.SetTrigger("attack");
            GameObject bul= Instantiate(bullet, shootingPosRight.position, Quaternion.identity);
            //if(direction == 1) bul.transform.position = new Vector2(shootingPosRight.position.x, bul.transform.position.y);
            //else if(direction == -1) bul.transform.position = new Vector2(shootingPosLeft.position.x, bul.transform.position.y);

            bul.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed*direction, 0);
            canShoot = false;
            cooldowntimer = coolDownTime;
        }

        if(!canShoot){
            cooldowntimer -= Time.deltaTime;
            if(cooldowntimer <=0){
                canShoot = true;
            }
        }

        if(direction == 1){
            spr.flipX = true;            
        }else{
            spr.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Limiter"){
            direction *= -1;
        }
    }
}
