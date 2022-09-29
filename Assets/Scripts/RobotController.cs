using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
   
    private float moveSpeed, dirX, dirY;
    public float velocity = 10;
    public float salto = 30;
    public int saltosHechos;
    public int limiteSaltos = 2;

    public GameObject bullet;
     //Para el sonido ademas el audiosource
    public AudioClip jumpClip;   
    public AudioClip bulletClip;
    public AudioClip collisionClip;
    public AudioClip recogerMonedas;
    AudioSource audioSource;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    const int ANIMACION_QUIETO = 0;
    const int ANIMACION_CORRER = 1;
    const int ANIMACION_SALTAR = 2;
    const int ANIMACION_DESLIZAR = 3;
    const int ANIMACION_MORIR = 4;
    const int ANIMACION_ATTACK = 12;
    const int ANIMACION_GLIDE = 13;
    const int ANIMACION_SUBIR = 14;

    private int bala = 0;
    bool escalera = false;
    bool plataforma = false;
    private Vector3 lastCheckpointPosition; 
    private GameManagerController gameManager;
     private int bandera = 0;
     public bool ClimbingAllowed { get; set; }
    
    void Start()
    {
        Debug.Log("Iniciando partida de juego");
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        saltosHechos = 0;
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * velocity;

        if (ClimbingAllowed)
        {
            dirY = Input.GetAxisRaw("Vertical") * velocity;
        }


            //Para balear
        if(Input.GetKeyUp(KeyCode.D) && sr.flipX == true && bala < 5){
            
            var bulletPosition = transform.position + new Vector3(-3, 0, 0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject; 
            var controller = gb.GetComponent<BulletController>();
            controller.SetLeftDirection();  
            audioSource.PlayOneShot(bulletClip);        

            gameManager.PerderBalas();
            bala++;
            
        }
        if(Input.GetKeyUp(KeyCode.D) && sr.flipX == false && bala < 5){
            var bulletPosition = transform.position + new Vector3(3, 0, 0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject; 
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection(); 
            audioSource.PlayOneShot(bulletClip);
            gameManager.PerderBalas(); 
            bala++;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            CambiarAnimacion(ANIMACION_CORRER);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            CambiarAnimacion(ANIMACION_CORRER);
        }

        //Para Saltar -- JUMP
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (saltosHechos < limiteSaltos)
            {
                rb.AddForce(new Vector2(0, salto), ForceMode2D.Impulse);
                saltosHechos++;

                audioSource.PlayOneShot(jumpClip);
            }
        }
        
        //Para subir -- CLIMB
        else if(Input.GetKey(KeyCode.UpArrow)){
            rb.velocity = new Vector2(rb.velocity.x, 10);
            CambiarAnimacion(ANIMACION_SUBIR);
            plataforma = true;
        }
        
        else if(Input.GetKey(KeyCode.DownArrow)){
            rb.velocity = new Vector2(rb.velocity.x, -10);
            CambiarAnimacion(ANIMACION_SUBIR);
            plataforma = true;
        }

        //Para deslizarce ..SLIDE
        else if(Input.GetKey(KeyCode.Z)){
            CambiarAnimacion(ANIMACION_DESLIZAR);
        }

        else if(Input.GetKey(KeyCode.Q)){
            rb.velocity = new Vector2(2, rb.velocity.y);
            CambiarAnimacion(ANIMACION_GLIDE);
        }
        //Para morir -- DEAD         
        else if(gameManager.livesText.text == "Fin de Juego"){
            rb.velocity = new Vector2(0, rb.velocity.y);
            CambiarAnimacion(ANIMACION_MORIR);
        }        
        
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            CambiarAnimacion(ANIMACION_QUIETO);
        }
    }


    void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }

    void OnCollisionEnter2D(Collision2D other)
    {   
        if (other.collider.tag == "Suelo")
        {
            saltosHechos = 0;
        }
         if(other.collider.tag=="Tilemap"){
          saltosHechos = 0;  
        }
        if(other.gameObject.name == "DarkHole"){
            if(lastCheckpointPosition != null){
                transform.position = lastCheckpointPosition;
                
            }
        }
        if(other.gameObject.tag == "Enemy"){
            //rb.velocity = new Vector2(-1, rb.velocity.y);
            gameManager.PerderVida();
        }     
        
        if(other.gameObject.name == "Escalera"){
            escalera = true;
            escalera = true;
        }
        if(other.gameObject.name == "Plataforma"){
            plataforma = true;
        }

        //Para hacer grande el player
        if (other.collider.name == "Crecer")
        {
            transform.localScale = new Vector3(2,2,1);
            audioSource.PlayOneShot(jumpClip);
        }
        //Para regresar a su estado normal
        if(other.gameObject.tag == "Enemy")
        {
            transform.localScale = new Vector3(1,1,1);
            audioSource.PlayOneShot(collisionClip);
        }
        //Para morir
        if(other.gameObject.tag == "Enemy"){
            gameManager.PerderVida();
        }
        if(other.gameObject.tag == "Moneda"){
            Destroy(other.gameObject);
            gameManager.GanarMonedas(10);                      
            audioSource.PlayOneShot(recogerMonedas);
        } 
        if(other.gameObject.tag == "Moneda1"){
            Destroy(other.gameObject);            
            gameManager.GanarMonedas1(20);                     
            audioSource.PlayOneShot(recogerMonedas);
        }            
        if(other.gameObject.tag == "Moneda2"){
            Destroy(other.gameObject);            
            gameManager.GanarMonedas2(30);            
            audioSource.PlayOneShot(recogerMonedas);
        }            
                   
        
    }
    /*
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger");
        lastCheckpointPosition = transform.position;
        gameManager.SaveGame();

        
    }*/
    
     void OnTriggerEnter2D(Collider2D other) {
       Debug.Log("Trigger"); 
       if(other.gameObject.name == "Checkpoint1"){       
        lastCheckpointPosition = transform.position;
        gameManager.SaveGame();
        bandera++;
       }
       if(bandera <= 0){
        lastCheckpointPosition = transform.position;
        gameManager.SaveGame();
       }
       
    }  
    private void FixedUpdate()
    {
        if (ClimbingAllowed)
        {
            rb.isKinematic = true;
            rb.velocity = new Vector2(dirX, dirY);
        }
        else
        {
            rb.isKinematic = false;
            rb.velocity = new Vector2(dirX, rb.velocity.y);
        }
    }  

}
