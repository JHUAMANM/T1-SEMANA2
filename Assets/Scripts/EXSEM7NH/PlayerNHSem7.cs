using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNHSem7 : MonoBehaviour
{
    public float defaultVelocity = 10;
    public float velocity = 0;
    public int jumpForce = 50;
    public bool puedeSaltar = false;

    public GameObject primeraBala;
    public GameObject segundaBala;
    public GameObject terceraBala;

    public AudioClip jumpClip;
    public AudioClip bulletClip;
    public AudioClip collisionClip;
    public AudioClip recogerMonedas;
    AudioSource audioSource;

    private float timeLeft = 0;

    private Vector3 lastCheckpointPosition;
    private GameManagerController gameManager;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    const int ANIMACION_QUIETO = 0;
    const int ANIMACION_CORRER = 1;
    const int ANIMACION_SALTAR = 2;
    const int ANIMACION_DESLIZAR = 3;
    const int ANIMACION_MORIR = 4;
    const int ANIMACION_ATTACK = 12;
    const int ANIMACION_GLIDE = 13;
    const int ANIMACION_SUBIR = 14;

    const int ANIMACION_POTENCIA = 4;
    Vector3 lastCheckPointPosition;

    private MenuArmas menuArmas;
    bool puedeDisparar = false;
    const int bandera = 0;
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        menuArmas = FindObjectOfType<MenuArmas>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        float X = gameManager.PositionX();
        float Y = gameManager.PositionY();
        float Z = gameManager.PositionZ();
        if (X != 0.0f || Y != 0.0f || Z != 0.0f)
        {
            transform.position = new Vector3(X, Y, Z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Moverse();

    }

    public void Moverse()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            WalkToLeft();
        //CambiarAnimacion(ANIMACION_CORRER);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            StopWalk();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            WalkToRight();
        //CambiarAnimacion(ANIMACION_CORRER);

        if (Input.GetKeyUp(KeyCode.RightArrow))
            StopWalk();



        Walk();
        BestJump();

        //Attack();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

        }
        //Para morir -- DEAD         
        if (gameManager.livesText.text == "Fin de Juego")
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            CambiarAnimacion(ANIMACION_MORIR);
        }
    }

    private void Walk()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        if (velocity < 0)
            sr.flipX = true;
        if (velocity > 0)
            sr.flipX = false;
    }
    private void BestJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.x;

        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.x;

        }
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
        CambiarAnimacion(ANIMACION_SALTAR);
        audioSource.PlayOneShot(jumpClip);

    }
    public void Deslizar()
    {
        CambiarAnimacion(ANIMACION_DESLIZAR);

    }
    public void Volver()
    {
        SceneManager.LoadScene(2);

    }
    public void WalkToLeft()
    {
        velocity = -defaultVelocity;
        CambiarAnimacion(ANIMACION_CORRER);
    }

    public void WalkToRight()
    {
        velocity = defaultVelocity;
        CambiarAnimacion(ANIMACION_CORRER);
    }

    public void StopWalk()
    {
        velocity = 0;
        CambiarAnimacion(ANIMACION_QUIETO);
    }

    public void cambiarArma(){
        puedeDisparar=true;
    }

    public void Attack()
    {


        if(puedeDisparar==false){
            var kunaiPosition = transform.position + new Vector3(3, 0, 0);
            var gb = Instantiate(primeraBala, kunaiPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
            controller.Impacto(2);
        }
         else{
            CambiarAnimacion(ANIMACION_ATTACK);
        }
        
    


    
        /*

        if (sr.flipX == false)
        {            
            var primeraBalaPosition = transform.position + new Vector3(2, 0, 0);
            var gb = Instantiate(primeraBala, primeraBalaPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
            controller.Impacto(2);
            audioSource.PlayOneShot(bulletClip);
        }*/
        /*
        if (sr.flipX == true)
        {
            var primeraBalaPosition = transform.position + new Vector3(-2, 0, 0);
            var gb = Instantiate(primeraBala, primeraBalaPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetLeftDirection();
            controller.Impacto(2);
            audioSource.PlayOneShot(bulletClip);
        }*/
        /*if(sr.flipX == false && bandera == 1){
            CambiarAnimacion(ANIMACION_ATTACK);

        }*/
        

        /*
                if (timeLeft > 1 && timeLeft <= 4)
                {
                    if (sr.flipX == false && Input.GetKeyDown(KeyCode.JoystickButton0))
                    {
                        var primeraBalaPosition = transform.position + new Vector3(2, 0, 0);
                        var gb = Instantiate(segundaBala, primeraBalaPosition, Quaternion.identity) as GameObject;
                        gb.transform.localScale = new Vector3(5, 5, 5);
                        var controller = gb.GetComponent<BulletController>();
                        controller.SetRightDirection();
                        audioSource.PlayOneShot(bulletClip);
                    }
                    if (sr.flipX == true)
                    {
                        var primeraBalaPosition = transform.position + new Vector3(-2, 0, 0);
                        var gb = Instantiate(segundaBala, primeraBalaPosition, Quaternion.identity) as GameObject;
                        gb.transform.localScale = new Vector3(5, 5, 5);
                        var controller = gb.GetComponent<BulletController>();
                        controller.SetLeftDirection();
                        audioSource.PlayOneShot(bulletClip);
                    }
                }
                if (timeLeft > 5)
                {
                    if (sr.flipX == false)
                    {
                        var primeraBalaPosition = transform.position + new Vector3(2, 0, 0);
                        var gb = Instantiate(terceraBala, primeraBalaPosition, Quaternion.identity) as GameObject;
                        gb.transform.localScale = new Vector3(8, 8, 8);
                        var controller = gb.GetComponent<BulletController>();
                        controller.SetRightDirection();
                        audioSource.PlayOneShot(bulletClip);
                    }
                    if (sr.flipX == true)
                    {
                        var primeraBalaPosition = transform.position + new Vector3(-2, 0, 0);
                        var gb = Instantiate(terceraBala, primeraBalaPosition, Quaternion.identity) as GameObject;
                        gb.transform.localScale = new Vector3(8, 8, 8);
                        var controller = gb.GetComponent<BulletController>();
                        controller.SetLeftDirection();
                        audioSource.PlayOneShot(bulletClip);
                    }
                }*/

    }

    private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "DarkHole")
        {
            if (lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
        }
        //Para morir
        if (other.gameObject.tag == "Enemy")
        {
            gameManager.PerderVida();
        }

        if (other.gameObject.tag == "Moneda")
        {
            Destroy(other.gameObject);
            gameManager.GanarMonedas(10);
            audioSource.PlayOneShot(recogerMonedas);
        }
        if (other.gameObject.tag == "Moneda1")
        {
            Destroy(other.gameObject);
            gameManager.GanarMonedas1(20);
            audioSource.PlayOneShot(recogerMonedas);
        }
        if (other.gameObject.tag == "Moneda2")
        {
            Destroy(other.gameObject);
            gameManager.GanarMonedas2(30);
            audioSource.PlayOneShot(recogerMonedas);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        lastCheckPointPosition = transform.position;
        if (other.gameObject.tag == "Guardar")
        {
            gameManager.SetLastCheckPosition(lastCheckPointPosition.x,
                                            lastCheckPointPosition.y,
                                            lastCheckPointPosition.z);
            gameManager.SaveGame();
        }
        Debug.Log("trigger");
        if (other.gameObject.tag == "Checkpoint")
        {
            lastCheckpointPosition = transform.position;
        }
    }
}
