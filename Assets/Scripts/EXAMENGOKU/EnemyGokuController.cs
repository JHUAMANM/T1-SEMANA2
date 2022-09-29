using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGokuController : MonoBehaviour
{
    public float vidasEnemigo = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(vidasEnemigo <=0){
        Destroy(this.gameObject);
       } 
    }

    public void Ataque(int ataque){
        vidasEnemigo -= ataque;
        Debug.Log("Las vidas del enemigo esta en: " + vidasEnemigo);
    }
}
