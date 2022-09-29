using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaNHSem7 : MonoBehaviour
{
    public float velocidadBala = 20;
    public int impacto = 1;

    Rigidbody2D rb;
    Animator animator;

    float realVelocity;
    private GameManagerController gameManager;

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
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,5);    
    }

    void Update()
    {
        rb.velocity = new Vector2(realVelocity,0);
    }
  

    public void OnCollisionEnter2D(Collision2D other){
        Destroy(this.gameObject);
        if(other.gameObject.tag == "Enemy"){
             Destroy(other.gameObject);
             gameManager.PerderBalas();
             gameManager.SaveGame();
            //other.gameObject.GetComponent<EnemyControllerMegaman>().Attacking(impacto);
            //other.gameObject.Attacking(danio);
            
        }
    }
}
