using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMegaman : MonoBehaviour
{
    public float velocidadBala = 20;
    public int impacto = 1;

    Rigidbody2D rb;
    Animator animator;

    float realVelocity;

    public void SetRightDirection(){
        realVelocity = velocidadBala;
    }
    
    public void SetLeftDirection(){
        realVelocity = -velocidadBala;
    }

    public void Impacto(int imp){
        impacto = imp;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,5);    
    }

    void Update()
    {
        rb.velocity = new Vector2(realVelocity,0);
    }

    

    void OnCollisionEnter2D(Collision2D other){
        //Destroy(this.gameObject);
        if(other.gameObject.tag == "Enemy"){
             Destroy(this.gameObject);
            other.gameObject.GetComponent<EnemyControllerMegaman>().Attacking(impacto);
            //other.gameObject.Attacking(danio);
            
        }
    }
}
