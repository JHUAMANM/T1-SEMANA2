using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGokuController : MonoBehaviour
{
     private float velocidad = 10;
     public float fuerzaSalto = 15;

    public GameObject primeraBala;
    public GameObject segundaBala;
    public GameObject terceraBala;
    
    private float timeLeft = 0;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    const int ANIMACION_IDLE = 0;
    const int ANIMACION_RUN = 1;
    const int ANIMACION_JUMP = 2;    
    const int ANIMACION_POTENCE = 3;
    const int ANIMACION_ATTACK = 4;   

    bool puedeSaltar = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //QUIETO
        rb.velocity = new Vector2(0,rb.velocity.y);
        CambiarAnimacion(ANIMACION_IDLE);
        
        //CORRER DERECHA
        if(Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(velocidad,rb.velocity.y);
            CambiarAnimacion(ANIMACION_RUN);
            sr.flipX = false;
        }

        //CORRER IZQUIERDA
        if(Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-velocidad,rb.velocity.y);
            CambiarAnimacion(ANIMACION_RUN);
            sr.flipX = true;
        }

        //SALTAR
        if(Input.GetKeyDown(KeyCode.Space) && puedeSaltar){
            rb.AddForce(new Vector2(0,fuerzaSalto), ForceMode2D.Impulse);
            CambiarAnimacion(ANIMACION_JUMP);
            puedeSaltar = false;
        }

        //CARGAR ENERGÍA
        if(Input.GetKey(KeyCode.X)){
            CambiarAnimacion(ANIMACION_POTENCE);
            timeLeft += Time.deltaTime;
            Debug.Log("Tiempo: " + timeLeft);
        }

        if(timeLeft < 1){
            if(Input.GetKeyUp(KeyCode.X) && sr.flipX == false){
                CambiarAnimacion(ANIMACION_ATTACK);
                var primeraBalaPosition = transform.position + new Vector3(3,0,0);
                var gb = Instantiate(primeraBala, primeraBalaPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BalaGokuController>();
                controller.SetRightDirection();

            }
            if(Input.GetKeyUp(KeyCode.X) && sr.flipX == true){
                CambiarAnimacion(ANIMACION_ATTACK);
                var primeraBalaPosition = transform.position + new Vector3(-3,0,0);
                var gb = Instantiate(primeraBala, primeraBalaPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BalaGokuController>();
                controller.SetLeftDirection();
            }

        }
         else if(timeLeft > 2 && timeLeft < 4){
                if(sr.flipX == false && Input.GetKeyUp(KeyCode.X)){
                CambiarAnimacion(ANIMACION_ATTACK);
                var segundaBalaPosition = transform.position + new Vector3(3,0,0);
                var gb = Instantiate(segundaBala, segundaBalaPosition, Quaternion.identity) as GameObject;
                gb.transform.localScale = new Vector3(5,5,5);
                var controller = gb.GetComponent<BalaGokuController>();
                controller.Impacto(2);
                controller.SetRightDirection();
                }
                if(sr.flipX == true && Input.GetKeyUp(KeyCode.X)){
                CambiarAnimacion(ANIMACION_ATTACK);
                var primeraBalaPosition = transform.position + new Vector3(-3,0,0);
                var gb = Instantiate(segundaBala, primeraBalaPosition, Quaternion.identity) as GameObject;
                gb.transform.localScale = new Vector3(5,5,5);
                var controller = gb.GetComponent<BalaGokuController>();
                controller.Impacto(2);
                controller.SetLeftDirection();
                }
            }
            else if(timeLeft > 5){
                if(Input.GetKeyUp(KeyCode.X)){
                   if(sr.flipX == false && Input.GetKeyUp(KeyCode.X)){
                        CambiarAnimacion(ANIMACION_ATTACK);
                        var primeraBalaPosition = transform.position + new Vector3(3,0,0);
                        var gb = Instantiate(terceraBala, primeraBalaPosition, Quaternion.identity) as GameObject;
                        gb.transform.localScale = new Vector3(8,8,8);
                        var controller = gb.GetComponent<BalaGokuController>();
                        controller.Impacto(3);
                        controller.SetRightDirection();
                    }
                    if(sr.flipX == true && Input.GetKeyUp(KeyCode.X)){
                        CambiarAnimacion(ANIMACION_ATTACK);
                        var terceraBalaPosition = transform.position + new Vector3(-3,0,0);
                        var gb = Instantiate(terceraBala, terceraBalaPosition, Quaternion.identity) as GameObject;
                        gb.transform.localScale = new Vector3(8,8,8);
                        var controller = gb.GetComponent<BalaGokuController>();
                        controller.Impacto(3);
                        controller.SetLeftDirection();
                    } 
                }
            }
        if(Input.GetKeyUp(KeyCode.X)){
            timeLeft = 0;
        }
                
    }

    private void CambiarAnimacion(int animacion){
        animator.SetInteger("Estado",animacion);
    }

    void OnCollisionEnter2D(Collision2D other){
        
        puedeSaltar = true;
    }
}
