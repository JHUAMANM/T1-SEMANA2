using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaGokuController : MonoBehaviour
{
    public float velocidad = 15;
    public int impacto = 1;

    Rigidbody2D rb;
    Animator animator;
    float realVelocidad;

     public void SetRightDirection(){
        realVelocidad = velocidad;
    }
    
    public void SetLeftDirection(){
        realVelocidad = -velocidad;
    }

    public void Impacto(int imp){
        impacto = imp;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,5);      
    }

    // Update is called once per frame
    void Update()
    {
      rb.velocity = new Vector2(realVelocidad,0);  
    }

     void OnCollisionEnter2D(Collision2D other){
        Destroy(this.gameObject);
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyGokuController>().Ataque(impacto);
            //other.gameObject.Attacking(danio);
            
        }
    }
}
