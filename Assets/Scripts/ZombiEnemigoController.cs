using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiEnemigoController : MonoBehaviour
{
    public float velocity = 10;
    
     //Para el sonido ademas el audiosource  

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    const int ANIMACION_MORIR = 4;    
    const int ANIMACION_CAMINAR = 7;
    const int ANIMACION_ATTACK = 12;
    
    
    void Start()
    {
        Debug.Log("Iniciando partida de juego");
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
       
    }
    void Update()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        CambiarAnimacion(ANIMACION_CAMINAR);

    }

    void CambiarAnimacion(int animacion){
        animator.SetInteger("Estado", animacion);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "LimiteDerecho"){
            velocity *=-1;
            sr.flipX = true;
        }
        if(other.gameObject.tag == "LimiteIzquierdo"){
            velocity *=-1;
            sr.flipX = false;
        }
    }
}
