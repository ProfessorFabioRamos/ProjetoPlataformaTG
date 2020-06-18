using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public int HP = 10;
    public Slider hpBar;
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
    private bool attacking;
    public Transform attackBox;
    private float attackBoxPositionRight;
    private float attackBoxPositionLeft;
    public Transform respawnPosition;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        hpBar.maxValue = HP;
        attackBoxPositionRight = 0.446f;
        attackBoxPositionLeft = -0.446f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isDead){
            float h = SimpleInput.GetAxis("Horizontal");

            rig.velocity = new Vector2(movementSpeed*h, rig.velocity.y);
            anim.SetFloat("speed", Mathf.Abs(h));

            if(h >0){
                spr.flipX = false;
                attackBox.localPosition = new Vector2(attackBoxPositionRight, 0.493f);
            }
            else if (h< 0){
                spr.flipX = true;
                attackBox.localPosition = new Vector2(attackBoxPositionLeft, 0.493f);
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
    }

    void Update(){
        if(!isDead){
            if(Input.GetKeyDown(KeyCode.E)){
                anim.SetTrigger("attack");
                attacking = true;
            }
            hpBar.value = HP;
        }
    }

    public void AttackAnimation(){
        anim.SetTrigger("attack");
        attacking = true;
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Enemy" && attacking){
            other.GetComponent<Enemy>().TakeDamage(1);
            attacking = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("COLISAO");
        if(other.transform.name == "EnemyBullet(Clone)" && !isDead){
           TakeDamage(1);
           Destroy(other.gameObject);
        }

        if(other.transform.tag == "HealingItem"){
            TakeDamage(-5);
            Destroy(other.gameObject);
        }

        if(other.transform.tag == "EndLevel"){
            Invoke("EndLevel", 3);
        }
    }

    void TakeDamage(int damage){
        HP -= damage;

        if(HP > 10){
            HP = 10;
        }

        if(HP <=0){
            isDead = true;
            anim.SetTrigger("dead");
            Invoke("Respawn", 3);
        }
    }

    void Respawn(){
        isDead = false;
        anim.SetTrigger("respawn");
        transform.position = respawnPosition.position;
        HP = 10;
    }

    void EndLevel(){
        SceneManager.LoadScene("Cena_2");
    }
}
