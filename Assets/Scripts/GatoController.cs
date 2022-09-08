using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoController : MonoBehaviour
{
    
    public float JumpForce = 20;
    public float velocity = 10;
    public int Saltos;
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
   
    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_SLIDE = 3;
    const int ANIMATION_DEAD = 4;
    const int ANIMATION_HURT = 8;
    const int ANIMATION_FALL = 9;
    

    bool puedeSaltar = true;

    private int saltosHechos;
    public int limiteSaltos = 2;

    private Vector3 lastCheckpointPosition;
    
    void Start()
    {
        Debug.Log("Iniciamos script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
     Debug.Log("Puede saltar"+puedeSaltar.ToString());
         puedeSaltar = true;

        //CORRER
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X)){
            rb.velocity = new Vector2(-20, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }

        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X)){
            rb.velocity = new Vector2(20, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
       
        //SLIDE
        else if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-20, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_SLIDE);
        }

        else if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(20, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_SLIDE);
        }


        //SALTAR
        else if(Input.GetKeyUp(KeyCode.Space)){
            if(saltosHechos<limiteSaltos){

                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);            
                ChangeAnimation(ANIMATION_SALTAR);
                saltosHechos++;
                
            }
            
        }

        //DEAD
       else if(Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_DEAD);
        }
        //HURT
        else if(Input.GetKeyUp(KeyCode.B))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_HURT);
        }

        //FALL
        else if(Input.GetKeyUp(KeyCode.C))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_FALL);
        }

        //QUIETO
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }

    
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Puede saltar");
        puedeSaltar = true;
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Estas muerto");
            } 
            if (other.gameObject.name == "DarkHole")
            {
                if (lastCheckpointPosition != null)
                {
                    transform.position = lastCheckpointPosition;
                }
            }

            if(other.collider.tag=="Tilemap"){
            saltosHechos = 0;  
        }

            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;
    }
   
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }
 
}