using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GokuController : MonoBehaviour
{
   public float velocity = 10;
   public GameObject bala1;
   public GameObject bala2;

   Rigidbody2D rb;
   SpriteRenderer sr;
   Animator animator;

   private Vector2 direction;

   private bool tieneNube = false;

   private float defaulGravity;
   private GameManagerController gameManager;   
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        defaulGravity = rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        

        //Para balear
        if(Input.GetKeyUp(KeyCode.D) && sr.flipX == true){
            
            var bulletPosition = transform.position + new Vector3(-3, 0, 0);
            var gb = Instantiate(bala1, bulletPosition, Quaternion.identity) as GameObject; 
            var controller = gb.GetComponent<BulletController>();
            controller.SetLeftDirection();  
            //audioSource.PlayOneShot(bulletClip);        

            gameManager.PerderBalas();
            
            
        }
        if(Input.GetKeyUp(KeyCode.D) && sr.flipX == false){
            var bulletPosition = transform.position + new Vector3(3, 0, 0);
            var gb = Instantiate(bala1, bulletPosition, Quaternion.identity) as GameObject; 
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection(); 
            //audioSource.PlayOneShot(bulletClip);
            gameManager.PerderBalas(); 
           
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        direction = new Vector2(x, y);
        Run();

        if(Input.GetKey(KeyCode.UpArrow)&& tieneNube){
            rb.velocity = new Vector2(rb.velocity.x, velocity);
        }
         if(Input.GetKey(KeyCode.DownArrow)&& tieneNube){
            rb.velocity = new Vector2(rb.velocity.x, -velocity);
        }
    }

    private void Run(){
        //if(direction.x == 0f)
        //    return;
        rb.velocity = new Vector2(direction.x * velocity, 0);
        sr.flipX = direction.x < 0;

    }

    void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.name == "Nube"){ 
            //Destroy(other.gameObject);                 
            rb.gravityScale = 0;
            tieneNube = true;
            animator.SetInteger("Estado", 1);            
        }
        if(other.gameObject.name == "Checkpoint"){

            SceneManager.LoadScene(GameManagerController.evaluacion);
        } 
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Tilemap"){            
            rb.gravityScale = defaulGravity;
            tieneNube = false;
            animator.SetInteger("Estado", 0);
        }
        
    }

    /*void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Nube"){
            Destroy(other.gameObject);
            //rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0;
            tieneNube = true;
        }
        
    }*/
}
