using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 10;
    public float salto = 80;
    public int saltosHechos;
    public int limiteSaltos = 2;
    bool puedeSaltar = true;
    private Vector3 lastCheckpointPosition; 

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    const int animacion_estatico = 0;
    const int animacion_correr = 1;
    const int animacion_caminar = 2;
    const int animacion_atacar = 3;
    const int animacion_saltar = 4;
    
    void Start()
    {
        Debug.Log("Iniciando partida de juego");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        saltosHechos = 0;
    }


    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
            CambiarAnimacion(animacion_estatico);
            
        if (Input.GetKey(KeyCode.RightArrow))
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
        if (Input.GetKey(KeyCode.LeftArrow))
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
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {   if(saltosHechos<limiteSaltos){
            rb.AddForce(new Vector2(0, salto), ForceMode2D.Impulse);
            //rb.velocity = new Vector2(rb.velocity.x, +salto);
            saltosHechos++;
            }
            
        }
        if(Input.GetKey(KeyCode.Z)){
           CambiarAnimacion(animacion_atacar);
        }
         
       
    }
    void OnCollisionEnter2D(Collision2D objeto){
        if(objeto.collider.name=="Tilemap"){
          saltosHechos = 0;  
        }
        if(objeto.gameObject.name == "DarkHole"){
            if(lastCheckpointPosition != null){
                transform.position = lastCheckpointPosition;
            }
        }     
    }

    void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }
      
    void OnTriggerEnter2D(Collider2D other) {
       Debug.Log("Trigger"); 
       lastCheckpointPosition = transform.position;
    } 

}

