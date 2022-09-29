using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    public float velocity = 10;
    //public float salto = 80;
    //public int saltosHechos;
    //public int limiteSaltos = 2;
    //bool puedeSaltar = true;
    public GameObject bullet;
    private Vector3 lastCheckpointPosition; 

    private GameManagerController gameManager;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    const int ANIMACION_QUIETO = 0;
    const int ANIMACION_DEAD = 4;
    const int ANIMACION_CAMINAR = 7;
    const int ANIMACION_ATACAR = 12;
    
    //private int bandera = 0;
    void Start()
    {
        Debug.Log("Iniciando partida de juego");
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //saltosHechos = 0;
    }


    void Update()
    {
        
        rb.velocity = new Vector2(-1, rb.velocity.y);
        sr.flipX = true;
        CambiarAnimacion(ANIMACION_CAMINAR);

       
        
        // rb.velocity = new Vector2(1, rb.velocity.y);
        // CambiarAnimacion(animacion_caminar);
        /*           
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            if(puedeSaltar==true){                
                CambiarAnimacion(animacion_correr);
            }
          
            if(Input.GetKey(KeyCode.X))
                rb.velocity = new Vector2(1, rb.velocity.y);
                sr.flipX = false;
                if(puedeSaltar==true){
                CambiarAnimacion(animacion_caminar);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            if(puedeSaltar==true){
                CambiarAnimacion(animacion_correr);
            }
             if(Input.GetKey(KeyCode.X))
                rb.velocity = new Vector2(-1, rb.velocity.y);
                sr.flipX = true;
                if(puedeSaltar==true){
               CambiarAnimacion(animacion_caminar);
            }                
            
        }*/
        
        if(Input.GetKey(KeyCode.Z)){
           CambiarAnimacion(ANIMACION_ATACAR);
        }

        if(Input.GetKey(KeyCode.W)){
           CambiarAnimacion(ANIMACION_DEAD);
        }
         
       
    }
    /*
    void OnCollisionEnter2D(Collision2D objeto){
        if(objeto.collider.name=="Tilemap"){
          saltosHechos = 0;  
        }
        if(objeto.gameObject.name == "DarkHole"){
            if(lastCheckpointPosition != null){
                transform.position = lastCheckpointPosition;
            }
        } 
        if(objeto.gameObject.tag == "Enemy"){
           CambiarAnimacion(animacion_dead);
            
        }    
    }*/

    void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }
     /* 
    void OnTriggerEnter2D(Collider2D other) {
       Debug.Log("Trigger"); 
       if(other.gameObject.name == "Checkpoint1"){
        bandera++;
        lastCheckpointPosition = transform.position;
       }
       if(bandera <= 0){
        lastCheckpointPosition = transform.position;
       }*/
    } 
//}
