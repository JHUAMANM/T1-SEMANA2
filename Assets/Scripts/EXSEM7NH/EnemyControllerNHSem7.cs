using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerNHSem7 : MonoBehaviour
{
    public float velocity = 10;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    const int ANIMACION_MORIR = 4;    
    const int ANIMACION_CAMINAR = 7;
    const int ANIMACION_ATTACK = 12;
    public float vidasEnemigo = 2; 
    
    
    void Start()
    {
        Debug.Log("Iniciando partida de juego");
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(PonerZombi());
       
    }
    void Update()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        CambiarAnimacion(ANIMACION_CAMINAR);

         if(vidasEnemigo<=0){
            Destroy(this.gameObject);
        }

    }

    void CambiarAnimacion(int animacion){
        animator.SetInteger("Estado", animacion);
    }


     public void Attacking(int ataque){
        vidasEnemigo -= ataque;
        Debug.Log("Vida Enemigo: " + vidasEnemigo);
    }

    public GameObject zombi;

    IEnumerator PonerZombi()
    {
    int segundos = Random.Range(2,6);

    yield return new WaitForSeconds(segundos);

    Instantiate (zombi,  transform.position, Quaternion.identity);

    StartCoroutine(PonerZombi());

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
