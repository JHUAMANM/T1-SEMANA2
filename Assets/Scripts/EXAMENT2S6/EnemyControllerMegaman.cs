using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerMegaman : MonoBehaviour
{
    public float vidasEnemigo = 3;    
    void Start()
    {
        
    }

    void Update()
    {
        if(vidasEnemigo<=0){
            Destroy(this.gameObject);
        }
    }
    public void Attacking(int ataque){
        vidasEnemigo -= ataque;
        Debug.Log("Vida Enemigo: " + vidasEnemigo);
    }
}