using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArmas : MonoBehaviour
{  
   
   public SpriteRenderer srArmas;
   public Sprite[] sprites;

   public int next = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Armas(){
        srArmas.sprite = sprites[next];
        next++;
        if(next == sprites.Length){
            next = 0;
        }
    }
}
